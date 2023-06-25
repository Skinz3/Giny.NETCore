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
    public class Cross : Zone
    {
        public bool Diagonal
        {
            get;
            set;
        }
        public List<DirectionsEnum> DisabledDirections
        {
            get;
            set;
        }
        public bool OnlyPerpendicular
        {
            get;
            set;
        }
        public bool AllDirections
        {
            get;
            set;
        }
        public uint Surface
        {
            get
            {
                return (uint)(this.Radius * 4 + 1);
            }
        }


        public Cross(byte minRadius, byte radius)
        {
            this.MinRadius = minRadius;
            this.Radius = radius;
            this.DisabledDirections = new List<DirectionsEnum>();
        }
        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            List<CellRecord> list = new List<CellRecord>();
            if (this.MinRadius == 0)
            {
                list.Add(centerCell);
            }
            List<DirectionsEnum> list2 = this.DisabledDirections.ToList();
            if (this.OnlyPerpendicular)
            {
                switch (this.Direction)
                {
                    case DirectionsEnum.DIRECTION_EAST:
                    case DirectionsEnum.DIRECTION_WEST:
                        list2.Add(DirectionsEnum.DIRECTION_EAST);
                        list2.Add(DirectionsEnum.DIRECTION_WEST);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        list2.Add(DirectionsEnum.DIRECTION_SOUTH_EAST);
                        list2.Add(DirectionsEnum.DIRECTION_NORTH_WEST);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH:
                    case DirectionsEnum.DIRECTION_NORTH:
                        list2.Add(DirectionsEnum.DIRECTION_SOUTH);
                        list2.Add(DirectionsEnum.DIRECTION_NORTH);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                        list2.Add(DirectionsEnum.DIRECTION_NORTH_EAST);
                        list2.Add(DirectionsEnum.DIRECTION_SOUTH_WEST);
                        break;
                }
            }
            for (int i = (int)this.Radius; i > 0; i--)
            {
                if (i >= (int)this.MinRadius)
                {
                    if (!this.Diagonal)
                    {
                        if (!list2.Contains(DirectionsEnum.DIRECTION_SOUTH_EAST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X + i, centerCell.Point.Y, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_NORTH_WEST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X - i, centerCell.Point.Y, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_NORTH_EAST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X, centerCell.Point.Y + i, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_SOUTH_WEST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X, centerCell.Point.Y - i, map, list);
                        }
                    }
                    if (this.Diagonal || this.AllDirections)
                    {
                        if (!list2.Contains(DirectionsEnum.DIRECTION_SOUTH))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X + i, centerCell.Point.Y - i, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_NORTH))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X - i, centerCell.Point.Y + i, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_EAST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X + i, centerCell.Point.Y + i, map, list);
                        }
                        if (!list2.Contains(DirectionsEnum.DIRECTION_WEST))
                        {
                            MapPoint.AddCellIfValid(centerCell.Point.X - i, centerCell.Point.Y - i, map, list);
                        }
                    }
                }
            }
            return list.ToArray();
        }


    }
}
