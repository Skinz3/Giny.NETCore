using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendDeleteRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5058;
        public override ushort MessageId => Id;

        public int accountId;

        public FriendDeleteRequestMessage()
        {
        }
        public FriendDeleteRequestMessage(int accountId)
        {
            this.accountId = accountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of FriendDeleteRequestMessage.accountId.");
            }

        }


    }
}








