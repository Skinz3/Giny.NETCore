using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceCreationValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 6054;
        public override ushort MessageId => Id;

        public string allianceName;
        public string allianceTag;
        public GuildEmblem allianceEmblem;

        public AllianceCreationValidMessage()
        {
        }
        public AllianceCreationValidMessage(string allianceName,string allianceTag,GuildEmblem allianceEmblem)
        {
            this.allianceName = allianceName;
            this.allianceTag = allianceTag;
            this.allianceEmblem = allianceEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)allianceName);
            writer.WriteUTF((string)allianceTag);
            allianceEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceName = (string)reader.ReadUTF();
            allianceTag = (string)reader.ReadUTF();
            allianceEmblem = new GuildEmblem();
            allianceEmblem.Deserialize(reader);
        }


    }
}








