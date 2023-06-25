using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeShopStockStartedMessage : NetworkMessage  
    { 
        public  const ushort Id = 7787;
        public override ushort MessageId => Id;

        public ObjectItemToSell[] objectsInfos;

        public ExchangeShopStockStartedMessage()
        {
        }
        public ExchangeShopStockStartedMessage(ObjectItemToSell[] objectsInfos)
        {
            this.objectsInfos = objectsInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i1 = 0;_i1 < objectsInfos.Length;_i1++)
            {
                (objectsInfos[_i1] as ObjectItemToSell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemToSell _item1 = null;
            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectsInfosLen;_i1++)
            {
                _item1 = new ObjectItemToSell();
                _item1.Deserialize(reader);
                objectsInfos[_i1] = _item1;
            }

        }


    }
}








