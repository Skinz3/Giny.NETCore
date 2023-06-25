using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CheckIntegrityMessage : NetworkMessage  
    { 
        public  const ushort Id = 320;
        public override ushort MessageId => Id;

        public byte[] data;

        public CheckIntegrityMessage()
        {
        }
        public CheckIntegrityMessage(byte[] data)
        {
            this.data = data;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)data.Length);
            for (uint _i1 = 0;_i1 < data.Length;_i1++)
            {
                writer.WriteByte((byte)data[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val1 = 0;
            uint _dataLen = (uint)reader.ReadVarInt();
            data = new byte[_dataLen];
            for (uint _i1 = 0;_i1 < _dataLen;_i1++)
            {
                _val1 = (int)reader.ReadByte();
                data[_i1] = (byte)_val1;
            }

        }


    }
}








