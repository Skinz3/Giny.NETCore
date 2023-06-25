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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerHPEroded)]
    public class DamagePerHpEroded : SpellEffectHandler
    {
        public DamagePerHpEroded(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short damagesAmount = (short)(target.Stats.ErodedLife * Effect.Min / 100d);
                Damage damages = new Damage(Source, target, GetEffectSchool(), damagesAmount, damagesAmount, this);
                damages.IgnoreBoost = true;
                target.InflictDamage(damages);
            }
        }
        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerHPEroded:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageWaterPerHPEroded:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageFirePerHPEroded:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageNeutralPerHPEroded:
                    return EffectSchoolEnum.Neutral;
                case EffectsEnum.Effect_DamageEarthPerHPEroded:
                    return EffectSchoolEnum.Earth;
            }

            return EffectSchoolEnum.Unknown;
        }
    }
}
