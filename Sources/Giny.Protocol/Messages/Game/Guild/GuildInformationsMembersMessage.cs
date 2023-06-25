using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInformationsMembersMessage : NetworkMessage  
    { 
        public  const ushort Id = 6736;
        public override ushort MessageId => Id;

        public GuildMember[] members;

        public GuildInformationsMembersMessage()
        {
        }
        public GuildInformationsMembersMessage(GuildMember[] members)
        {
            this.members = members;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)members.Length);
            for (uint _i1 = 0;_i1 < members.Length;_i1++)
            {
                (members[_i1] as GuildMember).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GuildMember _item1 = null;
            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _membersLen;_i1++)
            {
                _item1 = new GuildMember();
                _item1.Deserialize(reader);
                members[_i1] = _item1;
            }

        }


    }
}








