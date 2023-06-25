using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [D2OClass("LivingObjectSkinJntMood")]
    [Table("livingobjects")]
    public class LivingObjectRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, LivingObjectRecord> LivingObjects = new ConcurrentDictionary<long, LivingObjectRecord>();

        [Primary]
        [D2OField("skinId")]
        public long Id
        {
            get;
            set;
        }
        [Update]
        public ItemTypeEnum Type
        {
            get;
            set;
        }
        [Update]
        public List<short> SkinIds
        {
            get;
            set;
        }
        [Update]
        public short MaximumLevel
        {
            get;
            set;
        }
        [Ignore]
        public short MaximumExp
        {
            get;
            set;
        }
        [Ignore]
        public bool Skinnable
        {
            get
            {
                return SkinIds.Count > 0;
            }
        }

        [StartupInvoke(StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var livingObjectRecord in LivingObjects)
            {
                livingObjectRecord.Value.MaximumExp = CalculateMaximumExp(livingObjectRecord.Value.MaximumLevel);
            }
        }
        private static short CalculateMaximumExp(short maximumLevel)
        {
            short result = 0;
            for (int i = 0; i < maximumLevel - 1; i++)
            {
                result += (short)(10 + i);
            }
            return result;
        }
        public short GetSkin(int skinIndex)
        {
            return SkinIds[skinIndex - 1];
        }
        public static LivingObjectRecord GetLivingObjectRecord(short id)
        {
            return LivingObjects[id];
        }

        public static bool IsLivingObject(short gid)
        {
            return LivingObjects.ContainsKey(gid);
        }

        public static IEnumerable<LivingObjectRecord> GetLivingObjectRecords()
        {
            return LivingObjects.Values;
        }
    }
}
