using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones
{
    public class Line : Zone
    {
        private bool FromCaster
        {
            get;
            set;
        }
        private bool StopAtTarget
        {
            get;
            set;
        }
        public Line(byte minRadius, byte radius, bool fromCaster, bool stopAtTarget)
        {
            Radius = radius;
            MinRadius = minRadius;
            FromCaster = fromCaster;
            StopAtTarget = stopAtTarget;
        }

        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            short distance = 0;
            List<CellRecord> aCells = new List<CellRecord>();
            MapPoint origin = !FromCaster ? centerCell.Point : casterCell.Point;
            int x = origin.X;
            int y = origin.Y;

            int length = !this.FromCaster ? Radius : this.Radius + this.MinRadius - 1;

            if (FromCaster && this.StopAtTarget)
            {
                distance = origin.DistanceTo(centerCell.Point); // distanceToCell
                length = distance < length ? distance : length;
            }
            for (int r = this.MinRadius; r <= length; r++)
            {
                switch (this.Direction)
                {
                    case DirectionsEnum.DIRECTION_WEST:
                        AddCellIfValid(x - r, y - r, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH:
                        AddCellIfValid(x - r, y + r, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_EAST:
                        AddCellIfValid(x + r, y + r, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH:
                        AddCellIfValid(x + r, y - r, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        AddCellIfValid(x - r, y, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                        AddCellIfValid(x, y - r, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                        AddCellIfValid(x + r, y, map, aCells);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                        AddCellIfValid(x, y + r, map, aCells);
                        break;
                }
            }
            return aCells.ToArray();
        }

        private static void AddCellIfValid(int x, int y, MapRecord map, IList<CellRecord> container)
        {
            if (!MapPoint.IsInMap(x, y))
                return;

            container.Add(map.Cells[MapPoint.CoordToCellId(x, y)]);
        }
    }
}
