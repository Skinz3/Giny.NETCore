using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildFactsMessage : NetworkMessage
    {
        public const ushort Id = 9389;
        public override ushort MessageId => Id;

        public GuildFactSheetInformations infos;
        public int creationDate;
        public CharacterMinimalSocialPublicInformations[] members;

        public GuildFactsMessage()
        {
        }
        public GuildFactsMessage(GuildFactSheetInformations infos, int creationDate, CharacterMinimalSocialPublicInformations[] members)
        {
            this.infos = infos;
            this.creationDate = creationDate;
            this.members = members;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)infos.TypeId);
            infos.Serialize(writer);
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element creationDate.");
            }

            writer.WriteInt((int)creationDate);
            writer.WriteShort((short)members.Length);
            for (uint _i3 = 0; _i3 < members.Length; _i3++)
            {
                (members[_i3] as CharacterMinimalSocialPublicInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            CharacterMinimalSocialPublicInformations _item3 = null;
            uint _id1 = (uint)reader.ReadUShort();
            infos = ProtocolTypeManager.GetInstance<GuildFactSheetInformations>((short)_id1);
            infos.Deserialize(reader);
            creationDate = (int)reader.ReadInt();
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of GuildFactsMessage.creationDate.");
            }

            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0; _i3 < _membersLen; _i3++)
            {
                _item3 = new CharacterMinimalSocialPublicInformations();
                _item3.Deserialize(reader);
                members[_i3] = _item3;
            }

        }

    }
}


