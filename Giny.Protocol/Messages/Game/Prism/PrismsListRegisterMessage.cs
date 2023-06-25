using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismsListRegisterMessage : NetworkMessage  
    { 
        public  const ushort Id = 2687;
        public override ushort MessageId => Id;

        public byte listen;

        public PrismsListRegisterMessage()
        {
        }
        public PrismsListRegisterMessage(byte listen)
        {
            this.listen = listen;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)listen);
        }
        public override void Deserialize(IDataReader reader)
        {
            listen = (byte)reader.ReadByte();
            if (listen < 0)
            {
                throw new System.Exception("Forbidden value (" + listen + ") on element of PrismsListRegisterMessage.listen.");
            }

        }


    }
}








