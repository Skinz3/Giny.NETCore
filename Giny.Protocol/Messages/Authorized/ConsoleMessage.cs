using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ConsoleMessage : NetworkMessage  
    { 
        public  const ushort Id = 806;
        public override ushort MessageId => Id;

        public byte type;
        public string content;

        public ConsoleMessage()
        {
        }
        public ConsoleMessage(byte type,string content)
        {
            this.type = type;
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteUTF((string)content);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of ConsoleMessage.type.");
            }

            content = (string)reader.ReadUTF();
        }


    }
}








