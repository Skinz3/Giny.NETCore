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
        public  const ushort Id = 1750;
        public override ushort MessageId => Id;

        public GuildFactSheetInformations infos;
        public int creationDate;
        public short nbTaxCollectors;
        public CharacterMinimalGuildPublicInformations[] members;

        public GuildFactsMessage()
        {
        }
        public GuildFactsMessage(GuildFactSheetInformations infos,int creationDate,short nbTaxCollectors,CharacterMinimalGuildPublicInformations[] members)
        {
            this.infos = infos;
            this.creationDate = creationDate;
            this.nbTaxCollectors = nbTaxCollectors;
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
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element nbTaxCollectors.");
            }

            writer.WriteVarShort((short)nbTaxCollectors);
            writer.WriteShort((short)members.Length);
            for (uint _i4 = 0;_i4 < members.Length;_i4++)
            {
                (members[_i4] as CharacterMinimalGuildPublicInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            CharacterMinimalGuildPublicInformations _item4 = null;
            uint _id1 = (uint)reader.ReadUShort();
            infos = ProtocolTypeManager.GetInstance<GuildFactSheetInformations>((short)_id1);
            infos.Deserialize(reader);
            creationDate = (int)reader.ReadInt();
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of GuildFactsMessage.creationDate.");
            }

            nbTaxCollectors = (short)reader.ReadVarUhShort();
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element of GuildFactsMessage.nbTaxCollectors.");
            }

            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _membersLen;_i4++)
            {
                _item4 = new CharacterMinimalGuildPublicInformations();
                _item4.Deserialize(reader);
                members[_i4] = _item4;
            }

        }


    }
}








