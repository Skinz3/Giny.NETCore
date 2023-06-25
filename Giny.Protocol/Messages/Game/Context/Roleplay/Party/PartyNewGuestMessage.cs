using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyNewGuestMessage : AbstractPartyEventMessage  
    { 
        public new const ushort Id = 5642;
        public override ushort MessageId => Id;

        public PartyGuestInformations guest;

        public PartyNewGuestMessage()
        {
        }
        public PartyNewGuestMessage(PartyGuestInformations guest,int partyId)
        {
            this.guest = guest;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guest.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guest = new PartyGuestInformations();
            guest.Deserialize(reader);
        }


    }
}








