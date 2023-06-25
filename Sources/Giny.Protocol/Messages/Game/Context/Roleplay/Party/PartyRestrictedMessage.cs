using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyRestrictedMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 4094;
        public override ushort MessageId => Id;

        public bool restricted;

        public PartyRestrictedMessage()
        {
        }
        public PartyRestrictedMessage(bool restricted,int partyId)
        {
            this.restricted = restricted;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)restricted);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            restricted = (bool)reader.ReadBoolean();
        }


    }
}








