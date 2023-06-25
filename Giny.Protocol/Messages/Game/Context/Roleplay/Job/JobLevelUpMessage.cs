using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobLevelUpMessage : NetworkMessage  
    { 
        public  const ushort Id = 3922;
        public override ushort MessageId => Id;

        public byte newLevel;
        public JobDescription jobsDescription;

        public JobLevelUpMessage()
        {
        }
        public JobLevelUpMessage(byte newLevel,JobDescription jobsDescription)
        {
            this.newLevel = newLevel;
            this.jobsDescription = jobsDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (newLevel < 0 || newLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element newLevel.");
            }

            writer.WriteByte((byte)newLevel);
            jobsDescription.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            newLevel = (byte)reader.ReadSByte();
            if (newLevel < 0 || newLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element of JobLevelUpMessage.newLevel.");
            }

            jobsDescription = new JobDescription();
            jobsDescription.Deserialize(reader);
        }


    }
}








