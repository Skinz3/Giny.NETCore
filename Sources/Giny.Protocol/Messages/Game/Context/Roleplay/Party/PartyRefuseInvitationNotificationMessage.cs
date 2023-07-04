using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PartyRefuseInvitationNotificationMessage : AbstractPartyEventMessage
    {
        public new const ushort Id = 1547;
        public override ushort MessageId => Id;

        public long guestId;

        public PartyRefuseInvitationNotificationMessage()
        {
        }
        public PartyRefuseInvitationNotificationMessage(long guestId, int partyId)
        {
            this.guestId = guestId;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element guestId.");
            }

            writer.WriteVarLong((long)guestId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guestId = (long)reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element of PartyRefuseInvitationNotificationMessage.guestId.");
            }

        }

    }
}


