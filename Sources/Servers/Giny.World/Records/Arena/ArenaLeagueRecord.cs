using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Records.Characters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Arena
{
    [Table("arena_leagues")]
    public class ArenaLeagueRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, ArenaLeagueRecord> Leagues = new ConcurrentDictionary<long, ArenaLeagueRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public int LeagueId
        {
            get;
            set;
        }

        public uint NameId
        {
            get;
            set;
        }
        public uint OrnamentId
        {
            get;
            set;
        }
        public string Icon
        {
            get;
            set;
        }
        public string Illus
        {
            get;
            set;
        }
        public bool IsLastLeague
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int TypeId
        {
            get;
            set;
        }

        public int NextLeagueId
        {
            get;
            set;
        }

        [Ignore]
        public LeaguesEnum Type
        {
            get { return (LeaguesEnum)TypeId; }
        }

        [Ignore]
        public int MinRequiredRank
        {
            get;
            set;
        }

        [Ignore]
        public int MaxRequiredRank
        {
            get;
            set;
        }

    }
}
