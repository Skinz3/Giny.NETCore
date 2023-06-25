using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InteractiveMapUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 8782;
        public override ushort MessageId => Id;

        public InteractiveElement[] interactiveElements;

        public InteractiveMapUpdateMessage()
        {
        }
        public InteractiveMapUpdateMessage(InteractiveElement[] interactiveElements)
        {
            this.interactiveElements = interactiveElements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)interactiveElements.Length);
            for (uint _i1 = 0;_i1 < interactiveElements.Length;_i1++)
            {
                writer.WriteShort((short)(interactiveElements[_i1] as InteractiveElement).TypeId);
                (interactiveElements[_i1] as InteractiveElement).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            InteractiveElement _item1 = null;
            uint _interactiveElementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _interactiveElementsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<InteractiveElement>((short)_id1);
                _item1.Deserialize(reader);
                interactiveElements[_i1] = _item1;
            }

        }


    }
}








