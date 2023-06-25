using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceModificationNameAndTagValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 1131;
        public override ushort MessageId => Id;

        public string allianceName;
        public string allianceTag;

        public AllianceModificationNameAndTagValidMessage()
        {
        }
        public AllianceModificationNameAndTagValidMessage(string allianceName,string allianceTag)
        {
            this.allianceName = allianceName;
            this.allianceTag = allianceTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)allianceName);
            writer.WriteUTF((string)allianceTag);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceName = (string)reader.ReadUTF();
            allianceTag = (string)reader.ReadUTF();
        }


    }
}








