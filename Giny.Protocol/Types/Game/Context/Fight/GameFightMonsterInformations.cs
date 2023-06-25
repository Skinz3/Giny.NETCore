using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightMonsterInformations : GameFightAIInformations  
    { 
        public new const ushort Id = 9097;
        public override ushort TypeId => Id;

        public short creatureGenericId;
        public byte creatureGrade;
        public short creatureLevel;

        public GameFightMonsterInformations()
        {
        }
        public GameFightMonsterInformations(short creatureGenericId,byte creatureGrade,short creatureLevel,double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions)
        {
            this.creatureGenericId = creatureGenericId;
            this.creatureGrade = creatureGrade;
            this.creatureLevel = creatureLevel;
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
            if (creatureGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGenericId + ") on element creatureGenericId.");
            }

            writer.WriteVarShort((short)creatureGenericId);
            if (creatureGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGrade + ") on element creatureGrade.");
            }

            writer.WriteByte((byte)creatureGrade);
            if (creatureLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureLevel + ") on element creatureLevel.");
            }

            writer.WriteShort((short)creatureLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creatureGenericId = (short)reader.ReadVarUhShort();
            if (creatureGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGenericId + ") on element of GameFightMonsterInformations.creatureGenericId.");
            }

            creatureGrade = (byte)reader.ReadByte();
            if (creatureGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGrade + ") on element of GameFightMonsterInformations.creatureGrade.");
            }

            creatureLevel = (short)reader.ReadShort();
            if (creatureLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureLevel + ") on element of GameFightMonsterInformations.creatureLevel.");
            }

        }


    }
}








