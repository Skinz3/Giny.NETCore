using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyLoyaltyStatusMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 9291;
        public override ushort MessageId => Id;

        public bool loyal;

        public PartyLoyaltyStatusMessage()
        {
        }
        public PartyLoyaltyStatusMessage(bool loyal,int partyId)
        {
            this.loyal = loyal;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)loyal);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            loyal = (bool)reader.ReadBoolean();
        }


    }
}








