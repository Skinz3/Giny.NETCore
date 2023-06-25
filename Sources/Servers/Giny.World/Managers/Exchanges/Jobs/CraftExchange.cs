using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using Giny.World.Records.Jobs;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges.Jobs
{
    public class CraftExchange : JobExchange
    {
        public CraftExchange(Character character, SkillRecord skill) : base(character, skill)
        {

        }

        public void SetRecipe(short gid)
        {
            RecipeRecord recipeRecord = RecipeRecord.GetRecipeRecord(gid);

            foreach (var ingredient in recipeRecord.IngredientsQuantities)
            {
                CharacterItemRecord item = Character.Inventory.GetFirstItem(ingredient.Key, ingredient.Value);

                if (item != null)
                {
                    Items.AddItem(item, ingredient.Value);
                }
            }
        }
        public override void SetCount(int count)
        {
            RecipeRecord recipe = GetCurrentRecipe();

            if (recipe == null)
            {
                return;
            }

            int countMax = recipe.GetMaximumCount(Character.Inventory);

            if (count > CountLimit)
            {
                count = CountLimit;
            }

            if (count <= countMax)
            {
                this.Count = count;
                Character.Client.Send(new ExchangeCraftCountModifiedMessage(count));
            }
        }


        private RecipeRecord GetCurrentRecipe()
        {
            short[] gids = Items.GetItems().Select(x => x.GId).ToArray();
            int[] quantities = Items.GetItems().Select(x => x.Quantity).ToArray();

            RecipeRecord recipeRecord = RecipeRecord.GetRecipeRecord(gids, quantities);
            return recipeRecord;
        }
        public override void Ready(bool ready, short step)
        {
            if (ready)
            {
                RecipeRecord recipeRecord = GetCurrentRecipe();

                if (recipeRecord != null && recipeRecord.JobId == CharacterJob.JobId && recipeRecord.SkillId == Skill.Id)
                {
                    ItemRecord resultRecord = ItemRecord.GetItem((int)recipeRecord.ResultId);

                    if (resultRecord.Level <= CharacterJob.Level || resultRecord == null)
                    {
                        PerformCraft(recipeRecord);
                    }
                    else
                    {
                        OnCraftResulted(CraftResultEnum.CRAFT_FAILED);
                    }

                }
                else
                {
                    OnCraftResulted(CraftResultEnum.CRAFT_FAILED);
                }
            }
        }
        private void PerformCraft(RecipeRecord recipe)
        {
            List<CharacterItemRecord> results = new List<CharacterItemRecord>();
            Dictionary<int, int> removed = new Dictionary<int, int>();

            for (int i = 0; i < Count; i++)
            {
                foreach (var ingredient in Items.GetItems())
                {
                    if (!removed.ContainsKey(ingredient.UId))
                        removed.Add(ingredient.UId, ingredient.Quantity);
                    else
                        removed[ingredient.UId] += ingredient.Quantity;

                }
                results.Add(ItemsManager.Instance.CreateCharacterItem(recipe.ResultRecord, Character.Id, 1));
            }

            foreach (var pair in removed)
            {
                var item = Character.Inventory.GetItem(pair.Key);

                if (item.Quantity < pair.Value)
                {
                    Character.ReplyWarning("Unable to perform craft. Unable to compute recipe.");
                    OnCraftResulted(CraftResultEnum.CRAFT_FORBIDDEN);
                    return;
                }
            }
            Character.Inventory.RemoveItems(removed);
            Character.Inventory.AddItems(results);

            if (results.Count == 1)
            {
                OnCraftResulted(CraftResultEnum.CRAFT_SUCCESS, results.Last().GetObjectItemNotInContainer());
            }
            else
            {
                OnCraftResulted(CraftResultEnum.CRAFT_SUCCESS, new ObjectItemNotInContainer()
                {
                    effects = recipe.ResultRecord.Effects.GetObjectEffects(),
                    objectGID = (short)recipe.ResultId,
                    objectUID = 0,
                    quantity = results.Count,
                });
            }

            int craftXpRatio = recipe.ResultRecord.CraftXpRatio;
            int exp = JobFormulas.Instance.GetCraftExperience(recipe.ResultRecord.Level, CharacterJob.Level, craftXpRatio);
            Character.AddJobExp(CharacterJob.JobId, exp * Count);
            Items.Clear();
            ResetCount();

        }
        private void OnCraftResulted(CraftResultEnum result)
        {
            Character.Client.Send(new ExchangeCraftResultMessage((byte)result));
        }
        private void OnCraftResulted(CraftResultEnum result, ObjectItemNotInContainer item)
        {
            Character.Client.Send(new ExchangeCraftResultWithObjectDescMessage()
            {
                craftResult = (byte)result,
                objectInfo = item,
            });
        }


    }
}
