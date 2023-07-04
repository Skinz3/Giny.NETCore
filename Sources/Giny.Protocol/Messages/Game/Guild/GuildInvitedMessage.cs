using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildInvitedMessage : NetworkMessage
    {
        public const ushort Id = 4875;
        public override ushort MessageId => Id;

        public string recruterName;
        public GuildInformations guildInfo;

        public GuildInvitedMessage()
        {
        }
        public GuildInvitedMessage(string recruterName, GuildInformations guildInfo)
        {
            this.recruterName = recruterName;
            this.guildInfo = guildInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)recruterName);
            guildInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruterName = (string)reader.ReadUTF();
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
        }

    }
}


