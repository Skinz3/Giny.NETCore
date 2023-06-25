using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolPartyRefreshMessage : NetworkMessage  
    { 
        public  const ushort Id = 2234;
        public override ushort MessageId => Id;

        public PartyIdol partyIdol;

        public IdolPartyRefreshMessage()
        {
        }
        public IdolPartyRefreshMessage(PartyIdol partyIdol)
        {
            this.partyIdol = partyIdol;
        }
        public override void Serialize(IDataWriter writer)
        {
            partyIdol.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            partyIdol = new PartyIdol();
            partyIdol.Deserialize(reader);
        }


    }
}








