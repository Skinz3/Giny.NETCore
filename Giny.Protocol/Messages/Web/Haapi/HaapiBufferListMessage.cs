using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiBufferListMessage : NetworkMessage  
    { 
        public  const ushort Id = 5285;
        public override ushort MessageId => Id;

        public BufferInformation[] buffers;

        public HaapiBufferListMessage()
        {
        }
        public HaapiBufferListMessage(BufferInformation[] buffers)
        {
            this.buffers = buffers;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)buffers.Length);
            for (uint _i1 = 0;_i1 < buffers.Length;_i1++)
            {
                (buffers[_i1] as BufferInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            BufferInformation _item1 = null;
            uint _buffersLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _buffersLen;_i1++)
            {
                _item1 = new BufferInformation();
                _item1.Deserialize(reader);
                buffers[_i1] = _item1;
            }

        }


    }
}








