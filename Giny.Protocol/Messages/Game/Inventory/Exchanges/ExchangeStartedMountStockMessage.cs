using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartedMountStockMessage : NetworkMessage  
    { 
        public  const ushort Id = 3642;
        public override ushort MessageId => Id;

        public ObjectItem[] objectsInfos;

        public ExchangeStartedMountStockMessage()
        {
        }
        public ExchangeStartedMountStockMessage(ObjectItem[] objectsInfos)
        {
            this.objectsInfos = objectsInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i1 = 0;_i1 < objectsInfos.Length;_i1++)
            {
                (objectsInfos[_i1] as ObjectItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItem _item1 = null;
            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectsInfosLen;_i1++)
            {
                _item1 = new ObjectItem();
                _item1.Deserialize(reader);
                objectsInfos[_i1] = _item1;
            }

        }


    }
}








