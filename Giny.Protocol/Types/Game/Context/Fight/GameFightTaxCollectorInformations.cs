using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightTaxCollectorInformations : GameFightAIInformations  
    { 
        public new const ushort Id = 3419;
        public override ushort TypeId => Id;

        public short firstNameId;
        public short lastNameId;
        public byte level;

        public GameFightTaxCollectorInformations()
        {
        }
        public GameFightTaxCollectorInformations(short firstNameId,short lastNameId,byte level,double contextualId,EntityDispositionInformations disposition,EntityLook look,GameContextBasicSpawnInformation spawnInfo,byte wave,GameFightCharacteristics stats,short[] previousPositions)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.level = level;
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
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element firstNameId.");
            }

            writer.WriteVarShort((short)firstNameId);
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element lastNameId.");
            }

            writer.WriteVarShort((short)lastNameId);
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            firstNameId = (short)reader.ReadVarUhShort();
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element of GameFightTaxCollectorInformations.firstNameId.");
            }

            lastNameId = (short)reader.ReadVarUhShort();
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element of GameFightTaxCollectorInformations.lastNameId.");
            }

            level = (byte)reader.ReadSByte();
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of GameFightTaxCollectorInformations.level.");
            }

        }


    }
}








