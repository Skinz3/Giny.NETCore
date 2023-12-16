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
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerHpPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerHpPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerHpPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerHpPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerHpPercent)]
    public class DamagePercentTarget : SpellEffectHandler
    {
        public DamagePercentTarget(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override bool Reveals => true;

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                double deltaMin = (Effect.Min / 100d) * target.Stats.LifePoints;

                double deltaMax = Effect.IsDice ? (Effect.Max / 100d) * target.Stats.LifePoints : deltaMin;

                Damage damage = new Damage(Source, target, GetEffectSchool(), deltaMin, deltaMax, this);
                damage.IgnoreBoost = true;
                target.InflictDamage(damage);
            }
        }
        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageEarthPerHpPercent:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_DamageFirePerHpPercent:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_DamageWaterPerHpPercent:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_DamageAirPerHpPercent:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_DamageNeutralPerHpPercent:
                    return EffectElementEnum.Neutral;
            }
            return EffectElementEnum.Undefined;
        }
    }
}
