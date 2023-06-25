using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AcquaintanceAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2313;
        public override ushort MessageId => Id;

        public AcquaintanceInformation acquaintanceAdded;

        public AcquaintanceAddedMessage()
        {
        }
        public AcquaintanceAddedMessage(AcquaintanceInformation acquaintanceAdded)
        {
            this.acquaintanceAdded = acquaintanceAdded;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)acquaintanceAdded.TypeId);
            acquaintanceAdded.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            acquaintanceAdded = ProtocolTypeManager.GetInstance<AcquaintanceInformation>((short)_id1);
            acquaintanceAdded.Deserialize(reader);
        }


    }
}








