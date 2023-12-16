using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Other
{
    [SpellEffectHandler(EffectsEnum.Effect_EndTurn)]
    public class EndTurn : SpellEffectHandler
    {
        public EndTurn(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var target = targets.FirstOrDefault();

            if (!target.IsFighterTurn)
            {
                return;
            }

            CastHandler.Cast.PassTurn = true;
        }
    }
}
