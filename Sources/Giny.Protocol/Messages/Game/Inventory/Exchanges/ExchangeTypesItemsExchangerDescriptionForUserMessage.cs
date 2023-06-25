using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeTypesItemsExchangerDescriptionForUserMessage : NetworkMessage  
    { 
        public  const ushort Id = 5554;
        public override ushort MessageId => Id;

        public int objectType;
        public BidExchangerObjectInfo[] itemTypeDescriptions;

        public ExchangeTypesItemsExchangerDescriptionForUserMessage()
        {
        }
        public ExchangeTypesItemsExchangerDescriptionForUserMessage(int objectType,BidExchangerObjectInfo[] itemTypeDescriptions)
        {
            this.objectType = objectType;
            this.itemTypeDescriptions = itemTypeDescriptions;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element objectType.");
            }

            writer.WriteInt((int)objectType);
            writer.WriteShort((short)itemTypeDescriptions.Length);
            for (uint _i2 = 0;_i2 < itemTypeDescriptions.Length;_i2++)
            {
                (itemTypeDescriptions[_i2] as BidExchangerObjectInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            BidExchangerObjectInfo _item2 = null;
            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of ExchangeTypesItemsExchangerDescriptionForUserMessage.objectType.");
            }

            uint _itemTypeDescriptionsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _itemTypeDescriptionsLen;_i2++)
            {
                _item2 = new BidExchangerObjectInfo();
                _item2.Deserialize(reader);
                itemTypeDescriptions[_i2] = _item2;
            }

        }


    }
}








