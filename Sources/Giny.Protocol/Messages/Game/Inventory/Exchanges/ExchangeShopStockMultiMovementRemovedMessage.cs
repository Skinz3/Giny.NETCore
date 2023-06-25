using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeShopStockMultiMovementRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8793;
        public override ushort MessageId => Id;

        public int[] objectIdList;

        public ExchangeShopStockMultiMovementRemovedMessage()
        {
        }
        public ExchangeShopStockMultiMovementRemovedMessage(int[] objectIdList)
        {
            this.objectIdList = objectIdList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectIdList.Length);
            for (uint _i1 = 0;_i1 < objectIdList.Length;_i1++)
            {
                if (objectIdList[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + objectIdList[_i1] + ") on element 1 (starting at 1) of objectIdList.");
                }

                writer.WriteVarInt((int)objectIdList[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _objectIdListLen = (uint)reader.ReadUShort();
            objectIdList = new int[_objectIdListLen];
            for (uint _i1 = 0;_i1 < _objectIdListLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of objectIdList.");
                }

                objectIdList[_i1] = (int)_val1;
            }

        }


    }
}








