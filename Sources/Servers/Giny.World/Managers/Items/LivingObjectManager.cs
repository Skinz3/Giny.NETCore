using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items
{
    public class LivingObjectManager : Singleton<LivingObjectManager>
    {
        public void InitializeLivingObject(CharacterItemRecord item)
        {
            if (!item.Effects.Exists(EffectsEnum.Effect_LastMealDate)) 
            {
                return;
            }
            LivingObjectRecord record = LivingObjectRecord.GetLivingObjectRecord(item.GId);

            item.Effects.Clear();
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_LivingObjectCategory, (int)record.Type));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_LivingObjectLevel, 0));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_LivingObjectMood, 0));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_LivingObjectSkin, 1));

            if (record.Skinnable)
                item.AppearanceId = record.SkinIds[0];
        }

        public void AssociateLivingObject(Character character, CharacterItemRecord livingObject, CharacterItemRecord targeted)
        {
            LivingObjectRecord record = LivingObjectRecord.GetLivingObjectRecord(livingObject.GId);

            if (record != null)
            {
                if (record.Type == targeted.Record.TypeEnum)
                {
                    targeted.Effects.Add(new EffectInteger(EffectsEnum.Effect_LivingObjectId, (short)livingObject.Record.Id));

                    foreach (var effect in livingObject.Effects)
                    {
                        targeted.Effects.Add(effect);
                    }

                    character.Inventory.OnItemModified(targeted);
                    character.Inventory.RefreshWeight();

                    if (targeted.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED && record.Skinnable)
                    {
                        character.Inventory.UpdateItemAppearence(targeted, livingObject.AppearanceId);
                        character.RefreshActorOnMap();
                    }

                    character.Inventory.RemoveItem(livingObject, livingObject.Quantity);

                }
            }
            else
            {
                Logger.Write("Unable to associate living object. Unknown LivingObjectRecord.", Channels.Warning);
            }
        }

        public void ChangeLivingObjectSkin(Character character, CharacterItemRecord item, int skinIndex, CharacterInventoryPositionEnum livingPosition)
        {
            if (item.Quantity > 1)
            {
                return;
            }

            EffectInteger effect = item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectSkin);

            if (effect != null)
            {
                LivingObjectRecord record = LivingObjectRecord.IsLivingObject(item.GId) ? LivingObjectRecord.GetLivingObjectRecord(item.GId) : LivingObjectRecord.GetLivingObjectRecord((short)item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectId).Value);

                short skin = record.GetSkin(skinIndex);

                if (record.Skinnable)
                {
                    if (livingPosition != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        character.Inventory.UpdateItemAppearence(item, skin);
                    }
                    else
                    {
                        item.AppearanceId = skin;
                    }

                    character.RefreshActorOnMap();
                }

                effect.Value = (short)skinIndex;
                character.Inventory.OnItemModified(item);
                character.Inventory.RefreshWeight();
                item.UpdateElement();
            }
            else
            {
                Logger.Write("unable to modify item skin..its not a valid living object.", Channels.Warning);
            }
        }

        public void DissociateLivingObject(Character character, CharacterItemRecord item)
        {
            EffectInteger effect = item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectId);

            if (effect != null)
            {
                var levelEffect = item.Effects.GetFirst<Effect>(EffectsEnum.Effect_LivingObjectLevel);
                var categoryEffect = item.Effects.GetFirst<Effect>(EffectsEnum.Effect_LivingObjectCategory);
                var moodEffect = item.Effects.GetFirst<Effect>(EffectsEnum.Effect_LivingObjectMood);
                var skinEffect = item.Effects.GetFirst<Effect>(EffectsEnum.Effect_LivingObjectSkin);

                var newItem = ItemsManager.Instance.CreateCharacterItem(ItemRecord.GetItem(effect.Value), character.Id, item.Quantity);
                newItem.Effects.Clear();
                newItem.Effects.Add(levelEffect);
                newItem.Effects.Add(categoryEffect);
                newItem.Effects.Add(moodEffect);
                newItem.Effects.Add(skinEffect);
                newItem.AppearanceId = item.AppearanceId;

                item.Effects.RemoveAll(EffectsEnum.Effect_LivingObjectId);
                item.Effects.RemoveAll(EffectsEnum.Effect_LivingObjectLevel);
                item.Effects.RemoveAll(EffectsEnum.Effect_LivingObjectCategory);
                item.Effects.RemoveAll(EffectsEnum.Effect_LivingObjectMood);
                item.Effects.RemoveAll(EffectsEnum.Effect_LivingObjectSkin);

                character.Inventory.AddItem(newItem);

                if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    character.Inventory.UpdateItemAppearence(item, item.Record.AppearenceId);
                    character.Inventory.OnItemModified(item);
                }
                else
                {
                    item.AppearanceId = item.Record.AppearenceId;

                    if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        if (!character.Inventory.CheckPassiveStacking(item))
                        {
                            character.Inventory.OnItemModified(item);
                        }
                    }
                    else
                    {
                        character.Inventory.OnItemModified(item);
                    }

                }
                item.UpdateElement();


            }
        }

        public void FeedLivingObject(Character character, CharacterItemRecord item, ObjectItemQuantity[] meal)
        {
            var record = LivingObjectRecord.GetLivingObjectRecord((short)item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectId).Value);

            foreach (var mealInfo in meal)
            {
                var experienceEffect = item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectLevel);
                var mealItem = character.Inventory.GetItem(mealInfo.objectUID);

                if (experienceEffect.Value + mealItem.Record.Level > record.MaximumExp)
                {
                    experienceEffect.Value = record.MaximumExp;
                }
                else
                {
                    experienceEffect.Value += mealItem.Record.Level;
                }
                character.Inventory.RemoveItem(mealInfo.objectUID, mealInfo.quantity);
            }

            character.Inventory.OnItemModified(item);
        }
    }
}
