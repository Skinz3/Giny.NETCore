using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Movements;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_RepelsTo)]
    public class RepelsTo : SpellEffectHandler
    {
        public RepelsTo(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            DirectionsEnum orientation = CastCell.Point.OrientationTo(TargetCell.Point, true);

            MapPoint adjPoint = CastCell.Point.GetCellInDirection(orientation, 1);

            var delta = adjPoint.DistanceTo(TargetCell.Point);

            Fighter target = Source.Fight.GetFighter(adjPoint.CellId);

            if (target != null)
            {
                target.Slide(Source, orientation, delta, MovementType.Pull);
            }
        }
    }
}
