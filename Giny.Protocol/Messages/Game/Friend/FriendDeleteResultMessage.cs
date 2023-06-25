using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendDeleteResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 7028;
        public override ushort MessageId => Id;

        public bool success;
        public AccountTagInformation tag;

        public FriendDeleteResultMessage()
        {
        }
        public FriendDeleteResultMessage(bool success,AccountTagInformation tag)
        {
            this.success = success;
            this.tag = tag;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)success);
            tag.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            success = (bool)reader.ReadBoolean();
            tag = new AccountTagInformation();
            tag.Deserialize(reader);
        }


    }
}








