using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectsQuantityMessage : NetworkMessage  
    { 
        public  const ushort Id = 8174;
        public override ushort MessageId => Id;

        public ObjectItemQuantity[] objectsUIDAndQty;

        public ObjectsQuantityMessage()
        {
        }
        public ObjectsQuantityMessage(ObjectItemQuantity[] objectsUIDAndQty)
        {
            this.objectsUIDAndQty = objectsUIDAndQty;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectsUIDAndQty.Length);
            for (uint _i1 = 0;_i1 < objectsUIDAndQty.Length;_i1++)
            {
                (objectsUIDAndQty[_i1] as ObjectItemQuantity).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemQuantity _item1 = null;
            uint _objectsUIDAndQtyLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectsUIDAndQtyLen;_i1++)
            {
                _item1 = new ObjectItemQuantity();
                _item1.Deserialize(reader);
                objectsUIDAndQty[_i1] = _item1;
            }

        }


    }
}








