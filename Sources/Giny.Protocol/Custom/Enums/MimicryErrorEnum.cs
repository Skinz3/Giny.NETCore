using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.Custom.Enums
{
    public enum MimicryErrorEnum
    {
        UNKNOWN = -1,
        DELETION_ERROR_UNSECURE_MODE = -2,
        IMPOSSIBLE_ACTION = -3,
        FOOD_TYPE = -7,
        FOOD_LEVEL = -8,
        NO_VALID_MIMICRY = -9,
        NO_VALID_HOST = -10,
        NO_VALID_FOOD = -11,
        NO_MIMICRY_ASSOCIATED = -16,
        SAME_SKIN = -17
    }
}
