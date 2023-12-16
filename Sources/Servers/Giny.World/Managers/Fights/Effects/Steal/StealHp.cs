using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_StealHPFire)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPWater)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPAir)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPNeutral)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPFix)]
    public class StealHp : SpellEffectHandler
    {
        public StealHp(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                DamageResult result = target.InflictDamage(CreateDamage(target));

                double healDelta = result.LifeLoss / 2d;

                Source.Heal(new Healing(Source, target, GetEffectSchool(), healDelta, healDelta, this, true));
            }
        }

        private Damage CreateDamage(Fighter target)
        {
            return new Damage(Source, target, GetEffectSchool(), Effect.Min, Effect.Max, this);
        }
        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_StealHPEarth:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_StealHPWater:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_StealHPFire:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_StealHPAir:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_StealHPNeutral:
                    return EffectElementEnum.Neutral;
                case EffectsEnum.Effect_StealHPFix:
                    return EffectElementEnum.None;
            }
            return EffectElementEnum.Undefined;
        }
    }
}
