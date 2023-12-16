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

namespace Giny.World.Managers.Fights.Effects.Heals
{
    [SpellEffectHandler(EffectsEnum.Effect_LifePointsWinFromEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_LifePointsWinFromWater)]
    [SpellEffectHandler(EffectsEnum.Effect_LifePointsWinFromAir)]
    [SpellEffectHandler(EffectsEnum.Effect_LifePointsWinFromNeutral)]
    public class LifePointsWinFromElement : SpellEffectHandler
    {
        public LifePointsWinFromElement(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                target.Heal(new Healing(Source, target, GetEffectSchool(), Effect.Min, Effect.Max, this));
            }
        }

        private EffectElementEnum GetEffectSchool()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_LifePointsWinFromEarth:
                    return EffectElementEnum.Earth;
                case EffectsEnum.Effect_LifePointsWinFromWater:
                    return EffectElementEnum.Water;
                case EffectsEnum.Effect_LifePointsWinFromAir:
                    return EffectElementEnum.Air;
                case EffectsEnum.Effect_LifePointsWinFromNeutral:
                    return EffectElementEnum.Neutral;
            }

            throw new InvalidOperationException("Unable to compute effect school from effect " + Effect.EffectEnum);
        }
    }
}
