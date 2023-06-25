using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_TeleportPortal)]
    public class TeleportPortal : SpellEffectHandler
    {
        public TeleportPortal(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (this.Source.Team.GetPortals().Where(x => x.Active).Count() < 2)
            {
                return;
            }

            foreach (var target in targets)
            {
                target.TeleportToPortal(Source);
            }
        }
    }
}
