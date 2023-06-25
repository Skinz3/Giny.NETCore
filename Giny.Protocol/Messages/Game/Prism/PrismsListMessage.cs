using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 9789;
        public override ushort MessageId => Id;

        public PrismSubareaEmptyInfo[] prisms;

        public PrismsListMessage()
        {
        }
        public PrismsListMessage(PrismSubareaEmptyInfo[] prisms)
        {
            this.prisms = prisms;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)prisms.Length);
            for (uint _i1 = 0;_i1 < prisms.Length;_i1++)
            {
                writer.WriteShort((short)(prisms[_i1] as PrismSubareaEmptyInfo).TypeId);
                (prisms[_i1] as PrismSubareaEmptyInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            PrismSubareaEmptyInfo _item1 = null;
            uint _prismsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _prismsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>((short)_id1);
                _item1.Deserialize(reader);
                prisms[_i1] = _item1;
            }

        }


    }
}








