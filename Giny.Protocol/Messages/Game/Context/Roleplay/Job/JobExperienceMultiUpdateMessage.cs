using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobExperienceMultiUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7895;
        public override ushort MessageId => Id;

        public JobExperience[] experiencesUpdate;

        public JobExperienceMultiUpdateMessage()
        {
        }
        public JobExperienceMultiUpdateMessage(JobExperience[] experiencesUpdate)
        {
            this.experiencesUpdate = experiencesUpdate;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)experiencesUpdate.Length);
            for (uint _i1 = 0;_i1 < experiencesUpdate.Length;_i1++)
            {
                (experiencesUpdate[_i1] as JobExperience).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            JobExperience _item1 = null;
            uint _experiencesUpdateLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _experiencesUpdateLen;_i1++)
            {
                _item1 = new JobExperience();
                _item1.Deserialize(reader);
                experiencesUpdate[_i1] = _item1;
            }

        }


    }
}








