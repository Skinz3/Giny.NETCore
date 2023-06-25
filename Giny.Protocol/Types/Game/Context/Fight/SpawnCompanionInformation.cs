using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpawnCompanionInformation : SpawnInformation  
    { 
        public new const ushort Id = 8648;
        public override ushort TypeId => Id;

        public byte modelId;
        public short level;
        public double summonerId;
        public double ownerId;

        public SpawnCompanionInformation()
        {
        }
        public SpawnCompanionInformation(byte modelId,short level,double summonerId,double ownerId)
        {
            this.modelId = modelId;
            this.level = level;
            this.summonerId = summonerId;
            this.ownerId = ownerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element modelId.");
            }

            writer.WriteByte((byte)modelId);
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            if (summonerId < -9.00719925474099E+15 || summonerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + summonerId + ") on element summonerId.");
            }

            writer.WriteDouble((double)summonerId);
            if (ownerId < -9.00719925474099E+15 || ownerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element ownerId.");
            }

            writer.WriteDouble((double)ownerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            modelId = (byte)reader.ReadByte();
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element of SpawnCompanionInformation.modelId.");
            }

            level = (short)reader.ReadVarUhShort();
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of SpawnCompanionInformation.level.");
            }

            summonerId = (double)reader.ReadDouble();
            if (summonerId < -9.00719925474099E+15 || summonerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + summonerId + ") on element of SpawnCompanionInformation.summonerId.");
            }

            ownerId = (double)reader.ReadDouble();
            if (ownerId < -9.00719925474099E+15 || ownerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element of SpawnCompanionInformation.ownerId.");
            }

        }


    }
}








