using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InviteInHavenBagClosedMessage : NetworkMessage  
    { 
        public  const ushort Id = 5668;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations hostInformations;

        public InviteInHavenBagClosedMessage()
        {
        }
        public InviteInHavenBagClosedMessage(CharacterMinimalInformations hostInformations)
        {
            this.hostInformations = hostInformations;
        }
        public override void Serialize(IDataWriter writer)
        {
            hostInformations.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            hostInformations = new CharacterMinimalInformations();
            hostInformations.Deserialize(reader);
        }


    }
}








