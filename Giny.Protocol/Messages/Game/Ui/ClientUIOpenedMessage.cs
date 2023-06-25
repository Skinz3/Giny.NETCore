using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ClientUIOpenedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4363;
        public override ushort MessageId => Id;

        public byte type;

        public ClientUIOpenedMessage()
        {
        }
        public ClientUIOpenedMessage(byte type)
        {
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of ClientUIOpenedMessage.type.");
            }

        }


    }
}








