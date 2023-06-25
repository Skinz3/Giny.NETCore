using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InventoryContentMessage : NetworkMessage  
    { 
        public  const ushort Id = 1797;
        public override ushort MessageId => Id;

        public ObjectItem[] objects;
        public long kamas;

        public InventoryContentMessage()
        {
        }
        public InventoryContentMessage(ObjectItem[] objects,long kamas)
        {
            this.objects = objects;
            this.kamas = kamas;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objects.Length);
            for (uint _i1 = 0;_i1 < objects.Length;_i1++)
            {
                (objects[_i1] as ObjectItem).Serialize(writer);
            }

            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItem _item1 = null;
            uint _objectsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectsLen;_i1++)
            {
                _item1 = new ObjectItem();
                _item1.Deserialize(reader);
                objects[_i1] = _item1;
            }

            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of InventoryContentMessage.kamas.");
            }

        }


    }
}








