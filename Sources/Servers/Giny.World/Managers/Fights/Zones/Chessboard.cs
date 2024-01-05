using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones
{
    public class Chessboard : Zone
    {
        public Chessboard(byte radius)
        {
            this.Radius = radius;
        }
        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {

            var centerPoint = new MapPoint(centerCell.Id);
            var result = new List<CellRecord>();

            bool inverted = Radius % 2 == 0;

            if (Radius == 0)
            {
                if (MinRadius == 0)
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
                    bool even = (x % 2 != 0 && y % 2 == 0) || (x % 2 == 0 && y % 2 != 0);

                    if (inverted)
                    {
                        even = !even;
                    }
                    if (even)
                    {
                        MapPoint.AddCellIfValid(x, y, map, result);
                    }


                    y++;
                }

                x++;
            }

            return result.ToArray();
        }


    }
}
