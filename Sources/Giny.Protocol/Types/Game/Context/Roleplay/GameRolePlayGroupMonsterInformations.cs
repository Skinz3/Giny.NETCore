using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
    {
        public new const ushort Id = 7008;
        public override ushort TypeId => Id;

        public byte lootShare;
        public byte alignmentSide;
        public bool hasHardcoreDrop;
        public GroupMonsterStaticInformations staticInfos;

        public GameRolePlayGroupMonsterInformations()
        {
        }
        public GameRolePlayGroupMonsterInformations(byte lootShare, byte alignmentSide, bool hasHardcoreDrop, GroupMonsterStaticInformations staticInfos, double contextualId, EntityDispositionInformations disposition, EntityLook look)
        {
            this.lootShare = lootShare;
            this.alignmentSide = alignmentSide;
            this.hasHardcoreDrop = hasHardcoreDrop;
            this.staticInfos = staticInfos;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)staticInfos.TypeId);
            staticInfos.Serialize(writer);
            if (lootShare < -1 || lootShare > 8)
            {
                throw new System.Exception("Forbidden value (" + lootShare + ") on element lootShare.");
            }

            writer.WriteByte((byte)lootShare);
            writer.WriteByte((byte)alignmentSide);
            writer.WriteBoolean((bool)hasHardcoreDrop);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            staticInfos = ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>((short)_id1);
            staticInfos.Deserialize(reader);
            lootShare = (byte)reader.ReadByte();
            if (lootShare < -1 || lootShare > 8)
            {
                throw new System.Exception("Forbidden value (" + lootShare + ") on element of GameRolePlayGroupMonsterInformations.lootShare.");
            }

            alignmentSide = (byte)reader.ReadByte();
            hasHardcoreDrop = (bool)reader.ReadBoolean();
        }


    }
}


