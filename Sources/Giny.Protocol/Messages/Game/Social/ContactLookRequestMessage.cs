using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ContactLookRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9877;
        public override ushort MessageId => Id;

        public byte requestId;
        public byte contactType;

        public ContactLookRequestMessage()
        {
        }
        public ContactLookRequestMessage(byte requestId,byte contactType)
        {
            this.requestId = requestId;
            this.contactType = contactType;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (requestId < 0 || requestId > 255)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element requestId.");
            }

            writer.WriteByte((byte)requestId);
            writer.WriteByte((byte)contactType);
        }
        public override void Deserialize(IDataReader reader)
        {
            requestId = (byte)reader.ReadSByte();
            if (requestId < 0 || requestId > 255)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element of ContactLookRequestMessage.requestId.");
            }

            contactType = (byte)reader.ReadByte();
            if (contactType < 0)
            {
                throw new System.Exception("Forbidden value (" + contactType + ") on element of ContactLookRequestMessage.contactType.");
            }

        }


    }
}








