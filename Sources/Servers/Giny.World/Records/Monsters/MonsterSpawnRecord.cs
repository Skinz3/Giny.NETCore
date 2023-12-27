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
    [Table("monster_spawns")]
    public class MonsterSpawnRecord : IRecord
    {
        [Container]
        private static Dictionary<long, MonsterSpawnRecord> MonsterSpawns = new Dictionary<long, MonsterSpawnRecord>();

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
        public static List<MonsterSpawnRecord> GetMonsterSpawnRecords(long subareaId)
        {
            return MonsterSpawns.Values.Where(x => x.SubareaId == subareaId).ToList();
        }

        public static List<MonsterSpawnRecord> GetMonsterSpawnRecords()
        {
            return MonsterSpawns.Values.ToList();
        }
    }
}
