using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceMemberInformationUpdateMessage : NetworkMessage
    {
        public const ushort Id = 7409;
        public override ushort MessageId => Id;

        public AllianceMemberInfo member;

        public AllianceMemberInformationUpdateMessage()
        {
        }
        public AllianceMemberInformationUpdateMessage(AllianceMemberInfo member)
        {
            this.member = member;
        }
        public override void Serialize(IDataWriter writer)
        {
            member.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            member = new AllianceMemberInfo();
            member.Deserialize(reader);
        }

    }
}


