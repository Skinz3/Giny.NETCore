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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerHPLost)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerHPLost)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerHPLost)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerHPLost)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerHPLost)]
    public class DamagePerHpLost : SpellEffectHandler
    {
        public DamagePerHpLost(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short damagesAmount = (short)(Source.Stats.MissingLife * Effect.Min / 100d);
                Damage damages = new Damage(Source, target, GetEffectSchool(), damagesAmount, damagesAmount, this);
                damages.IgnoreBoost = true; // good
                target.InflictDamage(damages);
            }
        }

        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerHPLost:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageWaterPerHPLost:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageFirePerHPLost:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageNeutralPerHPLost:
                    return EffectSchoolEnum.Neutral;
                case EffectsEnum.Effect_DamageEarthPerHPLost:
                    return EffectSchoolEnum.Earth;
            }

            return EffectSchoolEnum.Unknown;
        }
    }
}
