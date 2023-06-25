using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightAIInformations : GameFightFighterInformations  
    { 
        public new const ushort Id = 1060;
        public override ushort TypeId => Id;


        public GameFightAIInformations()
        {
        }
        public GameFightAIInformations(double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions)
        {
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.spawnInfo = spawnInfo;
            this.wave = wave;
            this.stats = stats;
            this.previousPositions = previousPositions;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








