using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceMemberOnlineStatusMessage : NetworkMessage
    {
        public const ushort Id = 5744;
        public override ushort MessageId => Id;

        public long memberId;
        public bool online;

        public AllianceMemberOnlineStatusMessage()
        {
        }
        public AllianceMemberOnlineStatusMessage(long memberId, bool online)
        {
            this.memberId = memberId;
            this.online = online;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (memberId < 0 || memberId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteBoolean((bool)online);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of AllianceMemberOnlineStatusMessage.memberId.");
            }

            online = (bool)reader.ReadBoolean();
        }

    }
}


