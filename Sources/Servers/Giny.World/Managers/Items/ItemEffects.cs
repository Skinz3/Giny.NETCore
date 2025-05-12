using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items
{
    class ItemEffects
    {
        [ItemEffect(EffectsEnum.Effect_AddAP_111)]
        public static void AddAp111(Character character, int delta)
        {
            character.Record.Stats.ActionPoints.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddMP_128)]
        public static void AddMp128(Character character, int delta)
        {
            character.Record.Stats.MovementPoints.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddMP)]
        public static void AddMp(Character character, int delta)
        {
            character.Record.Stats.MovementPoints.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddStrength)]
        public static void AddStrength(Character character, int delta)
        {
            character.Record.Stats.Strength.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddChance)]
        public static void AddChance(Character character, int delta)
        {
            character.Record.Stats.Chance.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddIntelligence)]
        public static void AddIntelligence(Character character, int delta)
        {
            character.Record.Stats.Intelligence.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAgility)]
        public static void AddAgility(Character character, int delta)
        {
            character.Record.Stats.Agility.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWisdom)]
        public static void AddWisdom(Character character, int delta)
        {
            character.Record.Stats.Wisdom.Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddVitality)]
        public static void AddVitality(Character character, int delta)
        {
            var test1 = character.Stats.LifePoints;

            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.VITALITY).Objects += (short)delta;


            var vit = character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.VITALITY).TotalInContext();

            var test = character.Stats.LifePoints;
        }
        [ItemEffect(EffectsEnum.Effect_AddHealth)]
        public static void AddHealth(Character character, int delta)
        {
            AddVitality(character, delta);
        }
        [ItemEffect(EffectsEnum.Effect_SubVitality)]
        public static void SubVitality(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.VITALITY).Objects -= (short)delta;

        }
        [ItemEffect(EffectsEnum.Effect_AddInitiative)]
        public static void AddInitiative(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.INITIATIVE).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubInitiative)]
        public static void SubInitiative(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.INITIATIVE).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddRange)]
        public static void AddRange(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RANGE).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddCriticalHit)]
        public static void AddCriticalHit(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_HIT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubCriticalHit)]
        public static void SubCriticalHit(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_HIT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddDamageBonus)]
        public static void AddDamagesBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.ALL_DAMAGE_BONUS).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddTrapBonus)]
        public static void AddTrapBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TRAP_DAMAGE_BONUS).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddTrapBonusPercent)]
        public static void AddTrapBonusPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_IncreaseDamage_138)]
        public static void IncreaseDamage138(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DAMAGE_PERCENT).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddProspecting)]
        public static void AddProspecting(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.MAGIC_FIND).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddSummonLimit)]
        public static void AddSummonLimit(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.MAX_SUMMONED_CREATURES_BOOST).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddLock)]
        public static void AddLock(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TACKLE_BLOCK).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubLock)]
        public static void SubLock(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TACKLE_BLOCK).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddEvade)]
        public static void AddEvade(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TACKLE_EVADE).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubEvade)]
        public static void SubEvade(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.TACKLE_EVADE).Objects -= (short)delta;
        }



        [ItemEffect(EffectsEnum.Effect_AddNeutralResistPercent)]
        public static void AddNeutralResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubNeutralResistPercent)]
        public static void SubNeutralResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAirResistPercent)]
        public static void AddAirResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubAirResistPercent)]
        public static void SubAirResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWaterResistPercent)]
        public static void AddWaterResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubWaterResistPercent)]
        public static void SubWaterResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddPushDamageBonus)]
        public static void AddPushDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.PUSH_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddPushDamageReduction)]
        public static void AddPushDamageReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.PUSH_DAMAGE_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddCriticalDamageReduction)]
        public static void AddCriticalDamageReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubCriticalDamageReduction)]
        public static void SubCriticalDamageReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddCriticalDamageBonus)]
        public static void AddCriticalDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubCriticalDamageBonus)]
        public static void SubCriticalDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.CRITICAL_DAMAGE_BONUS).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddFireResistPercent)]
        public static void AddFireResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubFireResistPercent)]
        public static void SubFireResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddEarthResistPercent)]
        public static void AddEarthResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubEarthResistPercent)]
        public static void SubEarthResistPercent(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWaterDamageBonus)]
        public static void WaterDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.WATER_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAirDamageBonus)]
        public static void AirDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AIR_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddEarthDamageBonus)]
        public static void EarthDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.EARTH_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddNeutralDamageBonus)]
        public static void NeutralDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.NEUTRAL_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddFireDamageBonus)]
        public static void FireDamageBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.FIRE_DAMAGE_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddEarthElementReduction)]
        public static void EarthElementReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.EARTH_ELEMENT_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddFireElementReduction)]
        public static void FireElementReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.FIRE_ELEMENT_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddHealBonus)]
        public static void HealBonus(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.HEAL_BONUS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWaterElementReduction)]
        public static void WaterElementReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.WATER_ELEMENT_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAirElementReduction)]
        public static void AirElementReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AIR_ELEMENT_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddNeutralElementReduction)]
        public static void NeutralElementReduction(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddDamageReflection_220)]
        public static void AddReflect(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.REFLECT_DAMAGE).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_LearnEmote)]
        public static void Emote(Character character, int delta)
        {
            if (delta > 0)
                character.LearnEmote((byte)delta);
            else
                character.ForgetEmote((byte)Math.Abs(delta));
        }
        [ItemEffect(EffectsEnum.Effect_AddTitle)]
        public static void Title(Character character, int delta)
        {
            if (delta > 0)
            {
                if (character.LearnTitle((short)delta))
                {
                    character.ActiveTitle((short)delta);
                }
            }
            else
            {
                character.ForgetTitle((short)Math.Abs(delta));
            }
        }
        [ItemEffect(EffectsEnum.Effect_AddMeleeDamageMultiplier)]
        public static void AddMeleeDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubMeleeDamageMultiplier)]
        public static void SubMeeleDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubMeleeDamageReceivedMultiplier)]
        public static void AddMeleeResistances(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddMeleeDamageReceivedMultiplier)]
        public static void SubMeeleeResistance(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE).Objects += (short)delta;
        }


        [ItemEffect(EffectsEnum.Effect_AddWeaponDamageMultiplier)]
        public static void AddWeaponDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubWeaponDamageMultiplier)]
        public static void SubWeaponDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubWeaponDamageReceivedMultiplier)]
        public static void AddWeaponResistance(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWeaponDamageReceivedMultiplier)]
        public static void SubWeaponResistance(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddRangeDamageMultiplier)]
        public static void AddRangeDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubRangeDamageMultiplier)]
        public static void SuRangedDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubRangeDamageReceivedMultiplier)]
        public static void AddRangedResistances(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddRangeDamageReceivedMultiplier)]
        public static void SubRangedResistance(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddSpellDamageMultiplier)]
        public static void AddSpellDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubSpellDamageMultiplier)]
        public static void SubSpellDamageDone(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubSpellReceivedDamageMultiplier)]
        public static void AddSpellResistances(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddSpellReceivedDamageMultiplier)]
        public static void SubSpellResistance(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddDodgeAPProbability)]
        public static void AddDodgeAppProbability(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DODGE_AP_LOST_PROBABILITY).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubDodgeAPProbability)]
        public static void SubDodgeAPProbability(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DODGE_AP_LOST_PROBABILITY).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddDodgeMPProbability)]
        public static void AddDodgeMPProbability(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DODGE_MP_LOST_PROBABILITY).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubDodgeMPProbability)]
        public static void SubDodgeMPProbability(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.DODGE_MP_LOST_PROBABILITY).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddAPAttack)]
        public static void AddApAttack(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AP_REDUCTION).Objects += (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubAPAttack)]
        public static void SubApAttack(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.AP_REDUCTION).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddMPAttack)]
        public static void AddMpAttack(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.MP_REDUCTION).Objects += (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubMPAttack)]
        public static void SubMpAttack(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.MP_REDUCTION).Objects -= (short)delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubChance)]
        public static void SubChance(Character character, int delta)
        {
            character.Record.Stats.Chance.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubIntelligence)]
        public static void SubIntelligence(Character character, int delta)
        {
            character.Record.Stats.Intelligence.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubStrength)]
        public static void SubStrength(Character character, int delta)
        {
            character.Record.Stats.Strength.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubAgility)]
        public static void SubAgility(Character character, int delta)
        {
            character.Record.Stats.Agility.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubWisdom)]
        public static void SubWisdom(Character character, int delta)
        {
            character.Record.Stats.Wisdom.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubMP)]
        public static void SubMp(Character character, int delta)
        {
            character.Record.Stats.MovementPoints.Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubRange)]
        public static void SubRange(Character character, int delta)
        {
            character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.RANGE).Objects -= (short)delta;
        }
        [ItemEffect(EffectsEnum.Effect_984)]
        public static void BeSubscribed(Character character, int delta)
        {
            // Seems this effect has been removed but is still within D2oFiles
        }
    }
}
