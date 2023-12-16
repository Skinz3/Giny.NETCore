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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerMP)]
    public class DamagePerMpUsed : SpellEffectHandler
    {
        public DamagePerMpUsed(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (target.Stats.MovementPoints.Used > 0)
                {
                    double delta = Effect.Value * target.Stats.MovementPoints.Used;

                    Damage damage = new Damage(Source, target, GetEffectSchool(), delta, delta, this);

                    target.InflictDamage(damage);
                }

            }
        }
        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerMP:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_DamageEarthPerMP:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_DamageFirePerMP:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_DamageWaterPerMP:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_DamageNeutralPerMP:
                    return EffectElementEnum.Neutral;
            }
            return EffectElementEnum.Undefined;
        }
    }
}
