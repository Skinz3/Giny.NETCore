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
    public class Lozenge : Zone
    {
        public Lozenge(byte minRadius, byte radius) 
        {
            MinRadius = minRadius;
            Radius = radius;
        }
        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            var result = new List<CellRecord>();

            if (Radius == 0)
            {
                if (MinRadius == 0)
                    result.Add(centerCell);

                return result.ToArray();
            }

            int x = (int)(centerCell.Point.X - Radius);
            int y = 0;
            int i = 0;
            int j = 1;
            while (x <= centerCell.Point.X + Radius)
            {
                y = -i;

                while (y <= i)
                {
                    if (MinRadius == 0 || Math.Abs(centerCell.Point.X - x) + Math.Abs(y) >= MinRadius)
                        AddCellIfValid(x, y + centerCell.Point.Y, map, result);

                    y++;
                }

                if (i == Radius)
                {
                    j = -j;
                }

                i = i + j;
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
