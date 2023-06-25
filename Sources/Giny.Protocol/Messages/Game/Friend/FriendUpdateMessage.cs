using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7984;
        public override ushort MessageId => Id;

        public FriendInformations friendUpdated;

        public FriendUpdateMessage()
        {
        }
        public FriendUpdateMessage(FriendInformations friendUpdated)
        {
            this.friendUpdated = friendUpdated;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)friendUpdated.TypeId);
            friendUpdated.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            friendUpdated = ProtocolTypeManager.GetInstance<FriendInformations>((short)_id1);
            friendUpdated.Deserialize(reader);
        }


    }
}








