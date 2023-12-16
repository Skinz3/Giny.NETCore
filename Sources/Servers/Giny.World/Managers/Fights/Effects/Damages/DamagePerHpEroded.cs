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
                double damagesAmount = target.Stats.Life.Eroded * Effect.Min / 100d;
                Damage damages = new Damage(Source, target, GetEffectSchool(), damagesAmount, damagesAmount, this);
                damages.IgnoreBoost = true;
                target.InflictDamage(damages);
            }
        }
        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerHPEroded:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_DamageWaterPerHPEroded:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_DamageFirePerHPEroded:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_DamageNeutralPerHPEroded:
                    return EffectElementEnum.Neutral;
                case EffectsEnum.Effect_DamageEarthPerHPEroded:
                    return EffectElementEnum.Earth;
            }

            return EffectElementEnum.Undefined;
        }
    }
}
