using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceModificationEmblemValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 7588;
        public override ushort MessageId => Id;

        public GuildEmblem Alliancemblem;

        public AllianceModificationEmblemValidMessage()
        {
        }
        public AllianceModificationEmblemValidMessage(GuildEmblem Alliancemblem)
        {
            this.Alliancemblem = Alliancemblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            Alliancemblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            Alliancemblem = new GuildEmblem();
            Alliancemblem.Deserialize(reader);
        }


    }
}








