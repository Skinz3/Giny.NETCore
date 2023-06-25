using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightMonsterWithAlignmentInformations : GameFightMonsterInformations  
    { 
        public new const ushort Id = 1061;
        public override ushort TypeId => Id;

        public ActorAlignmentInformations alignmentInfos;

        public GameFightMonsterWithAlignmentInformations()
        {
        }
        public GameFightMonsterWithAlignmentInformations(ActorAlignmentInformations alignmentInfos,double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions,short creatureGenericId,byte creatureGrade,short creatureLevel)
        {
            this.alignmentInfos = alignmentInfos;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.spawnInfo = spawnInfo;
            this.wave = wave;
            this.stats = stats;
            this.previousPositions = previousPositions;
            this.creatureGenericId = creatureGenericId;
            this.creatureGrade = creatureGrade;
            this.creatureLevel = creatureLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alignmentInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alignmentInfos = new ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);
        }


    }
}








