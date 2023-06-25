using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectoryRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 2218;
        public override ushort MessageId => Id;

        public byte jobId;
        public long playerId;

        public JobCrafterDirectoryRemoveMessage()
        {
        }
        public JobCrafterDirectoryRemoveMessage(byte jobId,long playerId)
        {
            this.jobId = jobId;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobCrafterDirectoryRemoveMessage.jobId.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of JobCrafterDirectoryRemoveMessage.playerId.");
            }

        }


    }
}








