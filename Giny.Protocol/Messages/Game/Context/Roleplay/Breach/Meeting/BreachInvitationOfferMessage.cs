using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachInvitationOfferMessage : NetworkMessage  
    { 
        public  const ushort Id = 423;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations host;
        public int timeLeftBeforeCancel;

        public BreachInvitationOfferMessage()
        {
        }
        public BreachInvitationOfferMessage(CharacterMinimalInformations host,int timeLeftBeforeCancel)
        {
            this.host = host;
            this.timeLeftBeforeCancel = timeLeftBeforeCancel;
        }
        public override void Serialize(IDataWriter writer)
        {
            host.Serialize(writer);
            if (timeLeftBeforeCancel < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeftBeforeCancel + ") on element timeLeftBeforeCancel.");
            }

            writer.WriteVarInt((int)timeLeftBeforeCancel);
        }
        public override void Deserialize(IDataReader reader)
        {
            host = new CharacterMinimalInformations();
            host.Deserialize(reader);
            timeLeftBeforeCancel = (int)reader.ReadVarUhInt();
            if (timeLeftBeforeCancel < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeftBeforeCancel + ") on element of BreachInvitationOfferMessage.timeLeftBeforeCancel.");
            }

        }


    }
}








