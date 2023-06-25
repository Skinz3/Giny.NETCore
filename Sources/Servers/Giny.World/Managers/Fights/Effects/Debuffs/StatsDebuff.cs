using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDamageBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_SubChance)]
    [SpellEffectHandler(EffectsEnum.Effect_SubIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRange)]
    [SpellEffectHandler(EffectsEnum.Effect_SubMeleeDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRangedDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDodgeMPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDodgeAPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_SubMeleeResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRangedResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_SubSpellResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_SubLock)]
    [SpellEffectHandler(EffectsEnum.Effect_SubFireResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubEarthResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubNeutralResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAirResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubWaterResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubEvade)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_SubMPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_SubEarthElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAirElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubFireElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubWaterElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubNeutralElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubPushDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubPushDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubHealBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubSpellDamageDonePercent)]
    public class StatsDebuff : SpellEffectHandler
    {
        public const FightDispellableEnum Dispellable = FightDispellableEnum.DISPELLABLE;

        public StatsDebuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = Effect.GetDelta();

            foreach (var target in targets)
            {
                Characteristic characteristic = GetEffectCaracteristic(target);
                AddStatBuff(target, (short)-delta, characteristic, Dispellable);
            }

        }

        private Characteristic GetEffectCaracteristic(Fighter target)
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_SubRange:
                    return target.Stats[CharacteristicEnum.RANGE];

                case EffectsEnum.Effect_SubDamageBonusPercent:
                    return target.Stats[CharacteristicEnum.DAMAGE_PERCENT];

                case EffectsEnum.Effect_SubAgility:
                    return target.Stats.Agility;

                case EffectsEnum.Effect_SubChance:
                    return target.Stats.Chance;

                case EffectsEnum.Effect_SubIntelligence:
                    return target.Stats.Intelligence;

                case EffectsEnum.Effect_SubStrength:
                    return target.Stats.Strength;

                case EffectsEnum.Effect_SubWisdom:
                    return target.Stats.Wisdom;

                case EffectsEnum.Effect_SubMeleeDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE];

                case EffectsEnum.Effect_SubRangedDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE];

                case EffectsEnum.Effect_SubDamageBonus:
                    return target.Stats[CharacteristicEnum.ALL_DAMAGE_BONUS];

                case EffectsEnum.Effect_SubCriticalHit:
                    return target.Stats[CharacteristicEnum.CRITICAL_HIT];

                case EffectsEnum.Effect_SubDodgeMPProbability:
                    return target.Stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY];

                case EffectsEnum.Effect_SubDodgeAPProbability:
                    return target.Stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY];

                case EffectsEnum.Effect_SubMeleeResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE];

                case EffectsEnum.Effect_SubRangedResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE]; 

                case EffectsEnum.Effect_SubSpellResistance:
                    return target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS];

                case EffectsEnum.Effect_SubLock:
                    return target.Stats[CharacteristicEnum.TACKLE_BLOCK];

                case EffectsEnum.Effect_SubFireResistPercent:
                    return target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_SubEarthResistPercent:
                    return target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_SubNeutralResistPercent:
                    return target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_SubAirResistPercent:
                    return target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_SubWaterResistPercent:
                    return target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT];

                case EffectsEnum.Effect_SubEvade:
                    return target.Stats[CharacteristicEnum.TACKLE_EVADE];

                case EffectsEnum.Effect_SubAPAttack:
                    return target.Stats[CharacteristicEnum.AP_REDUCTION];

                case EffectsEnum.Effect_SubMPAttack:
                    return target.Stats[CharacteristicEnum.MP_REDUCTION];

                case EffectsEnum.Effect_SubEarthElementReduction:
                    return target.Stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_SubAirElementReduction:
                    return target.Stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_SubFireElementReduction:
                    return target.Stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_SubWaterElementReduction:
                    return target.Stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_SubNeutralElementReduction:
                    return target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION];

                case EffectsEnum.Effect_SubPushDamageBonus:
                    return target.Stats[CharacteristicEnum.PUSH_DAMAGE_BONUS];

                case EffectsEnum.Effect_SubPushDamageReduction:
                    return target.Stats[CharacteristicEnum.PUSH_DAMAGE_REDUCTION];

                case EffectsEnum.Effect_SubHealBonus:
                    return target.Stats[CharacteristicEnum.HEAL_BONUS];

                case EffectsEnum.Effect_SubCriticalDamageReduction:
                    return target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION];

                case EffectsEnum.Effect_SubSpellDamageDonePercent:
                    return target.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS];

                default:
                    target.Fight.Warn(Effect.EffectEnum + " cannot be applied to stat buff.");
                    return null;
            }
        }
    }
}
