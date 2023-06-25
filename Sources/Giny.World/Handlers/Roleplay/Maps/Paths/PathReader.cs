using Giny.Core;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Maps.Paths
{
    public class PathReader
    {
        public static short ReadCell(short cell)
        {
            return (short)(cell & 4095);
        }
        public static sbyte GetDirection(int result)
        {
            return (sbyte)(result >> 12);
        }
        public static Dictionary<short, DirectionsEnum> ReturnDispatchedCells(IEnumerable<short> keys)
        {
            var cells = new Dictionary<short, DirectionsEnum>();

            foreach (var key in keys)
            {
                cells.Add((short)(key & 4095), (DirectionsEnum)(key >> 12));
            }
            return cells;
        }
        public static Dictionary<short, DirectionsEnum> FightMove(Dictionary<short, DirectionsEnum> cells)
        {
            var indexedCells = cells.Keys.ToList();
            var Cells = new Dictionary<short, DirectionsEnum>();

            for (var i = 0; i < cells.Count - 1; i++)
            {
                if (indexedCells[i] <= 0 || indexedCells[i] >= 559 || indexedCells[i + 1] <= 0 || indexedCells[i + 1] >= 559)
                    continue;

                var loc1 = new MapPoint(indexedCells[i]);
                var loc2 = new MapPoint(indexedCells[i + 1]);


                if (loc1.Y - loc2.Y == 0 && loc1.X - loc2.X < 0)
                {
                    for (var j = (short)(loc1.X + 1); j <= loc2.X; j++)
                    {
                        var celldata = new MapPoint(j, loc1.Y);
                        if (celldata != null)
                            Cells.Add(celldata.CellId, DirectionsEnum.DIRECTION_SOUTH_EAST);
                    }
                }
                else if (loc1.X - loc2.X == 0 && loc1.Y - loc2.Y > 0)
                {
                    for (short j = (short)(loc1.Y - 1); j >= loc2.Y; j--)
                    {
                        var celldata = new MapPoint(loc1.X, j);
                        if (celldata != null)
                            Cells.Add(celldata.CellId, DirectionsEnum.DIRECTION_SOUTH_WEST);
                    }
                }
                else if (loc1.X - loc2.X == 0 && loc1.Y - loc2.Y < 0)
                {
                    for (var j = (short)(loc1.Y + 1); j <= loc2.Y; j++)
                    {
                        var celldata = new MapPoint(loc1.X, j);
                        if (celldata != null)
                            Cells.Add(celldata.CellId, DirectionsEnum.DIRECTION_NORTH_EAST);
                    }
                }
                else if (loc1.Y - loc2.Y == 0 && loc1.X - loc2.X > 0)
                {
                    for (var j = (short)(loc1.X - 1); j >= loc2.X; j--)
                    {
                        var celldata = new MapPoint(j, loc1.Y);
                        if (celldata != null)
                            Cells.Add(celldata.CellId, DirectionsEnum.DIRECTION_NORTH_WEST);
                    }
                }
                else
                {
                    Logger.Write("Unable to read fight movements.");
                }
            }

            return Cells;
        }
        public static int GetCellXCoord(int cellid)
        {
            int num = 15;
            return checked(cellid - (num - 1) * GetCellYCoord(cellid)) / num;
        }
        public static int GetCellYCoord(int cellid)
        {
            int num = 15;
            checked
            {
                int num2 = cellid / (num * 2 - 1);
                int num3 = cellid - num2 * (num * 2 - 1);
                int num4 = num3 % num;
                return num2 - num4;
            }
        }
     
        public static short[] CompressPath(short[] path)
        {
            List<short> compressed = new List<short>();

            foreach (var cellId in path)
            {
                var point = new MapPoint(cellId);

            }

            return compressed.ToArray();
        }
    }
}
