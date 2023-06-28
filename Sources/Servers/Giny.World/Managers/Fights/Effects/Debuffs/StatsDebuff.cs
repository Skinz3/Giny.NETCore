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
                AddStatBuff(target, (short)-delta, target.Stats[GetAssociatedCharacteristicEnum()], Dispellable);
            }

        }

       
    }
}
