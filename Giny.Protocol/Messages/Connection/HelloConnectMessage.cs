using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HelloConnectMessage : NetworkMessage  
    { 
        public  const ushort Id = 6424;
        public override ushort MessageId => Id;

        public string salt;
        public byte[] key;

        public HelloConnectMessage()
        {
        }
        public HelloConnectMessage(string salt,byte[] key)
        {
            this.salt = salt;
            this.key = key;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)salt);
            writer.WriteVarInt((int)key.Length);
            for (uint _i2 = 0;_i2 < key.Length;_i2++)
            {
                writer.WriteByte((byte)key[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val2 = 0;
            salt = (string)reader.ReadUTF();
            uint _keyLen = (uint)reader.ReadVarInt();
            key = new byte[_keyLen];
            for (uint _i2 = 0;_i2 < _keyLen;_i2++)
            {
                _val2 = (int)reader.ReadByte();
                key[_i2] = (byte)_val2;
            }

        }


    }
}








