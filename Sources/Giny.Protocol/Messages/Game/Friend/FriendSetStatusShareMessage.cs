using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendSetStatusShareMessage : NetworkMessage  
    { 
        public  const ushort Id = 9293;
        public override ushort MessageId => Id;

        public bool share;

        public FriendSetStatusShareMessage()
        {
        }
        public FriendSetStatusShareMessage(bool share)
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








