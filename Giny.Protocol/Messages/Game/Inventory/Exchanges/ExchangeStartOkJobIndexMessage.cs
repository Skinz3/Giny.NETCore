using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkJobIndexMessage : NetworkMessage  
    { 
        public  const ushort Id = 7830;
        public override ushort MessageId => Id;

        public int[] jobs;

        public ExchangeStartOkJobIndexMessage()
        {
        }
        public ExchangeStartOkJobIndexMessage(int[] jobs)
        {
            this.jobs = jobs;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)jobs.Length);
            for (uint _i1 = 0;_i1 < jobs.Length;_i1++)
            {
                if (jobs[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + jobs[_i1] + ") on element 1 (starting at 1) of jobs.");
                }

                writer.WriteVarInt((int)jobs[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _jobsLen = (uint)reader.ReadUShort();
            jobs = new int[_jobsLen];
            for (uint _i1 = 0;_i1 < _jobsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of jobs.");
                }

                jobs[_i1] = (int)_val1;
            }

        }


    }
}








