using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFactsErrorMessage : NetworkMessage  
    { 
        public  const ushort Id = 3499;
        public override ushort MessageId => Id;

        public int guildId;

        public GuildFactsErrorMessage()
        {
        }
        public GuildFactsErrorMessage(int guildId)
        {
            this.guildId = guildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of GuildFactsSystem.ExceptionMessage.guildId.");
            }

        }


    }
}








