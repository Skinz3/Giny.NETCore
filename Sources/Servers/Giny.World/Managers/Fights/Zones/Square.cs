using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;

namespace Giny.World.Managers.Fights.Zones
{
    public class Square : Zone
    {
        public Square(byte minRadius, byte radius)
        {
            MinRadius = minRadius;
            Radius = radius;
        }

        public bool DiagonalFree
        {
            get;
            set;
        }

        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            var centerPoint = new MapPoint(centerCell.Id);
            var result = new List<CellRecord>();

            if (Radius == 0)
            {
                if (MinRadius == 0 && !DiagonalFree)
                    result.Add(centerCell);

                return result.ToArray();
            }

            int x = (int)(centerPoint.X - Radius);
            int y;
            while (x <= centerPoint.X + Radius)
            {
                y = (int)(centerPoint.Y - Radius);
                while (y <= centerPoint.Y + Radius)
                {
                    if (MinRadius == 0 || Math.Abs(centerPoint.X - x) + Math.Abs(centerPoint.Y - y) >= MinRadius)
                        if (!DiagonalFree || Math.Abs(centerPoint.X - x) != Math.Abs(centerPoint.Y - y))
                            AddCellIfValid(x, y, map, result);

                    y++;
                }

                x++;
            }

            return result.ToArray();
        }

        private static void AddCellIfValid(int x, int y, MapRecord map, IList<CellRecord> container)
        {
            if (!MapPoint.IsInMap(x, y))
                return;

            container.Add(map.Cells[MapPoint.CoordToCellId(x, y)]);
        }
    }
}
