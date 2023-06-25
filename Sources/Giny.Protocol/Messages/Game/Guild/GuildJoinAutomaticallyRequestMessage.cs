using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildJoinAutomaticallyRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3965;
        public override ushort MessageId => Id;

        public int guildId;

        public GuildJoinAutomaticallyRequestMessage()
        {
        }
        public GuildJoinAutomaticallyRequestMessage(int guildId)
        {
            this.guildId = guildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)guildId);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildId = (int)reader.ReadInt();
        }


    }
}








