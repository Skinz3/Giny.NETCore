using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CheckFileMessage : NetworkMessage  
    { 
        public  const ushort Id = 3494;
        public override ushort MessageId => Id;

        public string filenameHash;
        public byte type;
        public string value;

        public CheckFileMessage()
        {
        }
        public CheckFileMessage(string filenameHash,byte type,string value)
        {
            this.filenameHash = filenameHash;
            this.type = type;
            this.value = value;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)filenameHash);
            writer.WriteByte((byte)type);
            writer.WriteUTF((string)value);
        }
        public override void Deserialize(IDataReader reader)
        {
            filenameHash = (string)reader.ReadUTF();
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of CheckFileMessage.type.");
            }

            value = (string)reader.ReadUTF();
        }


    }
}








