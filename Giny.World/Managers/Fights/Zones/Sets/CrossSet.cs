using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones.Sets
{
    public class CrossSet : Set
    {
        public CrossSet(MapPoint center, short maxRange, short minRange = 0)
        {
            Center = center;
            MinRange = minRange;
            MaxRange = maxRange;
        }

        public MapPoint Center
        {
            get;
            set;
        }

        public short MinRange
        {
            get;
            set;
        }

        public short MaxRange
        {
            get;
            set;
        }

        public bool Diagonal
        {
            get;
            set;
        }

        public bool AllDirections
        {
            get;
            set;
        }

        public override IEnumerable<MapPoint> EnumerateSet()
        {
            for (short i = MinRange; i <= MaxRange; i++)
            {
                if (!Diagonal || AllDirections)
                {
                    MapPoint point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_NORTH_EAST, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH_EAST, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH_WEST, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_NORTH_WEST, i)) != null)
                        yield return point;
                }

                if (Diagonal || AllDirections)
                {
                    MapPoint point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_EAST, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_WEST, i)) != null)
                        yield return point;
                    if ((point = Center.GetCellInDirection(DirectionsEnum.DIRECTION_NORTH, i)) != null)
                        yield return point;
                }
            }
        }

        public override bool BelongToSet(MapPoint point)
        {
            var dist = point.ManhattanDistanceTo(Center);

            if (AllDirections)
                return (point.IsOnSameLine(Center) && dist >= MinRange && dist <= MaxRange) ||
                    (point.IsOnSameDiagonal(Center) && dist / 2 >= MinRange && dist / 2 <= MaxRange);

            if (Diagonal) // dist/2 because we mesaure distances in diagonal
                return point.IsOnSameDiagonal(Center) && dist / 2 >= MinRange && dist / 2 <= MaxRange;

            return point.IsOnSameLine(Center) && dist >= MinRange && dist <= MaxRange;
        }
    }
}
