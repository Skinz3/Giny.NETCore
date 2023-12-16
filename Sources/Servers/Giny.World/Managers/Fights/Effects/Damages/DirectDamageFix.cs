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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirFix)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterFix)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFireFix)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthFix)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralFix)]
    public class DirectDamageFix : SpellEffectHandler
    {
        public DirectDamageFix(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var damages = new Damage(Source, target, GetEffectElement(), (short)Effect.Min, (short)Effect.Min, this,true);
                target.InflictDamage(damages);
            }
        }

        private EffectElementEnum GetEffectElement()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirFix:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_DamageEarthFix:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_DamageFireFix:
                    return EffectElementEnum.Fire;
                case EffectsEnum.Effect_DamageNeutralFix:
                    return EffectElementEnum.Neutral;
                case EffectsEnum.Effect_DamageWaterFix:
                    return EffectElementEnum.Water;
            }

            throw new NotImplementedException("Unable to retreive effect element for effect " + Effect.EffectEnum);
        }
    }
}
