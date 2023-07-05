using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PartyCancelInvitationNotificationMessage : AbstractPartyEventMessage
    {
        public new const ushort Id = 5196;
        public override ushort MessageId => Id;

        public long cancelerId;
        public long guestId;

        public PartyCancelInvitationNotificationMessage()
        {
        }
        public PartyCancelInvitationNotificationMessage(long cancelerId, long guestId, int partyId)
        {
            this.cancelerId = cancelerId;
            this.guestId = guestId;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (cancelerId < 0 || cancelerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + cancelerId + ") on element cancelerId.");
            }

            writer.WriteVarLong((long)cancelerId);
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element guestId.");
            }

            writer.WriteVarLong((long)guestId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            cancelerId = (long)reader.ReadVarUhLong();
            if (cancelerId < 0 || cancelerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + cancelerId + ") on element of PartyCancelInvitationNotificationMessage.cancelerId.");
            }

            guestId = (long)reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element of PartyCancelInvitationNotificationMessage.guestId.");
            }

        }

    }
}


