using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ArenaLeagueRanking  
    { 
        public const ushort Id = 6285;
        public virtual ushort TypeId => Id;

        public short rank;
        public short leagueId;
        public short leaguePoints;
        public short totalLeaguePoints;
        public int ladderPosition;

        public ArenaLeagueRanking()
        {
        }
        public ArenaLeagueRanking(short rank,short leagueId,short leaguePoints,short totalLeaguePoints,int ladderPosition)
        {
            this.rank = rank;
            this.leagueId = leagueId;
            this.leaguePoints = leaguePoints;
            this.totalLeaguePoints = totalLeaguePoints;
            this.ladderPosition = ladderPosition;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element rank.");
            }

            writer.WriteVarShort((short)rank);
            if (leagueId < 0)
            {
                throw new System.Exception("Forbidden value (" + leagueId + ") on element leagueId.");
            }

            writer.WriteVarShort((short)leagueId);
            writer.WriteVarShort((short)leaguePoints);
            writer.WriteVarShort((short)totalLeaguePoints);
            writer.WriteInt((int)ladderPosition);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            rank = (short)reader.ReadVarUhShort();
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element of ArenaLeagueRanking.rank.");
            }

            leagueId = (short)reader.ReadVarUhShort();
            if (leagueId < 0)
            {
                throw new System.Exception("Forbidden value (" + leagueId + ") on element of ArenaLeagueRanking.leagueId.");
            }

            leaguePoints = (short)reader.ReadVarShort();
            totalLeaguePoints = (short)reader.ReadVarShort();
            ladderPosition = (int)reader.ReadInt();
        }


    }
}








