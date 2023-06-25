using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StatedMapUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 1576;
        public override ushort MessageId => Id;

        public StatedElement[] statedElements;

        public StatedMapUpdateMessage()
        {
        }
        public StatedMapUpdateMessage(StatedElement[] statedElements)
        {
            this.statedElements = statedElements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)statedElements.Length);
            for (uint _i1 = 0;_i1 < statedElements.Length;_i1++)
            {
                (statedElements[_i1] as StatedElement).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            StatedElement _item1 = null;
            uint _statedElementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _statedElementsLen;_i1++)
            {
                _item1 = new StatedElement();
                _item1.Deserialize(reader);
                statedElements[_i1] = _item1;
            }

        }


    }
}








