using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PlayerStatusUpdateRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 27;
        public override ushort MessageId => Id;

        public PlayerStatus status;

        public PlayerStatusUpdateRequestMessage()
        {
        }
        public PlayerStatusUpdateRequestMessage(PlayerStatus status)
        {
            this.status = status;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id1);
            status.Deserialize(reader);
        }


    }
}








