using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightCharacterInformations : GameFightFighterNamedInformations  
    { 
        public new const ushort Id = 2596;
        public override ushort TypeId => Id;

        public short level;
        public ActorAlignmentInformations alignmentInfos;
        public byte breed;
        public bool sex;

        public GameFightCharacterInformations()
        {
        }
        public GameFightCharacterInformations(short level,ActorAlignmentInformations alignmentInfos,byte breed,bool sex,double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions,string name,PlayerStatus status,short leagueId,int ladderPosition,bool hiddenInPrefight)
        {
            this.level = level;
            this.alignmentInfos = alignmentInfos;
            this.breed = breed;
            this.sex = sex;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.spawnInfo = spawnInfo;
            this.wave = wave;
            this.stats = stats;
            this.previousPositions = previousPositions;
            this.name = name;
            this.status = status;
            this.leagueId = leagueId;
            this.ladderPosition = ladderPosition;
            this.hiddenInPrefight = hiddenInPrefight;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            alignmentInfos.Serialize(writer);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of GameFightCharacterInformations.level.");
            }

            alignmentInfos = new ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);
            breed = (byte)reader.ReadByte();
            sex = (bool)reader.ReadBoolean();
        }


    }
}








