using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Monsters
{
    [Table("monsterspawns")]
    public class MonsterSpawnRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, MonsterSpawnRecord> MonsterSpawns = new ConcurrentDictionary<long, MonsterSpawnRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }
        public short MonsterId
        {
            get;
            set;
        }
        public short SubareaId
        {
            get;
            set;
        }
        public double Probability
        {
            get;
            set;
        }
        public static int Count()
        {
            return MonsterSpawns.Count;
        }
        public static IEnumerable<MonsterSpawnRecord> GetMonsterSpawnRecords(long subareaId)
        {
            return MonsterSpawns.Values.Where(x => x.SubareaId == subareaId);
        }
        public static IEnumerable<MonsterSpawnRecord> GetMonsterSpawnRecords()
        {
            return MonsterSpawns.Values;
        }
    }
}
