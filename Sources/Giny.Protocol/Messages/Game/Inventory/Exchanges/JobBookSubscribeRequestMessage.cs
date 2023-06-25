using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobBookSubscribeRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7407;
        public override ushort MessageId => Id;

        public byte[] jobIds;

        public JobBookSubscribeRequestMessage()
        {
        }
        public JobBookSubscribeRequestMessage(byte[] jobIds)
        {
            this.jobIds = jobIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)jobIds.Length);
            for (uint _i1 = 0;_i1 < jobIds.Length;_i1++)
            {
                if (jobIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + jobIds[_i1] + ") on element 1 (starting at 1) of jobIds.");
                }

                writer.WriteByte((byte)jobIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _jobIdsLen = (uint)reader.ReadUShort();
            jobIds = new byte[_jobIdsLen];
            for (uint _i1 = 0;_i1 < _jobIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadByte();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of jobIds.");
                }

                jobIds[_i1] = (byte)_val1;
            }

        }


    }
}








