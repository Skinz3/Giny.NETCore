using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobDescriptionMessage : NetworkMessage  
    { 
        public  const ushort Id = 8240;
        public override ushort MessageId => Id;

        public JobDescription[] jobsDescription;

        public JobDescriptionMessage()
        {
        }
        public JobDescriptionMessage(JobDescription[] jobsDescription)
        {
            this.jobsDescription = jobsDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)jobsDescription.Length);
            for (uint _i1 = 0;_i1 < jobsDescription.Length;_i1++)
            {
                (jobsDescription[_i1] as JobDescription).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            JobDescription _item1 = null;
            uint _jobsDescriptionLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _jobsDescriptionLen;_i1++)
            {
                _item1 = new JobDescription();
                _item1.Deserialize(reader);
                jobsDescription[_i1] = _item1;
            }

        }


    }
}








