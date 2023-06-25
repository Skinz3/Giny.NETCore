using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4007;
        public override ushort MessageId => Id;

        public FriendInformations friendAdded;

        public FriendAddedMessage()
        {
        }
        public FriendAddedMessage(FriendInformations friendAdded)
        {
            this.friendAdded = friendAdded;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)friendAdded.TypeId);
            friendAdded.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            friendAdded = ProtocolTypeManager.GetInstance<FriendInformations>((short)_id1);
            friendAdded.Deserialize(reader);
        }


    }
}








