using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Pool;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using Giny.World.Records.Maps;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Arena
{

    [Table("arena_maps")]
    public class ArenaMapRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, ArenaMapRecord> MapRecords = new ConcurrentDictionary<long, ArenaMapRecord>();

        public ArenaMapRecord(int mapId)
        {
            this.MapId = mapId;
        }

        public static List<ArenaMapRecord> ArenaMaps = new List<ArenaMapRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public int MapId;

        public static MapRecord GetArenaMap()
        {
            return MapRecord.GetMap(ArenaMaps.Random(new Random()).MapId);
        }
    }
}