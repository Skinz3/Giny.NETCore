using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildMemberOnlineStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 4229;
        public override ushort MessageId => Id;

        public long memberId;
        public bool online;

        public GuildMemberOnlineStatusMessage()
        {
        }
        public GuildMemberOnlineStatusMessage(long memberId,bool online)
        {
            this.memberId = memberId;
            this.online = online;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteBoolean((bool)online);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of GuildMemberOnlineStatusMessage.memberId.");
            }

            online = (bool)reader.ReadBoolean();
        }


    }
}








