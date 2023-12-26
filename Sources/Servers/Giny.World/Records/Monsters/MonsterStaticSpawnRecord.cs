using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Monsters
{
    [Table("monster_static_spawns")]
    public class MonsterStaticSpawnRecord : IRecord
    {
        [Container]
        private static Dictionary<long, MonsterStaticSpawnRecord> MonsterStaticSpawns = new Dictionary<long, MonsterStaticSpawnRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }
        public List<short> Monsters
        {
            get;
            set;
        }

        public List<byte> Grades
        {
            get;
            set;
        }
        public long MapId
        {
            get;
            set;
        }

        public short CellId
        {
            get;
            set;
        }
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        public short RespawnDelay
        {
            get;
            set;
        }

        public static List<MonsterStaticSpawnRecord> GetStaticSpawns()
        {
            return MonsterStaticSpawns.Values.ToList();
        }
    }
}
