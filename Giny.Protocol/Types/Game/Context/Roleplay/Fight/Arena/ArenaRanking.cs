using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ArenaRanking  
    { 
        public const ushort Id = 3975;
        public virtual ushort TypeId => Id;

        public short rank;
        public short bestRank;

        public ArenaRanking()
        {
        }
        public ArenaRanking(short rank,short bestRank)
        {
            this.rank = rank;
            this.bestRank = bestRank;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element rank.");
            }

            writer.WriteVarShort((short)rank);
            if (bestRank < 0 || bestRank > 20000)
            {
                throw new System.Exception("Forbidden value (" + bestRank + ") on element bestRank.");
            }

            writer.WriteVarShort((short)bestRank);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            rank = (short)reader.ReadVarUhShort();
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element of ArenaRanking.rank.");
            }

            bestRank = (short)reader.ReadVarUhShort();
            if (bestRank < 0 || bestRank > 20000)
            {
                throw new System.Exception("Forbidden value (" + bestRank + ") on element of ArenaRanking.bestRank.");
            }

        }


    }
}








