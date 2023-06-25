using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildKickRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 983;
        public override ushort MessageId => Id;

        public long kickedId;

        public GuildKickRequestMessage()
        {
        }
        public GuildKickRequestMessage(long kickedId)
        {
            this.kickedId = kickedId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kickedId < 0 || kickedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element kickedId.");
            }

            writer.WriteVarLong((long)kickedId);
        }
        public override void Deserialize(IDataReader reader)
        {
            kickedId = (long)reader.ReadVarUhLong();
            if (kickedId < 0 || kickedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element of GuildKickRequestMessage.kickedId.");
            }

        }


    }
}








