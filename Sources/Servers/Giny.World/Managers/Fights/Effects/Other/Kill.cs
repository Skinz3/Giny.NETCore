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
    [SpellEffectHandler(EffectsEnum.Effect_Kill)]
    public class Kill : SpellEffectHandler
    {
        public Kill(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                Source.Fight.Send(new Giny.Protocol.Messages.GameActionFightKillMessage()
                {
                    actionId = 0,
                    sourceId = Source.Id,
                    targetId = target.Id,
                });

                target.Die(Source);

            }

        

        }
    }
}
