using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFactsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5203;
        public override ushort MessageId => Id;

        public int guildId;

        public GuildFactsRequestMessage()
        {
        }
        public GuildFactsRequestMessage(int guildId)
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
                throw new System.Exception("Forbidden value (" + guildId + ") on element of GuildFactsRequestMessage.guildId.");
            }

        }


    }
}








