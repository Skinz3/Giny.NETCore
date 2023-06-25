using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightMutantInformations : GameFightFighterNamedInformations  
    { 
        public new const ushort Id = 1606;
        public override ushort TypeId => Id;

        public byte powerLevel;

        public GameFightMutantInformations()
        {
        }
        public GameFightMutantInformations(byte powerLevel,double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions,string name,PlayerStatus status,short leagueId,int ladderPosition,bool hiddenInPrefight)
        {
            this.powerLevel = powerLevel;
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
            if (powerLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + powerLevel + ") on element powerLevel.");
            }

            writer.WriteByte((byte)powerLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            powerLevel = (byte)reader.ReadByte();
            if (powerLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + powerLevel + ") on element of GameFightMutantInformations.powerLevel.");
            }

        }


    }
}








