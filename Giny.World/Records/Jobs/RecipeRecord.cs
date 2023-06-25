using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Enums;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Jobs
{
    [D2OClass("Recipe")]
    [Table("recipes")]
    public class RecipeRecord : ITable
    {
        [Container]
        private static Dictionary<long, RecipeRecord> Recipes = new Dictionary<long, RecipeRecord>();

        [Primary]
        [D2OField("resultId")]
        public long ResultId
        {
            get;
            set;
        }

        [Ignore]
        public ItemRecord ResultRecord
        {
            get;
            set;
        }

        [D2OField("ingredientIds")]
        public List<short> Ingredients
        {
            get;
            set;
        }
        [D2OField("quantities")]
        public List<int> Quantities
        {
            get;
            set;
        }
        [Ignore]
        public Dictionary<short, int> IngredientsQuantities
        {
            get;
            set;
        }
        [D2OField("jobId")]
        public long JobId
        {
            get;
            set;
        }
        [D2OField("skillId")]
        public long SkillId
        {
            get;
            set;
        }

        [Ignore]
        public long Id => ResultId;

        [StartupInvoke("Recipes", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var recipe in Recipes.Values)
            {
                recipe.IngredientsQuantities = new Dictionary<short, int>();

                for (int i = 0; i < recipe.Ingredients.Count; i++)
                {
                    recipe.IngredientsQuantities.Add(recipe.Ingredients[i], recipe.Quantities[i]);
                    recipe.ResultRecord = ItemRecord.GetItem(recipe.ResultId);
                }
            }
        }

        public static IEnumerable<RecipeRecord> GetRecipesRecords()
        {
            return Recipes.Values;
        }
        public static RecipeRecord GetRecipeRecord(long resultId)
        {
            return Recipes.TryGetValue(resultId);
        }
        public static RecipeRecord GetRecipeRecord(short[] gids, int[] quantities)
        {
            Dictionary<short, int> ingredientsQuantities = new Dictionary<short, int>();

            for (int i = 0; i < gids.Length; i++)
            {
                ingredientsQuantities.Add(gids[i], quantities[i]);
            }

            foreach (var recipe in Recipes.Values)
            {
                if (recipe.IsValidRecipe(ingredientsQuantities))
                {
                    return recipe;
                }
            }

            return null;
        }

        [PerformanceIssue]
        private bool IsValidRecipe(Dictionary<short, int> ingredientsQuantities)
        {
            if (ingredientsQuantities.Count != IngredientsQuantities.Count)
                return false;

            foreach (var ingredient in IngredientsQuantities)
            {
                if (ingredientsQuantities.ContainsKey(ingredient.Key))
                {
                    int requiredQuantity = ingredientsQuantities[ingredient.Key];

                    if (requiredQuantity != ingredient.Value)
                        return false;
                }
                else
                    return false;
            }
            return true;
        }

        public int GetMaximumCount(ItemCollection<CharacterItemRecord> collection)
        {
            int min = int.MaxValue;

            foreach (var ingredient in IngredientsQuantities)
            {
                var gid = ingredient.Key;
                var quantity = ingredient.Value;

                var item = collection.GetItems().Where(x => x.GId == gid).MaxBy(x => x.Quantity);

                var value = item.Quantity / ingredient.Value;

                if (value < min)
                {
                    min = value;
                }
            }

            return min;
        }


    }
}
