using Giny.Core.DesignPattern;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Npcs
{
    [Table("npcspawns")]
    public class NpcSpawnRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, NpcSpawnRecord> NpcSpawns = new ConcurrentDictionary<long, NpcSpawnRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        [Update]
        public short TemplateId
        {
            get;
            set;
        }

        [Update]
        public int MapId
        {
            get;
            set;
        }

        [Update]
        public short CellId
        {
            get;
            set;
        }

        [Update]
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        [Ignore]
        public NpcRecord Template
        {
            get;
            set;
        }
        [Ignore]
        public List<NpcActionRecord> Actions
        {
            get;
            set;
        }
        public static IEnumerable<NpcSpawnRecord> GetNpcSpawns()
        {
            return NpcSpawns.Values;
        }
        public static IEnumerable<NpcSpawnRecord> GetNpcsOnMap(int mapId)
        {
            return NpcSpawns.Values.Where(x => x.MapId == mapId);
        }
        public static int GetMapId(int npcSpawnId)
        {
            return NpcSpawns[npcSpawnId].MapId;
        }
        [StartupInvoke("Npcs Bidings", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var npc in NpcSpawns)
            {
                npc.Value.Template = NpcRecord.GetNpcRecord(npc.Value.TemplateId);
                npc.Value.Actions = NpcActionRecord.GetNpcActions(npc.Key).ToList();
            }
        }

        public static long PopNextId()
        {
            if (NpcSpawns.Count == 0)
            {
                return 1;
            }
            return NpcSpawns.Keys.OrderByDescending(x => x).First() + 1;
        }
        public static NpcSpawnRecord GetNpcSpawnRecord(long spawnId)
        {
            return NpcSpawns[spawnId];
        }

        public override string ToString()
        {
            return "(Id : " + Id + ") " + Template.Name + " (MapId : " + MapId + ")";
        }
    }
}
