using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ArenaRankInfos  
    { 
        public const ushort Id = 9582;
        public virtual ushort TypeId => Id;

        public ArenaRanking ranking;
        public ArenaLeagueRanking leagueRanking;
        public short victoryCount;
        public short fightcount;
        public short numFightNeededForLadder;

        public ArenaRankInfos()
        {
        }
        public ArenaRankInfos(ArenaRanking ranking,ArenaLeagueRanking leagueRanking,short victoryCount,short fightcount,short numFightNeededForLadder)
        {
            this.ranking = ranking;
            this.leagueRanking = leagueRanking;
            this.victoryCount = victoryCount;
            this.fightcount = fightcount;
            this.numFightNeededForLadder = numFightNeededForLadder;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (ranking == null)
            {
                writer.WriteByte(0);
            }

            else
            {
                writer.WriteByte(1);
                ranking.Serialize(writer);
            }

            if (leagueRanking == null)
            {
                writer.WriteByte(0);
            }

            else
            {
                writer.WriteByte(1);
                leagueRanking.Serialize(writer);
            }

            if (victoryCount < 0)
            {
                throw new System.Exception("Forbidden value (" + victoryCount + ") on element victoryCount.");
            }

            writer.WriteVarShort((short)victoryCount);
            if (fightcount < 0)
            {
                throw new System.Exception("Forbidden value (" + fightcount + ") on element fightcount.");
            }

            writer.WriteVarShort((short)fightcount);
            if (numFightNeededForLadder < 0)
            {
                throw new System.Exception("Forbidden value (" + numFightNeededForLadder + ") on element numFightNeededForLadder.");
            }

            writer.WriteShort((short)numFightNeededForLadder);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            if (reader.ReadByte() == 0)
            {
                ranking = null;
            }

            else
            {
                ranking = new ArenaRanking();
                ranking.Deserialize(reader);
            }

            if (reader.ReadByte() == 0)
            {
                leagueRanking = null;
            }

            else
            {
                leagueRanking = new ArenaLeagueRanking();
                leagueRanking.Deserialize(reader);
            }

            victoryCount = (short)reader.ReadVarUhShort();
            if (victoryCount < 0)
            {
                throw new System.Exception("Forbidden value (" + victoryCount + ") on element of ArenaRankInfos.victoryCount.");
            }

            fightcount = (short)reader.ReadVarUhShort();
            if (fightcount < 0)
            {
                throw new System.Exception("Forbidden value (" + fightcount + ") on element of ArenaRankInfos.fightcount.");
            }

            numFightNeededForLadder = (short)reader.ReadShort();
            if (numFightNeededForLadder < 0)
            {
                throw new System.Exception("Forbidden value (" + numFightNeededForLadder + ") on element of ArenaRankInfos.numFightNeededForLadder.");
            }

        }


    }
}








