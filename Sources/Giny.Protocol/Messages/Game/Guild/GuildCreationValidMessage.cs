using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildCreationValidMessage : NetworkMessage
    {
        public const ushort Id = 5853;
        public override ushort MessageId => Id;

        public string guildName;
        public SocialEmblem guildEmblem;

        public GuildCreationValidMessage()
        {
        }
        public GuildCreationValidMessage(string guildName, SocialEmblem guildEmblem)
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
            guildEmblem = new SocialEmblem();
            guildEmblem.Deserialize(reader);
        }

    }
}


