using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InviteInHavenBagOfferMessage : NetworkMessage  
    { 
        public  const ushort Id = 7339;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations hostInformations;
        public int timeLeftBeforeCancel;

        public InviteInHavenBagOfferMessage()
        {
        }
        public InviteInHavenBagOfferMessage(CharacterMinimalInformations hostInformations,int timeLeftBeforeCancel)
        {
            this.hostInformations = hostInformations;
            this.timeLeftBeforeCancel = timeLeftBeforeCancel;
        }
        public override void Serialize(IDataWriter writer)
        {
            hostInformations.Serialize(writer);
            writer.WriteVarInt((int)timeLeftBeforeCancel);
        }
        public override void Deserialize(IDataReader reader)
        {
            hostInformations = new CharacterMinimalInformations();
            hostInformations.Deserialize(reader);
            timeLeftBeforeCancel = (int)reader.ReadVarInt();
        }


    }
}








