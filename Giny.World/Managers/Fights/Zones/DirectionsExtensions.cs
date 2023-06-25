using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones
{
    public static class DirectionsExtensions
    {
        public static DirectionsEnum GetOpposedDirection(this DirectionsEnum direction) => (DirectionsEnum)(((int)direction + 4) % 8);

        public static bool IsDiagonal(this DirectionsEnum direction) => ((int)direction) % 2 == 0;

        public static DirectionsEnum[] GetDiagonalDecomposition(this DirectionsEnum direction)
        {
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    return new[] { DirectionsEnum.DIRECTION_NORTH_EAST, DirectionsEnum.DIRECTION_SOUTH_EAST };
                case DirectionsEnum.DIRECTION_WEST:
                    return new[] { DirectionsEnum.DIRECTION_NORTH_WEST, DirectionsEnum.DIRECTION_SOUTH_WEST };
                case DirectionsEnum.DIRECTION_NORTH:
                    return new[] { DirectionsEnum.DIRECTION_NORTH_WEST, DirectionsEnum.DIRECTION_NORTH_EAST };
                case DirectionsEnum.DIRECTION_SOUTH:
                    return new[] { DirectionsEnum.DIRECTION_SOUTH_WEST, DirectionsEnum.DIRECTION_SOUTH_EAST };
                default:
                    return new[] { direction };
            }
        }
    }
}
