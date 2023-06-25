using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectoryListRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8307;
        public override ushort MessageId => Id;

        public byte jobId;

        public JobCrafterDirectoryListRequestMessage()
        {
        }
        public JobCrafterDirectoryListRequestMessage(byte jobId)
        {
            this.jobId = jobId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
        }
        public override void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobCrafterDirectoryListRequestMessage.jobId.");
            }

        }


    }
}








