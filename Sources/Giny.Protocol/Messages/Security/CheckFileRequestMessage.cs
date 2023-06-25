using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CheckFileRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9159;
        public override ushort MessageId => Id;

        public string filename;
        public byte type;

        public CheckFileRequestMessage()
        {
        }
        public CheckFileRequestMessage(string filename,byte type)
        {
            this.filename = filename;
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)filename);
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            filename = (string)reader.ReadUTF();
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of CheckFileRequestMessage.type.");
            }

        }


    }
}








