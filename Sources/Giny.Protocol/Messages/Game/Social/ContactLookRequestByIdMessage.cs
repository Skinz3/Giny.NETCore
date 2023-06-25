using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ContactLookRequestByIdMessage : ContactLookRequestMessage  
    { 
        public new const ushort Id = 9628;
        public override ushort MessageId => Id;

        public long playerId;

        public ContactLookRequestByIdMessage()
        {
        }
        public ContactLookRequestByIdMessage(long playerId,byte requestId,byte contactType)
        {
            this.playerId = playerId;
            this.requestId = requestId;
            this.contactType = contactType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of ContactLookRequestByIdMessage.playerId.");
            }

        }


    }
}








