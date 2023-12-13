using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_DisablePortal)]
    public class DisablePortal : SpellEffectHandler
    {
        public DisablePortal(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var affectedPoints = GetAffectedCells().Select(x => x.Point);

            var portals = Source.GetMarks<Portal>().Where(x => affectedPoints.Contains(x.CenterCell.Point));

            foreach (var portal in portals)
            {
                portal.Disable();
            }
        }
    }
}
