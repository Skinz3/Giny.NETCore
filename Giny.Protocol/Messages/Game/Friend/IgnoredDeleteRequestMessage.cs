using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IgnoredDeleteRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6327;
        public override ushort MessageId => Id;

        public int accountId;
        public bool session;

        public IgnoredDeleteRequestMessage()
        {
        }
        public IgnoredDeleteRequestMessage(int accountId,bool session)
        {
            this.accountId = accountId;
            this.session = session;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            writer.WriteBoolean((bool)session);
        }
        public override void Deserialize(IDataReader reader)
        {
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of IgnoredDeleteRequestMessage.accountId.");
            }

            session = (bool)reader.ReadBoolean();
        }


    }
}








