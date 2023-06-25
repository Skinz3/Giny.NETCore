using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones
{
    [WIP("add weapon functions (weaponManager)")]
    public class ZoneManager : Singleton<ZoneManager>
    {
        public const int EFFECTSHAPE_DEFAULT_EFFICIENCY = 10;
        public const int EFFECTSHAPE_DEFAULT_MAX_EFFICIENCY_APPLY = 4;

        public const int UNLIMITED_ZONE_SIZE = 50;

        public Zone BuildZone(string rawZone, DirectionsEnum direction)
        {
            ZoneEnum zoneShape = 0;
            byte zoneSize = 0;
            byte zoneMinSize = 0;

            if (string.IsNullOrEmpty(rawZone))
            {
                return null;
            }

            var shape = (ZoneEnum)rawZone[0];
            byte size = 0;
            byte minSize = 0;
            int zoneEfficiency = 0;
            int zoneMaxEfficiency = 0;
            int zoneEfficiencyPercent = 0;

            var data = rawZone.Remove(0, 1).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var hasMinSize = shape == ZoneEnum.C || shape == ZoneEnum.X || shape == ZoneEnum.Q || shape == ZoneEnum.plus || shape == ZoneEnum.sharp;

            if (shape != ZoneEnum.semicolon)
            {
                if (data.Length >= 4)
                {
                    size = byte.Parse(data[0]);
                    minSize = byte.Parse(data[1]);
                    zoneEfficiency = byte.Parse(data[2]);
                    zoneMaxEfficiency = byte.Parse(data[2]);
                }
                else
                {
                    if (data.Length >= 1)
                        size = byte.Parse(data[0]);

                    if (data.Length >= 2)
                        if (hasMinSize)
                            minSize = byte.Parse(data[1]);
                        else
                            zoneEfficiency = byte.Parse(data[1]);

                    if (data.Length >= 3)
                        if (hasMinSize)
                            zoneEfficiency = byte.Parse(data[2]);
                        else
                            zoneMaxEfficiency = byte.Parse(data[2]);
                }
            }


            zoneShape = shape;
            zoneSize = size;
            zoneMinSize = minSize;
            zoneEfficiencyPercent = zoneEfficiency;

            Zone result = null;

            switch (zoneShape)
            {
                case ZoneEnum.semicolon:
                    return new SemiColumn(data.Select(x => short.Parse(x)));
                case ZoneEnum.X:
                    result = new Cross(zoneMinSize, zoneSize);
                    break;
                case ZoneEnum.L:
                    result = new Line(zoneMinSize, zoneSize, false, false);
                    break;
                case ZoneEnum.l:

                    bool stopAtTarget = false;
                    if (data.Length >= 5)
                    {
                        stopAtTarget = byte.Parse(data[4]) == 1;
                    }
                    result = new Line(zoneSize, zoneMinSize, true, stopAtTarget);
                    break;
                case ZoneEnum.T:
                    result = new Cross(0, zoneSize)
                    {
                        OnlyPerpendicular = true
                    };
                    break;
                case ZoneEnum.D:
                    result = new Cross(0, zoneSize);
                    break;
                case ZoneEnum.C:
                    result = new Lozenge(zoneMinSize, zoneSize);
                    break;
                case ZoneEnum.I:
                    result = new Lozenge(zoneSize, 63);
                    break;
                case ZoneEnum.O:
                    result = new Lozenge(zoneSize, zoneSize);
                    break;
                case ZoneEnum.Q:
                    result = new Cross(zoneMinSize > 0 ? zoneMinSize : (byte)1, zoneSize);
                    break;
                case ZoneEnum.G:
                    result = new Square(0, zoneSize);
                    break;
                case ZoneEnum.V:
                    result = new Cone(0, zoneSize);
                    break;
                case ZoneEnum.W:
                    result = new Square(0, zoneSize)
                    {
                        DiagonalFree = true
                    };
                    break;
                case ZoneEnum.plus:
                    result = new Cross(0, zoneSize)
                    {
                        Diagonal = true
                    };
                    break;
                case ZoneEnum.sharp:
                    result = new Cross(zoneMinSize > 0 ? zoneMinSize : (byte)1, zoneSize)
                    {
                        Diagonal = true
                    };
                    break;
                case ZoneEnum.star:
                    result = new Cross(0, zoneSize)
                    {
                        AllDirections = true
                    };
                    break;
                case ZoneEnum.slash:
                    result = new Line(zoneMinSize, zoneSize, false, false);
                    break;
                case ZoneEnum.U:
                    result = new HalfLozenge(0, zoneSize);
                    break;
                case ZoneEnum.A:
                case ZoneEnum.a:
                    result = new Lozenge(0, 63);
                    break;
                case ZoneEnum.P:
                    result = new Single();
                    break;
                case ZoneEnum.minus:
                    result = new Cross(0, zoneSize)
                    {
                        Diagonal = true,
                        OnlyPerpendicular = true
                    };
                    break;
                default:
                    result = new Cross(zoneMinSize, zoneSize);
                    break;
            }
            result.Initialize(zoneShape, direction);
            return result;
        }
    }
}
