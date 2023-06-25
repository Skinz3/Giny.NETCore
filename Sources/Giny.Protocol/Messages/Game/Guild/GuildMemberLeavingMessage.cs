using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildMemberLeavingMessage : NetworkMessage  
    { 
        public  const ushort Id = 6137;
        public override ushort MessageId => Id;

        public bool kicked;
        public long memberId;

        public GuildMemberLeavingMessage()
        {
        }
        public GuildMemberLeavingMessage(bool kicked,long memberId)
        {
            this.kicked = kicked;
            this.memberId = memberId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)kicked);
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
        }
        public override void Deserialize(IDataReader reader)
        {
            kicked = (bool)reader.ReadBoolean();
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of GuildMemberLeavingMessage.memberId.");
            }

        }


    }
}








