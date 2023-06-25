using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildJoinedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2641;
        public override ushort MessageId => Id;

        public GuildInformations guildInfo;
        public int rankId;

        public GuildJoinedMessage()
        {
        }
        public GuildJoinedMessage(GuildInformations guildInfo,int rankId)
        {
            this.guildInfo = guildInfo;
            this.rankId = rankId;
        }
        public override void Serialize(IDataWriter writer)
        {
            guildInfo.Serialize(writer);
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of GuildJoinedMessage.rankId.");
            }

        }


    }
}








