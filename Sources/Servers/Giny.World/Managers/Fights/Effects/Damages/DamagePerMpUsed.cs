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
                if (target.Stats.MpUsed > 0)
                {
                    short delta = (short)(Effect.Value * target.Stats.MpUsed);

                    Damage damage = new Damage(Source, target, GetEffectSchool(), delta, delta, this);

                    target.InflictDamage(damage);
                }

            }
        }
        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerMP:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageEarthPerMP:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_DamageFirePerMP:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageWaterPerMP:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageNeutralPerMP:
                    return EffectSchoolEnum.Neutral;
            }
            return EffectSchoolEnum.Unknown;
        }
    }
}
