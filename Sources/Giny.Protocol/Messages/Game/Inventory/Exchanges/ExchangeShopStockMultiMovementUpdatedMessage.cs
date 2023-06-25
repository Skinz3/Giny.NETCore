using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeShopStockMultiMovementUpdatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4703;
        public override ushort MessageId => Id;

        public ObjectItemToSell[] objectInfoList;

        public ExchangeShopStockMultiMovementUpdatedMessage()
        {
        }
        public ExchangeShopStockMultiMovementUpdatedMessage(ObjectItemToSell[] objectInfoList)
        {
            this.objectInfoList = objectInfoList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectInfoList.Length);
            for (uint _i1 = 0;_i1 < objectInfoList.Length;_i1++)
            {
                (objectInfoList[_i1] as ObjectItemToSell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemToSell _item1 = null;
            uint _objectInfoListLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectInfoListLen;_i1++)
            {
                _item1 = new ObjectItemToSell();
                _item1.Deserialize(reader);
                objectInfoList[_i1] = _item1;
            }

        }


    }
}








