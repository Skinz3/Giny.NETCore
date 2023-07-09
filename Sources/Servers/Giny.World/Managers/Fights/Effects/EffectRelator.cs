using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects
{
    /// <summary>
    /// Effects.d2o has a characteristicId field 
    /// but it seems to be missing some values ​​in 2.63...
    /// </summary>
    public class EffectRelator
    {
        public static CharacteristicEnum GetAssociatedCharacteristicEnum(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_AddChance:
                case EffectsEnum.Effect_StealChance:
                    return CharacteristicEnum.CHANCE;

                case EffectsEnum.Effect_StealWisdom:
                    return CharacteristicEnum.WISDOM;

                case EffectsEnum.Effect_AddIntelligence:
                case EffectsEnum.Effect_StealIntelligence:
                    return CharacteristicEnum.INTELLIGENCE;

                case EffectsEnum.Effect_StealAgility:
                case EffectsEnum.Effect_AddAgility:
                    return CharacteristicEnum.AGILITY;

                case EffectsEnum.Effect_AddStrength:
                case EffectsEnum.Effect_StealStrength:
                    return CharacteristicEnum.STRENGTH;

                case EffectsEnum.Effect_AddRange:
                case EffectsEnum.Effect_AddRange_136:
                case EffectsEnum.Effect_StealRange:
                    return CharacteristicEnum.RANGE;

                case EffectsEnum.Effect_IncreaseDamage_138:
                    return CharacteristicEnum.DAMAGE_PERCENT;

                case EffectsEnum.Effect_AddWisdom:
                    return CharacteristicEnum.WISDOM;

                case EffectsEnum.Effect_AddWeaponDamageBonus:
                    return CharacteristicEnum.WEAPON_POWER;

                case EffectsEnum.Effect_MeleeDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_RangedDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_AddDamageBonus:
                    return CharacteristicEnum.ALL_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddCriticalHit:
                    return CharacteristicEnum.CRITICAL_HIT;

                case EffectsEnum.Effect_AddDodgeMPProbability:
                    return CharacteristicEnum.DODGE_MP_LOST_PROBABILITY;

                case EffectsEnum.Effect_AddDodgeAPProbability:
                    return CharacteristicEnum.DODGE_AP_LOST_PROBABILITY;

                case EffectsEnum.Effect_AddMeleeResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_AddRangedResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_AddSpellResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_AddLock:
                    return CharacteristicEnum.TACKLE_BLOCK;

                case EffectsEnum.Effect_AddWaterDamageBonus:
                    return CharacteristicEnum.WATER_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddFireDamageBonus:
                    return CharacteristicEnum.FIRE_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddAirDamageBonus:
                    return CharacteristicEnum.AIR_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddEarthDamageBonus:
                    return CharacteristicEnum.EARTH_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddNeutralDamageBonus:
                    return CharacteristicEnum.NEUTRAL_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddAPAttack:
                    return CharacteristicEnum.AP_REDUCTION;

                case EffectsEnum.Effect_AddMPAttack:
                    return CharacteristicEnum.MP_REDUCTION;

                case EffectsEnum.Effect_AddFireResistPercent:
                    return CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddWaterResistPercent:
                    return CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddEarthResistPercent:
                    return CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddAirResistPercent:
                    return CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddNeutralResistPercent:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_SubEvadePercent:
                case EffectsEnum.Effect_SubEvade:
                case EffectsEnum.Effect_AddEvade:
                    return CharacteristicEnum.TACKLE_EVADE;

                case EffectsEnum.Effect_AddDamageBonusPercent:
                    return CharacteristicEnum.DAMAGE_PERCENT;

                case EffectsEnum.Effect_AddNeutralElementReduction:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddFireElementReduction:
                    return CharacteristicEnum.FIRE_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddAirElementReduction:
                    return CharacteristicEnum.AIR_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddEarthElementReduction:
                    return CharacteristicEnum.EARTH_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddWaterElementReduction:
                    return CharacteristicEnum.WATER_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddTrapBonusPercent:
                    return CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT;

                case EffectsEnum.Effect_SubWeaponDamageDonePercent:
                case EffectsEnum.Effect_WeaponDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON;

                case EffectsEnum.Effect_AddPushDamageReduction:
                    return CharacteristicEnum.PUSH_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_AddTrapBonus:
                    return CharacteristicEnum.TRAP_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddHealBonus:
                    return CharacteristicEnum.HEAL_BONUS;

                case EffectsEnum.Effect_AddPushDamageBonus:
                    return CharacteristicEnum.PUSH_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddCriticalDamageReduction:
                    return CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_AddCriticalDamageBonus:
                    return CharacteristicEnum.CRITICAL_DAMAGE_BONUS;

                case EffectsEnum.Effect_SpellDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_IncreaseSpellDamage:
                    return CharacteristicEnum.DAMAGE_PERCENT_SPELL;

                case EffectsEnum.Effect_SubRange:
                    return CharacteristicEnum.RANGE;

                case EffectsEnum.Effect_SubDamageBonusPercent:
                    return CharacteristicEnum.DAMAGE_PERCENT;

                case EffectsEnum.Effect_SubAgility:
                    return CharacteristicEnum.AGILITY;

                case EffectsEnum.Effect_SubChance:
                    return CharacteristicEnum.CHANCE;

                case EffectsEnum.Effect_SubIntelligence:
                    return CharacteristicEnum.INTELLIGENCE;

                case EffectsEnum.Effect_SubStrength:
                    return CharacteristicEnum.STRENGTH;

                case EffectsEnum.Effect_SubWisdom:
                    return CharacteristicEnum.WISDOM;

                case EffectsEnum.Effect_SubMeleeDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_SubRangedDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_SubDamageBonus:
                    return CharacteristicEnum.ALL_DAMAGE_BONUS;

                case EffectsEnum.Effect_SubCriticalHit:
                    return CharacteristicEnum.CRITICAL_HIT;

                case EffectsEnum.Effect_SubDodgeMPProbability:
                    return CharacteristicEnum.DODGE_MP_LOST_PROBABILITY;

                case EffectsEnum.Effect_SubDodgeAPProbability:
                    return CharacteristicEnum.DODGE_AP_LOST_PROBABILITY;

                case EffectsEnum.Effect_SubMeleeResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_SubRangedResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_SubSpellResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_SubLock:
                    return CharacteristicEnum.TACKLE_BLOCK;

                case EffectsEnum.Effect_SubFireResistPercent:
                    return CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_SubEarthResistPercent:
                    return CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_SubNeutralResistPercent:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_SubAirResistPercent:
                    return CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_SubWaterResistPercent:
                    return CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT;




                case EffectsEnum.Effect_SubAPAttack:
                    return CharacteristicEnum.AP_REDUCTION;

                case EffectsEnum.Effect_SubMPAttack:
                    return CharacteristicEnum.MP_REDUCTION;

                case EffectsEnum.Effect_SubEarthElementReduction:
                    return CharacteristicEnum.EARTH_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_SubAirElementReduction:
                    return CharacteristicEnum.AIR_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_SubFireElementReduction:
                    return CharacteristicEnum.FIRE_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_SubWaterElementReduction:
                    return CharacteristicEnum.WATER_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_SubNeutralElementReduction:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_SubPushDamageBonus:
                    return CharacteristicEnum.PUSH_DAMAGE_BONUS;

                case EffectsEnum.Effect_SubPushDamageReduction:
                    return CharacteristicEnum.PUSH_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_SubHealBonus:
                    return CharacteristicEnum.HEAL_BONUS;

                case EffectsEnum.Effect_SubCriticalDamageReduction:
                    return CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_SubSpellDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_ReduceFinalDamages:
                case EffectsEnum.Effect_IncreaseFinalDamages:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER;

                case EffectsEnum.Effect_AddErosion:
                    return CharacteristicEnum.PERMANENT_DAMAGE_PERCENT;



            }

            throw new NotImplementedException("Unknown corresponding characteristic for effect " + effect);
        }
    }
}
