using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildModificationEmblemValidMessage : NetworkMessage
    {
        public const ushort Id = 5777;
        public override ushort MessageId => Id;

        public SocialEmblem guildEmblem;

        public GuildModificationEmblemValidMessage()
        {
        }
        public GuildModificationEmblemValidMessage(SocialEmblem guildEmblem)
        {
            this.guildEmblem = guildEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            guildEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildEmblem = new SocialEmblem();
            guildEmblem.Deserialize(reader);
        }

    }
}


