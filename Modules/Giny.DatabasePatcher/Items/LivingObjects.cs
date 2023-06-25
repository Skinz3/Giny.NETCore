using Giny.Core;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Items
{
    public class LivingObjects
    {
        static Dictionary<int, short> LivingObjectsMaxiumLevels = new Dictionary<int, short>()
        {
            { 9233  ,  20 },
            { 9234 ,   20 },
            { 9255   , 20 },
            { 9256  ,  20 },
            { 12424 ,  20 },
            { 12425  , 20 },
            { 12426  , 20 },
            { 12427  , 20 },
            { 13210  , 20 },
            { 13211  , 20 },
            { 13212  , 20 },
            { 13213  , 20 },
            { 18154  , 20 },
            { 18155  , 20 },
            { 19524  , 10 },
            { 19525  , 10 },
            { 19526  , 10 },
        };
        static Dictionary<int, short[]> LivingObjectsSkins = new Dictionary<int, short[]>()
        {
            { 9233,new short[] { 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135 } },
            { 9234,new short[] { 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1151, 1152, 1153, 1154, 1155 } },
            { 9255,new short[] {  } },
            { 9256,new short[] {  } },
            { 12424,new short[] { 1490, 1491, 1492, 1493, 1494, 1495, 1496, 1497, 1498, 1499, 1500, 1501, 1502, 1503, 1504, 1505, 1506, 1507, 1508, 1509 } },
            { 12425,new short[] { 1470, 1471, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 1479, 1480, 1481, 1482, 1483, 1484, 1485, 1486, 1487, 1488, 1489 } },
            { 12426,new short[] {  } },
            { 12427,new short[] {  } },
            { 13210,new short[] {  } },
            { 13211,new short[] { 1688, 1689, 1690, 1691, 1692, 1693, 1694, 1695, 1696, 1697, 1698, 1699, 1700, 1701, 1702, 1703, 1704, 1705, 1706, 1707 } },
            { 13212,new short[] {  } },
            { 13213,new short[] { 1708, 1709, 1710, 1711, 1712, 1713, 1714, 1715, 1716, 1717, 1718, 1719, 1720, 1721, 1722, 1723, 1724, 1725, 1726, 1727 } },
            { 18154,new short[] { 3410, 3411, 3412, 3413, 3414, 3415, 3416, 3417, 3418, 3419, 3430, 3431, 3432, 3433, 3434, 3435, 3436, 3437, 3438, 3439 } },
            { 18155,new short[] { 3420, 3421, 3422, 3423, 3424, 3425, 3426, 3427, 3428, 3429, 3440, 3441, 3442, 3443, 3444, 3445, 3446, 3447, 3448, 3449 } },
            { 19524,new short[]{  3667 , 3668, 3669, 3670, 3671, 3672, 3673, 3674, 3675, 3676 } },
            { 19525,new short[]{  3677, 3678, 3679, 3680, 3681, 3682, 3683, 3684, 3685, 3686 } },
            { 19526,new short[]{  3657, 3658, 3659, 3660, 3661, 3662, 3663, 3664, 3665, 3666 } }
        };
        static Dictionary<int, int> LivingObjectsTypes = new Dictionary<int, int>()
        {
            { 9233  , 17 },
            { 9234 ,   16 },
            { 9255   , 1 },
            { 9256  ,  9 },
            { 12424 ,  16 },
            { 12425  , 17 },
            { 12426  , 10 },
            { 12427  , 11 },
            { 13210  , 11 },
            { 13211  , 17 },
            { 13212  , 10 },
            { 13213  , 16 },
            { 18154  , 16 },
            { 18155  , 17 },
            { 19524,   17 },
            { 19525, 82 },
            { 19526 , 16 },
        };

        public static void Patch()
        {
            Logger.Write("Fixing living objects ...");

            foreach (var record in LivingObjectRecord.GetLivingObjectRecords())
            {
                if (LivingObjectsSkins.ContainsKey((int)record.Id))
                {
                    record.SkinIds = LivingObjectsSkins[(int)record.Id].ToList();
                }
                if (LivingObjectsTypes.ContainsKey((int)record.Id))
                {
                    record.Type = (ItemTypeEnum)LivingObjectsTypes[(int)record.Id];
                }
                if (LivingObjectsMaxiumLevels.ContainsKey((int)record.Id))
                {
                    record.MaximumLevel = LivingObjectsMaxiumLevels[(int)record.Id];
                }

                record.UpdateInstantElement();
            }
        }

    }
}
