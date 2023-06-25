using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Stats;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRange_136)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseDamage_138)]
    [SpellEffectHandler(EffectsEnum.Effect_AddStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_AddChance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRange)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWeaponDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_MeleeDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_RangedDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDodgeMPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDodgeAPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMeleeResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRangedResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddSpellResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddLock)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEvade)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddTrapBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_WeaponDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddTrapBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddHealBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SpellDamageDonePercent)]
    public class StatsBuff : SpellEffectHandler
    {
        public const FightDispellableEnum Dispellable = FightDispellableEnum.DISPELLABLE;

        public StatsBuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = Effect.GetDelta();

            foreach (var target in targets)
            {
                Characteristic characteristic = GetEffectCaracteristic(target);
                AddStatBuff(target, delta, characteristic, Dispellable);
            }

        }

        private Characteristic GetEffectCaracteristic(Fighter target)
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_AddRange:
                    return target.Stats[CharacteristicEnum.RANGE];

                case EffectsEnum.Effect_AddRange_136:
                    return target.Stats[CharacteristicEnum.RANGE];

                case EffectsEnum.Effect_IncreaseDamage_138:
                    return target.Stats[CharacteristicEnum.DAMAGE_PERCENT];

                case EffectsEnum.Effect_AddAgility:
                    return target.Stats.Agility;

                case EffectsEnum.Effect_AddChance:
                    return target.Stats.Chance;

                case EffectsEnum.Effect_AddIntelligence:
                    return target.Stats.Intelligence;

                case EffectsEnum.Effect_AddStrength:
                    return target.Stats.Strength;

                case EffectsEnum.Effect_AddWisdom:
                    return target.Stats.Wisdom;

                case EffectsEnum.Effect_AddWeaponDamageBonus:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON];

                case EffectsEnum.Effect_MeleeDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE];

                case EffectsEnum.Effect_RangedDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE];

                case EffectsEnum.Effect_AddDamageBonus:
                    return target.Stats[CharacteristicEnum.ALL_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddCriticalHit:
                    return target.Stats[CharacteristicEnum.CRITICAL_HIT];

                case EffectsEnum.Effect_AddDodgeMPProbability:
                    return target.Stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY];

                case EffectsEnum.Effect_AddDodgeAPProbability:
                    return target.Stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY];

                case EffectsEnum.Effect_AddMeleeResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE];

                case EffectsEnum.Effect_AddRangedResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE];

                case EffectsEnum.Effect_AddSpellResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS];

                case EffectsEnum.Effect_AddLock:
                    return target.Stats[CharacteristicEnum.TACKLE_BLOCK];

                case EffectsEnum.Effect_AddWaterDamageBonus:
                    return target.Stats[CharacteristicEnum.WATER_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddFireDamageBonus:
                    return target.Stats[CharacteristicEnum.FIRE_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddAirDamageBonus:
                    return target.Stats[CharacteristicEnum.AIR_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddEarthDamageBonus:
                    return target.Stats[CharacteristicEnum.EARTH_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddNeutralDamageBonus:
                    return target.Stats[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddAPAttack:
                    return target.Stats[CharacteristicEnum.AP_REDUCTION];

                case EffectsEnum.Effect_AddMPAttack:
                    return target.Stats[CharacteristicEnum.MP_REDUCTION];

                case EffectsEnum.Effect_AddFireResistPercent:
                    return target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_AddWaterResistPercent:
                    return target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_AddEarthResistPercent:
                    return target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_AddAirResistPercent:
                    return target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_AddNeutralResistPercent:
                    return target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_AddEvade:
                    return target.Stats[CharacteristicEnum.TACKLE_EVADE];

                case EffectsEnum.Effect_AddDamageBonusPercent:
                    return target.Stats[CharacteristicEnum.DAMAGE_PERCENT];

                case EffectsEnum.Effect_AddNeutralElementReduction:
                    return target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_AddFireElementReduction:
                    return target.Stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_AddAirElementReduction:
                    return target.Stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_AddEarthElementReduction:
                    return target.Stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_AddWaterElementReduction:
                    return target.Stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_AddTrapBonusPercent:
                    return target.Stats[CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT];

                case EffectsEnum.Effect_WeaponDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON];

                case EffectsEnum.Effect_AddPushDamageReduction:
                    return target.Stats[CharacteristicEnum.PUSH_DAMAGE_REDUCTION];

                case EffectsEnum.Effect_AddTrapBonus:
                    return target.Stats[CharacteristicEnum.TRAP_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddHealBonus:
                    return target.Stats[CharacteristicEnum.HEAL_BONUS];

                case EffectsEnum.Effect_AddPushDamageBonus:
                    return target.Stats[CharacteristicEnum.PUSH_DAMAGE_BONUS];

                case EffectsEnum.Effect_AddCriticalDamageReduction:
                    return target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION];

                case EffectsEnum.Effect_AddCriticalDamageBonus:
                    return target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS];

                case EffectsEnum.Effect_SpellDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS];

                default:
                    target.Fight.Warn(Effect.EffectEnum + " cannot be applied to stat buff.");
                    return null;
            }
        }
    }
}
