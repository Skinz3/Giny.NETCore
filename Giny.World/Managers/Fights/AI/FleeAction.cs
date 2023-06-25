using Giny.Core.Extensions;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class FleeAction : AIAction
    {
        public FleeAction(AIFighter fighter) : base(fighter)
        {
        }

        protected override void Apply()
        {
            if (Fighter.IsTackled() || Fighter.Stats.LifePercentage > 20)
            {
                return;
            }
            var mp = Fighter.Stats.MovementPoints.TotalInContext();

            if (mp > 0)
            {
                var points = MapPoint.GetOrthogonalGridReference().Where(x => x.DistanceTo(Fighter.Cell.Point) == mp); // erf

                var target = points.Random();

                var path = Fighter.FindPath(target);

                Fighter.Move(path);
            }

        }
    }
}