using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class RemoveGuildRankRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 2691;
        public override ushort MessageId => Id;

        public int rankId;
        public int newRankId;

        public RemoveGuildRankRequestMessage()
        {
        }
        public RemoveGuildRankRequestMessage(int rankId,int newRankId)
        {
            this.rankId = rankId;
            this.newRankId = newRankId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
            if (newRankId < 0)
            {
                throw new System.Exception("Forbidden value (" + newRankId + ") on element newRankId.");
            }

            writer.WriteVarInt((int)newRankId);
        }
        public override void Deserialize(IDataReader reader)
        {
            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of RemoveGuildRankRequestMessage.rankId.");
            }

            newRankId = (int)reader.ReadVarUhInt();
            if (newRankId < 0)
            {
                throw new System.Exception("Forbidden value (" + newRankId + ") on element of RemoveGuildRankRequestMessage.newRankId.");
            }

        }


    }
}








