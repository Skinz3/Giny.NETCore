using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachInvitationCloseMessage : NetworkMessage  
    { 
        public  const ushort Id = 5791;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations host;

        public BreachInvitationCloseMessage()
        {
        }
        public BreachInvitationCloseMessage(CharacterMinimalInformations host)
        {
            this.host = host;
        }
        public override void Serialize(IDataWriter writer)
        {
            host.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            host = new CharacterMinimalInformations();
            host.Deserialize(reader);
        }


    }
}








