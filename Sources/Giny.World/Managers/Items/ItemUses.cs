using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
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
    class ItemUses
    {
        [ItemUsageHandler(EffectsEnum.Effect_ConsultDocument)]
        public static bool ConsultDocument(Character character, EffectInteger effect)
        {
            character.OpenBookDialog(effect.Value);
            return false;
        }
        [ItemUsageHandler(EffectsEnum.Effect_TeleportToSavePoint)]
        public static bool TeleportSavePoint(Character character, EffectInteger effect)
        {
            character.PlayEmote(61);
            character.SpawnPoint();
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_LearnEmote)]
        public static bool LearnEmote(Character character, EffectInteger effect)
        {
            character.LearnEmote((byte)effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentAgility)]
        public static bool PermanentAgility(Character character, EffectInteger effect)
        {
            character.Record.Stats.Agility.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 12, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentStrength)]
        public static bool PermanentStrength(Character character, EffectInteger effect)
        {
            character.Record.Stats.Strength.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 10, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentIntelligence)]
        public static bool PermanentIntelligence(Character character, EffectInteger effect)
        {
            character.Record.Stats.Intelligence.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 14, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentWisdom)]
        public static bool PermanentWisdom(Character character, EffectInteger effect)
        {
            character.Record.Stats.Wisdom.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 9, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentChance)]
        public static bool PermanentChance(Character character, EffectInteger effect)
        {
            character.Record.Stats.Chance.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 11, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddPermanentVitality)]
        public static bool PermanentVitality(Character character, EffectInteger effect)
        {
            character.Record.Stats[CharacteristicEnum.VITALITY].Additional += (short)effect.Value;
            character.Record.Stats.LifePoints += effect.Value;
            character.Record.Stats.MaxLifePoints += effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 13, effect.Value);
            return true;
        }
        [ItemUsageHandler(EffectsEnum.Effect_AddExp)]
        public static bool AddExp(Character character, EffectInteger effect)
        {
            character.AddExperience(effect.Value, true);
            return true;
        }
        [ItemUsageHandler(14485)]
        public static bool Mimicry(Character character, CharacterItemRecord item)
        {
            character.OpenUIByObject(ObjectUITypeEnum.MIMICRY, item.UId);
            return false;
        }

        [ItemUsageHandler(16836)]
        public static bool Container16836(Character character, CharacterItemRecord item)
        {
            character.Inventory.AddItem(2012, 10);
            return true;
        }
        [ItemUsageHandler(16833)]
        public static bool Container16833(Character character, CharacterItemRecord item)
        {
            character.Inventory.AddItem(519, 10);
            return true;
        }
        [ItemUsageHandler(16828)]
        public static bool Container16828(Character character, CharacterItemRecord item)
        {
            character.Inventory.AddItem(6671, 10);
            return true;
        }
    }
}
