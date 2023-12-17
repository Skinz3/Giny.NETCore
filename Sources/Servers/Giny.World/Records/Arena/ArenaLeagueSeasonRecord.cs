using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Arena
{

    [Table("arena_leagues_seasons")]
    public class ArenaLeagueSeasonRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, ArenaLeagueSeasonRecord> LeagueSeasons = new ConcurrentDictionary<long, ArenaLeagueSeasonRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public int SeasonId
        {
            get;
            set;
        }

        public uint NameId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
