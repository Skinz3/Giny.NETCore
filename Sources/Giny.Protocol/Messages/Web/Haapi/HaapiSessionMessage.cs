using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiSessionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1564;
        public override ushort MessageId => Id;

        public string key;
        public byte type;

        public HaapiSessionMessage()
        {
        }
        public HaapiSessionMessage(string key,byte type)
        {
            this.key = key;
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)key);
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            key = (string)reader.ReadUTF();
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of HaapiSessionMessage.type.");
            }

        }


    }
}








