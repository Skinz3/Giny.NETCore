using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class RawDataMessage : NetworkMessage  
    { 
        public  const ushort Id = 4473;
        public override ushort MessageId => Id;

        public byte[] content;

        public RawDataMessage()
        {
        }
        public RawDataMessage(byte[] content)
        {
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)content.Length);
            for (uint _i1 = 0;_i1 < content.Length;_i1++)
            {
                writer.WriteByte((byte)content[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _contentLen = reader.ReadVarInt();
            content = reader.ReadBytes(_contentLen);
        }


    }
}








