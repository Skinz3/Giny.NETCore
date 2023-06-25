using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildListMessage : NetworkMessage  
    { 
        public  const ushort Id = 3993;
        public override ushort MessageId => Id;

        public GuildInformations[] guilds;

        public GuildListMessage()
        {
        }
        public GuildListMessage(GuildInformations[] guilds)
        {
            this.guilds = guilds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)guilds.Length);
            for (uint _i1 = 0;_i1 < guilds.Length;_i1++)
            {
                (guilds[_i1] as GuildInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GuildInformations _item1 = null;
            uint _guildsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _guildsLen;_i1++)
            {
                _item1 = new GuildInformations();
                _item1.Deserialize(reader);
                guilds[_i1] = _item1;
            }

        }


    }
}








