using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseUnsoldItemsMessage : NetworkMessage  
    { 
        public  const ushort Id = 2886;
        public override ushort MessageId => Id;

        public ObjectItemGenericQuantity[] items;

        public ExchangeBidHouseUnsoldItemsMessage()
        {
        }
        public ExchangeBidHouseUnsoldItemsMessage(ObjectItemGenericQuantity[] items)
        {
            this.items = items;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)items.Length);
            for (uint _i1 = 0;_i1 < items.Length;_i1++)
            {
                (items[_i1] as ObjectItemGenericQuantity).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemGenericQuantity _item1 = null;
            uint _itemsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _itemsLen;_i1++)
            {
                _item1 = new ObjectItemGenericQuantity();
                _item1.Deserialize(reader);
                items[_i1] = _item1;
            }

        }


    }
}








