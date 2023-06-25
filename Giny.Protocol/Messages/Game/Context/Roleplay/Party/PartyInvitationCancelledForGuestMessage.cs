using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationCancelledForGuestMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 3125;
        public override ushort MessageId => Id;

        public long cancelerId;

        public PartyInvitationCancelledForGuestMessage()
        {
        }
        public PartyInvitationCancelledForGuestMessage(long cancelerId,int partyId)
        {
            this.cancelerId = cancelerId;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (cancelerId < 0 || cancelerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + cancelerId + ") on element cancelerId.");
            }

            writer.WriteVarLong((long)cancelerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            cancelerId = (long)reader.ReadVarUhLong();
            if (cancelerId < 0 || cancelerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + cancelerId + ") on element of PartyInvitationCancelledForGuestMessage.cancelerId.");
            }

        }


    }
}








