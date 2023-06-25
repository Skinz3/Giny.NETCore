using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StorageObjectsRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 9509;
        public override ushort MessageId => Id;

        public int[] objectUIDList;

        public StorageObjectsRemoveMessage()
        {
        }
        public StorageObjectsRemoveMessage(int[] objectUIDList)
        {
            this.objectUIDList = objectUIDList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objectUIDList.Length);
            for (uint _i1 = 0;_i1 < objectUIDList.Length;_i1++)
            {
                if (objectUIDList[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + objectUIDList[_i1] + ") on element 1 (starting at 1) of objectUIDList.");
                }

                writer.WriteVarInt((int)objectUIDList[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _objectUIDListLen = (uint)reader.ReadUShort();
            objectUIDList = new int[_objectUIDListLen];
            for (uint _i1 = 0;_i1 < _objectUIDListLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of objectUIDList.");
                }

                objectUIDList[_i1] = (int)_val1;
            }

        }


    }
}








