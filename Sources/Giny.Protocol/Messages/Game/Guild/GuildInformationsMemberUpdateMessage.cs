using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildInformationsMemberUpdateMessage : NetworkMessage
    {
        public const ushort Id = 8691;
        public override ushort MessageId => Id;

        public GuildMemberInfo member;

        public GuildInformationsMemberUpdateMessage()
        {
        }
        public GuildInformationsMemberUpdateMessage(GuildMemberInfo member)
        {
            this.member = member;
        }
        public override void Serialize(IDataWriter writer)
        {
            member.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            member = new GuildMemberInfo();
            member.Deserialize(reader);
        }

    }
}


