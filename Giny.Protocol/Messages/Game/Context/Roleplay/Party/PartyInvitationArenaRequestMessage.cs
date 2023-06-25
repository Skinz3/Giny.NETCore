using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage  
    { 
        public new const ushort Id = 5948;
        public override ushort MessageId => Id;


        public PartyInvitationArenaRequestMessage()
        {
        }
        public PartyInvitationArenaRequestMessage(AbstractPlayerSearchInformation target)
        {
            this.target = target;
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








