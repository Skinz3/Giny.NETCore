using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildModificationValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 8608;
        public override ushort MessageId => Id;

        public string guildName;
        public GuildEmblem guildEmblem;

        public GuildModificationValidMessage()
        {
        }
        public GuildModificationValidMessage(string guildName,GuildEmblem guildEmblem)
        {
            this.guildName = guildName;
            this.guildEmblem = guildEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)guildName);
            guildEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildName = (string)reader.ReadUTF();
            guildEmblem = new GuildEmblem();
            guildEmblem.Deserialize(reader);
        }


    }
}








