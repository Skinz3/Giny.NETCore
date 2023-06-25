using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextRemoveMultipleElementsWithEventsMessage : GameContextRemoveMultipleElementsMessage  
    { 
        public new const ushort Id = 409;
        public override ushort MessageId => Id;

        public byte[] elementEventIds;

        public GameContextRemoveMultipleElementsWithEventsMessage()
        {
        }
        public GameContextRemoveMultipleElementsWithEventsMessage(byte[] elementEventIds,double[] elementsIds)
        {
            this.elementEventIds = elementEventIds;
            this.elementsIds = elementsIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)elementEventIds.Length);
            for (uint _i1 = 0;_i1 < elementEventIds.Length;_i1++)
            {
                if (elementEventIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + elementEventIds[_i1] + ") on element 1 (starting at 1) of elementEventIds.");
                }

                writer.WriteByte((byte)elementEventIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            base.Deserialize(reader);
            uint _elementEventIdsLen = (uint)reader.ReadUShort();
            elementEventIds = new byte[_elementEventIdsLen];
            for (uint _i1 = 0;_i1 < _elementEventIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadByte();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of elementEventIds.");
                }

                elementEventIds[_i1] = (byte)_val1;
            }

        }


    }
}








