using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations  
    { 
        public new const ushort Id = 7122;
        public override ushort TypeId => Id;

        public byte nbWaves;
        public GroupMonsterStaticInformations[] alternatives;

        public GameRolePlayGroupMonsterWaveInformations()
        {
        }
        public GameRolePlayGroupMonsterWaveInformations(byte nbWaves,GroupMonsterStaticInformations[] alternatives,double contextualId,EntityDispositionInformations disposition,EntityLook look,GroupMonsterStaticInformations staticInfos,byte lootShare,byte alignmentSide,bool keyRingBonus,bool hasHardcoreDrop,bool hasAVARewardToken)
        {
            this.nbWaves = nbWaves;
            this.alternatives = alternatives;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.staticInfos = staticInfos;
            this.lootShare = lootShare;
            this.alignmentSide = alignmentSide;
            this.keyRingBonus = keyRingBonus;
            this.hasHardcoreDrop = hasHardcoreDrop;
            this.hasAVARewardToken = hasAVARewardToken;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (nbWaves < 0)
            {
                throw new System.Exception("Forbidden value (" + nbWaves + ") on element nbWaves.");
            }

            writer.WriteByte((byte)nbWaves);
            writer.WriteShort((short)alternatives.Length);
            for (uint _i2 = 0;_i2 < alternatives.Length;_i2++)
            {
                writer.WriteShort((short)(alternatives[_i2] as GroupMonsterStaticInformations).TypeId);
                (alternatives[_i2] as GroupMonsterStaticInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            GroupMonsterStaticInformations _item2 = null;
            base.Deserialize(reader);
            nbWaves = (byte)reader.ReadByte();
            if (nbWaves < 0)
            {
                throw new System.Exception("Forbidden value (" + nbWaves + ") on element of GameRolePlayGroupMonsterWaveInformations.nbWaves.");
            }

            uint _alternativesLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _alternativesLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>((short)_id2);
                _item2.Deserialize(reader);
                alternatives[_i2] = _item2;
            }

        }


    }
}








