using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Arena
{
    //to save inside character table field blob
    [ProtoContract]
    public class ArenaRankRecord
    {
        [ProtoMember(1)]  
        public ArenaRanking Rank
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public ArenaLeagueRanking BestRank
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public short VictoryCount
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public short FightCount
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public short NumFightNeededForLadder
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public DateTime ArenaPenalityDate
        {
            get;
            set;
        }

        /* daily part */
        [ProtoMember(7)]
        public ArenaLeagueRanking DailyBestRank
        {
            get;
            set;
        }

        [ProtoMember(8)]
        public short DailyVictoryCount
        {
            get;
            set;
        }

        [ProtoMember(9)]
        public short DailyFightCount
        {
            get;
            set;
        }

        [ProtoMember(10)]   
        public DateTime ArenaDailyDate
        {
            get;
            set;
        }


        public ArenaRankInfos GetArenaRankInfos()
        {
            return new ArenaRankInfos(Rank, BestRank, VictoryCount, FightCount, NumFightNeededForLadder);
        }

        public static ArenaRankRecord New()
        {
            return new ArenaRankRecord()
            {
                Rank = new ArenaRanking(),
                BestRank = new ArenaLeagueRanking(),
                VictoryCount = 0,
                FightCount = 0,
                NumFightNeededForLadder = 0,
                ArenaPenalityDate = new DateTime(),
                DailyBestRank = new ArenaLeagueRanking(),
                DailyVictoryCount = 0,
                DailyFightCount = 0,
                ArenaDailyDate = new DateTime(),

            };
        }
    }

}
