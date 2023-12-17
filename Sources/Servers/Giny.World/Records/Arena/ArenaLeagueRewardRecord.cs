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
    [Table("arena_leagues_rewards")]
    public class ArenaLeagueRewardRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, ArenaLeagueRewardRecord> LeagueRewards = new ConcurrentDictionary<long, ArenaLeagueRewardRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public int RewardId
        {
            get;
            set;
        }

        public uint SeasonId
        {
            get;
            set;
        }

        public uint LeagueId
        {
            get;
            set;
        }

        public uint TitlesRewards
        {
            get;
            set;
        }

        public bool EndSeasonRewards
        {
            get;
            set;
        }
    }
}
