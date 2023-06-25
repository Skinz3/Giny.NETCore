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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerCasterHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerCasterHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerCasterHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerCasterHPEroded)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerCasterHPEroded)]
    public class DamagePerCasterHpEroded : SpellEffectHandler
    {
        public DamagePerCasterHpEroded(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short damagesAmount = (short)(Source.Stats.ErodedLife * Effect.Min / 100d);
                Damage damages = new Damage(Source, target, GetEffectSchool(), damagesAmount, damagesAmount, this);
                damages.IgnoreBoost = true;
                damages.IgnoreResistances = true; // good
                target.InflictDamage(damages);
            }
        }
        private EffectSchoolEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerCasterHPEroded:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageWaterPerCasterHPEroded:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageFirePerCasterHPEroded:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageNeutralPerCasterHPEroded:
                    return EffectSchoolEnum.Neutral;
                case EffectsEnum.Effect_DamageEarthPerCasterHPEroded:
                    return EffectSchoolEnum.Earth;
            }

            return EffectSchoolEnum.Unknown;
        }
    }
}
