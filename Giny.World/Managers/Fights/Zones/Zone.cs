using System.Linq;
using System;
using Giny.Protocol.Custom.Enums;
using Giny.World.Records.Maps;
using Giny.World.Managers.Fights.Cast;
using System.Collections;
using System.Collections.Generic;

namespace Giny.World.Managers.Fights.Zones
{
    public abstract class Zone
    {
        public ZoneEnum ZoneType
        {
            get;
            private set;
        }

        public void Initialize(ZoneEnum zoneType, DirectionsEnum direction)
        {
            ZoneType = zoneType;
            Direction = direction;
            EfficiencyMalus = EfficiencyMalus > 0 ? EfficiencyMalus : ZoneManager.EFFECTSHAPE_DEFAULT_EFFICIENCY;
            MaxEfficiency = MaxEfficiency > 0 ? MaxEfficiency : ZoneManager.EFFECTSHAPE_DEFAULT_MAX_EFFICIENCY_APPLY;
        }

        protected byte MinRadius
        {
            get;
            set;
        }

        protected int EfficiencyMalus
        {
            get;
            set;
        }

        protected int MaxEfficiency
        {
            get;
            set;
        }

        protected DirectionsEnum Direction
        {
            get;
            set;
        }

        protected byte Radius
        {
            get;
            set;
        }

        public abstract CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map);

        public double GetShapeEfficiency(CellRecord targetCell, CellRecord impactCell)
        {
            if (Radius >= ZoneManager.UNLIMITED_ZONE_SIZE)
                return 1.0;

            short distance = 0;

            switch (ZoneType)
            {
                case ZoneEnum.A:
                case ZoneEnum.a:
                case ZoneEnum.Z:
                case ZoneEnum.I:
                case ZoneEnum.O:
                case ZoneEnum.semicolon:
                case ZoneEnum.empty:
                case ZoneEnum.P:
                    return 1.0;
                case ZoneEnum.B:
                case ZoneEnum.V:
                case ZoneEnum.G:
                case ZoneEnum.W:
                    distance = targetCell.Point.SquareDistanceTo(impactCell.Point);
                    break;
                case ZoneEnum.minus:
                case ZoneEnum.plus:
                case ZoneEnum.U:
                    distance = (short)(targetCell.Point.ManhattanDistanceTo(impactCell.Point) / 2);
                    break;
                default:
                    distance = targetCell.Point.ManhattanDistanceTo(impactCell.Point);
                    break;
            }

            if (distance > Radius)
                return 1.0;

            if (MinRadius > 0)
            {
                if (distance <= MinRadius)
                    return 1.0;

                return Math.Max(0d, 1 - 0.01 * Math.Min(distance - MinRadius, MaxEfficiency) * EfficiencyMalus);
            }

            return Math.Max(0d, 1 - 0.01 * Math.Min(distance, MaxEfficiency) * EfficiencyMalus);
        }
    }
}
