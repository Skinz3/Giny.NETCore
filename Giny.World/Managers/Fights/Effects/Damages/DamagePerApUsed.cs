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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerAP)]
    public class DamagePerApUsed : SpellEffectHandler
    {
        public DamagePerApUsed(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (target.Stats.ApUsed > 0)
                {
                    short delta = (short)(Effect.Max * (target.Stats.ApUsed/(double)Effect.Min));

                    Damage damage = new Damage(Source, target, GetEffectSchool(), delta, delta, this);

                    target.InflictDamage(damage);
                }

            }
        }
        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerAP:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageEarthPerAP:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_DamageFirePerAP:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageWaterPerAP:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageNeutralPerAP:
                    return EffectSchoolEnum.Neutral;
            }
            return EffectSchoolEnum.Unknown;
        }
    }
}
