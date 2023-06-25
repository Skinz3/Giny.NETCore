using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InviteInHavenBagMessage : NetworkMessage  
    { 
        public  const ushort Id = 8351;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations guestInformations;
        public bool accept;

        public InviteInHavenBagMessage()
        {
        }
        public InviteInHavenBagMessage(CharacterMinimalInformations guestInformations,bool accept)
        {
            this.guestInformations = guestInformations;
            this.accept = accept;
        }
        public override void Serialize(IDataWriter writer)
        {
            guestInformations.Serialize(writer);
            writer.WriteBoolean((bool)accept);
        }
        public override void Deserialize(IDataReader reader)
        {
            guestInformations = new CharacterMinimalInformations();
            guestInformations.Deserialize(reader);
            accept = (bool)reader.ReadBoolean();
        }


    }
}








