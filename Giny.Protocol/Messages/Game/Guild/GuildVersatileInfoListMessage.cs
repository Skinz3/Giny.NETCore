using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildVersatileInfoListMessage : NetworkMessage  
    { 
        public  const ushort Id = 6214;
        public override ushort MessageId => Id;

        public GuildVersatileInformations[] guilds;

        public GuildVersatileInfoListMessage()
        {
        }
        public GuildVersatileInfoListMessage(GuildVersatileInformations[] guilds)
        {
            this.guilds = guilds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)guilds.Length);
            for (uint _i1 = 0;_i1 < guilds.Length;_i1++)
            {
                writer.WriteShort((short)(guilds[_i1] as GuildVersatileInformations).TypeId);
                (guilds[_i1] as GuildVersatileInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GuildVersatileInformations _item1 = null;
            uint _guildsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _guildsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GuildVersatileInformations>((short)_id1);
                _item1.Deserialize(reader);
                guilds[_i1] = _item1;
            }

        }


    }
}








