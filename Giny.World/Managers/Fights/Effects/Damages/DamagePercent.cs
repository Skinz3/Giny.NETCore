using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentFire)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentWater)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentAir)]
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentNeutral)]
    public class DamagePercent : SpellEffectHandler
    {
        public DamagePercent(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override bool Reveals => true;

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = (short)((Effect.Min /100d) * Source.Stats.LifePoints);
                Damage damage = new Damage(Source, target, GetEffectSchool(), delta, delta, this);
                damage.IgnoreBoost = true; // good
                target.InflictDamage(damage);
            }
        }
        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamagePercentEarth:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_DamagePercentFire:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamagePercentWater:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamagePercentAir:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamagePercentNeutral:
                    return EffectSchoolEnum.Neutral;
            }
            return EffectSchoolEnum.Unknown;
        }
    }
}
