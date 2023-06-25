using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendStatusShareStateMessage : NetworkMessage  
    { 
        public  const ushort Id = 3877;
        public override ushort MessageId => Id;

        public bool share;

        public FriendStatusShareStateMessage()
        {
        }
        public FriendStatusShareStateMessage(bool share)
        {
            this.share = share;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)share);
        }
        public override void Deserialize(IDataReader reader)
        {
            share = (bool)reader.ReadBoolean();
        }


    }
}








