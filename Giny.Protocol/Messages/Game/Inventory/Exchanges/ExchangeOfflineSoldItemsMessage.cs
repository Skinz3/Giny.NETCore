using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeOfflineSoldItemsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6012;
        public override ushort MessageId => Id;

        public ObjectItemQuantityPriceDateEffects[] bidHouseItems;
        public ObjectItemQuantityPriceDateEffects[] merchantItems;

        public ExchangeOfflineSoldItemsMessage()
        {
        }
        public ExchangeOfflineSoldItemsMessage(ObjectItemQuantityPriceDateEffects[] bidHouseItems,ObjectItemQuantityPriceDateEffects[] merchantItems)
        {
            this.bidHouseItems = bidHouseItems;
            this.merchantItems = merchantItems;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)bidHouseItems.Length);
            for (uint _i1 = 0;_i1 < bidHouseItems.Length;_i1++)
            {
                (bidHouseItems[_i1] as ObjectItemQuantityPriceDateEffects).Serialize(writer);
            }

            writer.WriteShort((short)merchantItems.Length);
            for (uint _i2 = 0;_i2 < merchantItems.Length;_i2++)
            {
                (merchantItems[_i2] as ObjectItemQuantityPriceDateEffects).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemQuantityPriceDateEffects _item1 = null;
            ObjectItemQuantityPriceDateEffects _item2 = null;
            uint _bidHouseItemsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _bidHouseItemsLen;_i1++)
            {
                _item1 = new ObjectItemQuantityPriceDateEffects();
                _item1.Deserialize(reader);
                bidHouseItems[_i1] = _item1;
            }

            uint _merchantItemsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _merchantItemsLen;_i2++)
            {
                _item2 = new ObjectItemQuantityPriceDateEffects();
                _item2.Deserialize(reader);
                merchantItems[_i2] = _item2;
            }

        }


    }
}








