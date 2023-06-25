using Giny.Core.DesignPattern;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [Table("dungeons")]
    public class DungeonRecord : ITable
    {
        [Container]
        private static Dictionary<long, DungeonRecord> Dungeons = new Dictionary<long, DungeonRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }
        [Update]
        public string Name
        {
            get;
            set;
        }
        [Update]
        public long EntranceMapId
        {
            get;
            set;
        }
        [Update]
        public long ExitMapId
        {
            get;
            set;
        }

        [ProtoSerialize]
        [Update]
        public Dictionary<long, MonsterRoom> Rooms
        {
            get;
            set;
        }

        public long? GetNextMapId(long currentMapId)
        {
            int index = 0;

            foreach (var mapId in Rooms.Keys)
            {
                if (mapId == currentMapId)
                {
                    if (index >= Rooms.Keys.Count-1)
                    {
                        return ExitMapId;
                    }
                    return Rooms.Keys.ElementAt(index + 1);
                }

                index++;

            }


            return null;
        }

        public static IEnumerable<DungeonRecord> GetDungeonRecords()
        {
            return Dungeons.Values;
        }

        public static DungeonRecord GetDungeonRecord(long mapId)
        {
            return Dungeons.Values.FirstOrDefault(x => x.Rooms.ContainsKey(mapId));
        }
        public static bool IsDungeonEntrance(long id)
        {
            return Dungeons.Values.Any(x => x.EntranceMapId == id);
        }
        public override string ToString()
        {
            return "{" + Id + "} " + Name;
        }

        
    }

    [ProtoContract]
    public class MonsterRoom
    {
        public const float DefaultRespawnDelay = 10f;

        [ProtoMember(1)]
        public List<short> MonsterIds
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public float RespawnDelay
        {
            get;
            set;
        }
        public MonsterRoom()
        {
            RespawnDelay = DefaultRespawnDelay;
            this.MonsterIds = new List<short>();
        }
        public MonsterRoom(float respawnDelay, params short[] monsters)
        {
            this.RespawnDelay = respawnDelay;
            this.MonsterIds = monsters.ToList();
        }

        public int GetRespawnInterval()
        {
            return (int)(RespawnDelay * 1000);
        }
    }
}
