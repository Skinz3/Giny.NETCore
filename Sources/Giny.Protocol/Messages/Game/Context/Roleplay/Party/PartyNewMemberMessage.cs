using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyNewMemberMessage : PartyUpdateMessage  
    { 
        public new const ushort Id = 8047;
        public override ushort MessageId => Id;


        public PartyNewMemberMessage()
        {
        }
        public PartyNewMemberMessage(int partyId,PartyMemberInformations memberInformations)
        {
            this.partyId = partyId;
            this.memberInformations = memberInformations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








