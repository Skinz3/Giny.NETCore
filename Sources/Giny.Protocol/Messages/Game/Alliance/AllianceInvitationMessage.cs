using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceInvitationMessage : NetworkMessage
    {
        public const ushort Id = 8696;
        public override ushort MessageId => Id;

        public long targetId;

        public AllianceInvitationMessage()
        {
        }
        public AllianceInvitationMessage(long targetId)
        {
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (targetId < 0 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteVarLong((long)targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = (long)reader.ReadVarUhLong();
            if (targetId < 0 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of AllianceInvitationMessage.targetId.");
            }

        }

    }
}


