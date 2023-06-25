using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones.Sets
{
    public class LozengeSet : Set
    {
        public LozengeSet(MapPoint center, int radius, int minRadius = 0)
        {
            Center = center;
            Radius = radius;
            MinRadius = minRadius;
        }

        public MapPoint Center
        {
            get;
            set;
        }

        public int Radius
        {
            get;
            set;
        }

        public int MinRadius
        {
            get;
            set;
        }

        public override IEnumerable<MapPoint> EnumerateSet()
        {
            for (int r = MinRadius; r <= Radius; r++)
            {
                // add all points that have a manhattan distance of r from the center
                if (r == 0)
                {
                    yield return Center;
                    continue;
                }

                // bottom right side
                for (int i = 0; i < r; i++)
                {
                    yield return new MapPoint(Center.X + (r - i), Center.Y - i);
                }

                //bottom left side
                for (int i = 0; i < r; i++)
                {
                    yield return new MapPoint(Center.X - i, Center.Y - (r - i));
                }

                // top left side
                for (int i = 0; i < r; i++)
                {
                    yield return new MapPoint(Center.X - (r - i), Center.Y + i);
                }

                // top right side
                for (int i = 0; i < r; i++)
                {
                    yield return new MapPoint(Center.X + i, Center.Y + (r - i));
                }
            }
        }

        public override bool BelongToSet(MapPoint point)
        {
            var dist = point.ManhattanDistanceTo(Center);
            return dist <= Radius && dist >= MinRadius;
        }
    }
}
