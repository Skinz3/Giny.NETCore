using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectsRemovedMessage : ExchangeObjectMessage  
    { 
        public new const ushort Id = 7723;
        public override ushort MessageId => Id;

        public int[] objectUID;

        public ExchangeObjectsRemovedMessage()
        {
        }
        public ExchangeObjectsRemovedMessage(int[] objectUID,bool remote)
        {
            this.objectUID = objectUID;
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)objectUID.Length);
            for (uint _i1 = 0;_i1 < objectUID.Length;_i1++)
            {
                if (objectUID[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + objectUID[_i1] + ") on element 1 (starting at 1) of objectUID.");
                }

                writer.WriteVarInt((int)objectUID[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            base.Deserialize(reader);
            uint _objectUIDLen = (uint)reader.ReadUShort();
            objectUID = new int[_objectUIDLen];
            for (uint _i1 = 0;_i1 < _objectUIDLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of objectUID.");
                }

                objectUID[_i1] = (int)_val1;
            }

        }


    }
}








