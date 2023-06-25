using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateGuildRankRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1215;
        public override ushort MessageId => Id;

        public GuildRankInformation rank;

        public UpdateGuildRankRequestMessage()
        {
        }
        public UpdateGuildRankRequestMessage(GuildRankInformation rank)
        {
            this.rank = rank;
        }
        public override void Serialize(IDataWriter writer)
        {
            rank.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            rank = new GuildRankInformation();
            rank.Deserialize(reader);
        }


    }
}








