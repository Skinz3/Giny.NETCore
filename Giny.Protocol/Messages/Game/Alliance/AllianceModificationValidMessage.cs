using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceModificationValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 2712;
        public override ushort MessageId => Id;

        public string allianceName;
        public string allianceTag;
        public GuildEmblem Alliancemblem;

        public AllianceModificationValidMessage()
        {
        }
        public AllianceModificationValidMessage(string allianceName,string allianceTag,GuildEmblem Alliancemblem)
        {
            this.allianceName = allianceName;
            this.allianceTag = allianceTag;
            this.Alliancemblem = Alliancemblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)allianceName);
            writer.WriteUTF((string)allianceTag);
            Alliancemblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceName = (string)reader.ReadUTF();
            allianceTag = (string)reader.ReadUTF();
            Alliancemblem = new GuildEmblem();
            Alliancemblem.Deserialize(reader);
        }


    }
}








