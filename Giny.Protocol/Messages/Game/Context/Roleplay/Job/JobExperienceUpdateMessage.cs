using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobExperienceUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 9901;
        public override ushort MessageId => Id;

        public JobExperience experiencesUpdate;

        public JobExperienceUpdateMessage()
        {
        }
        public JobExperienceUpdateMessage(JobExperience experiencesUpdate)
        {
            this.experiencesUpdate = experiencesUpdate;
        }
        public override void Serialize(IDataWriter writer)
        {
            experiencesUpdate.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            experiencesUpdate = new JobExperience();
            experiencesUpdate.Deserialize(reader);
        }


    }
}








