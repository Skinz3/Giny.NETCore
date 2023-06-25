using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeMountsPaddockRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 4356;
        public override ushort MessageId => Id;

        public int[] mountsId;

        public ExchangeMountsPaddockRemoveMessage()
        {
        }
        public ExchangeMountsPaddockRemoveMessage(int[] mountsId)
        {
            this.mountsId = mountsId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)mountsId.Length);
            for (uint _i1 = 0;_i1 < mountsId.Length;_i1++)
            {
                writer.WriteVarInt((int)mountsId[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val1 = 0;
            uint _mountsIdLen = (uint)reader.ReadUShort();
            mountsId = new int[_mountsIdLen];
            for (uint _i1 = 0;_i1 < _mountsIdLen;_i1++)
            {
                _val1 = (int)reader.ReadVarInt();
                mountsId[_i1] = (int)_val1;
            }

        }


    }
}








