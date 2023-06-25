using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerExperienceModificatorMessage : NetworkMessage  
    { 
        public  const ushort Id = 7253;
        public override ushort MessageId => Id;

        public short experiencePercent;

        public ServerExperienceModificatorMessage()
        {
        }
        public ServerExperienceModificatorMessage(short experiencePercent)
        {
            this.experiencePercent = experiencePercent;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (experiencePercent < 0)
            {
                throw new System.Exception("Forbidden value (" + experiencePercent + ") on element experiencePercent.");
            }

            writer.WriteVarShort((short)experiencePercent);
        }
        public override void Deserialize(IDataReader reader)
        {
            experiencePercent = (short)reader.ReadVarUhShort();
            if (experiencePercent < 0)
            {
                throw new System.Exception("Forbidden value (" + experiencePercent + ") on element of ServerExperienceModificatorMessage.experiencePercent.");
            }

        }


    }
}








