using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildApplicationIsAnsweredMessage : NetworkMessage  
    { 
        public  const ushort Id = 3119;
        public override ushort MessageId => Id;

        public bool accepted;
        public GuildInformations guildInformation;

        public GuildApplicationIsAnsweredMessage()
        {
        }
        public GuildApplicationIsAnsweredMessage(bool accepted,GuildInformations guildInformation)
        {
            this.accepted = accepted;
            this.guildInformation = guildInformation;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)accepted);
            guildInformation.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            accepted = (bool)reader.ReadBoolean();
            guildInformation = new GuildInformations();
            guildInformation.Deserialize(reader);
        }


    }
}








