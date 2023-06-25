using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiValidationRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6667;
        public override ushort MessageId => Id;

        public string transaction;

        public HaapiValidationRequestMessage()
        {
        }
        public HaapiValidationRequestMessage(string transaction)
        {
            this.transaction = transaction;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)transaction);
        }
        public override void Deserialize(IDataReader reader)
        {
            transaction = (string)reader.ReadUTF();
        }


    }
}








