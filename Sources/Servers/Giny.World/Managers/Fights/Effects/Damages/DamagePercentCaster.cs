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
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentCasterEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentCasterFire)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentCasterWater)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentCasterAir)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentCasterNeutral)]
    public class DamagePercentCaster : SpellEffectHandler
    {
        public DamagePercentCaster(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override bool Reveals => true;

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                double deltaMin = (Effect.Min / 100d) * Source.Stats.LifePoints;
                double deltaMax = Effect.IsDice ? (Effect.Max / 100d) * Source.Stats.LifePoints : deltaMin;

                Damage damage = new Damage(Source, target, GetEffectSchool(), deltaMin, deltaMax, this);
                damage.IgnoreBoost = true; // good
                target.InflictDamage(damage);
            }
        }
        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamagePercentCasterEarth:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_DamagePercentCasterFire:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_DamagePercentCasterWater:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_DamagePercentCasterAir:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_DamagePercentCasterNeutral:
                    return EffectElementEnum.Neutral;
            }
            return EffectElementEnum.Undefined;
        }
    }
}
