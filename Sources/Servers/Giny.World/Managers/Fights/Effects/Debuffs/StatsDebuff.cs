using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
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
    [SpellEffectHandler(EffectsEnum.Effect_SubMeleeDamageMultiplier)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRangeDamageMultiplier)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDodgeMPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDodgeAPProbability)]

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
    [SpellEffectHandler(EffectsEnum.Effect_SubSpellDamageMultiplier)]
    [SpellEffectHandler(EffectsEnum.Effect_ReduceFinalDamages)]
    [SpellEffectHandler(EffectsEnum.Effect_SubMeleeDamageReceivedMultiplier)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRangeDamageReceivedMultiplier)]
    [SpellEffectHandler(EffectsEnum.Effect_SubSpellReceivedDamageMultiplier)]
    public class StatsDebuff : SpellEffectHandler
    {


        public StatsDebuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            int delta = Effect.GetDelta();

            foreach (var target in targets)
            {
                AddStatBuff(target, (short)-delta, target.Stats[GetAssociatedCharacteristicEnum()], Effect.DispellableEnum);

                switch (Effect.EffectEnum)
                {
                    case EffectsEnum.Effect_SubRange:
                    case EffectsEnum.Effect_SubRange_135:
                        target.TriggerBuffs(TriggerTypeEnum.OnRangeLost, null);
                        break;

                    case EffectsEnum.Effect_SubAPPercent:
                    case EffectsEnum.Effect_SubAP:
                    case EffectsEnum.Effect_SubAP_Roll:
                       
                        target.TriggerBuffs(TriggerTypeEnum.OnApRemovalAttempt, null);
                        Source.TriggerBuffs(TriggerTypeEnum.OnCasterRemoveApAttempt, null);
                        break;

                    case EffectsEnum.Effect_SubMPPercent:
                    case EffectsEnum.Effect_SubMP_Roll:
                    case EffectsEnum.Effect_SubMP:
                        target.TriggerBuffs(TriggerTypeEnum.OnMpRemovalAttempt, null);
                        Source.TriggerBuffs(TriggerTypeEnum.OnCasterRemoveMpAttempt, null);
                        break;

                }


            }

        }


    }
}
