using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildSummaryMessage : PaginationAnswerAbstractMessage  
    { 
        public new const ushort Id = 912;
        public override ushort MessageId => Id;

        public GuildFactSheetInformations[] guilds;

        public GuildSummaryMessage()
        {
        }
        public GuildSummaryMessage(GuildFactSheetInformations[] guilds,double offset,uint count,uint total)
        {
            this.guilds = guilds;
            this.offset = offset;
            this.count = count;
            this.total = total;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)guilds.Length);
            for (uint _i1 = 0;_i1 < guilds.Length;_i1++)
            {
                (guilds[_i1] as GuildFactSheetInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GuildFactSheetInformations _item1 = null;
            base.Deserialize(reader);
            uint _guildsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _guildsLen;_i1++)
            {
                _item1 = new GuildFactSheetInformations();
                _item1.Deserialize(reader);
                guilds[_i1] = _item1;
            }

        }


    }
}








