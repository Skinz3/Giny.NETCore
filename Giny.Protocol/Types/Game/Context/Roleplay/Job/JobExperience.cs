using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobExperience  
    { 
        public const ushort Id = 2255;
        public virtual ushort TypeId => Id;

        public byte jobId;
        public byte jobLevel;
        public long jobXP;
        public long jobXpLevelFloor;
        public long jobXpNextLevelFloor;

        public JobExperience()
        {
        }
        public JobExperience(byte jobId,byte jobLevel,long jobXP,long jobXpLevelFloor,long jobXpNextLevelFloor)
        {
            this.jobId = jobId;
            this.jobLevel = jobLevel;
            this.jobXP = jobXP;
            this.jobXpLevelFloor = jobXpLevelFloor;
            this.jobXpNextLevelFloor = jobXpNextLevelFloor;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            if (jobLevel < 0 || jobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + jobLevel + ") on element jobLevel.");
            }

            writer.WriteByte((byte)jobLevel);
            if (jobXP < 0 || jobXP > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXP + ") on element jobXP.");
            }

            writer.WriteVarLong((long)jobXP);
            if (jobXpLevelFloor < 0 || jobXpLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXpLevelFloor + ") on element jobXpLevelFloor.");
            }

            writer.WriteVarLong((long)jobXpLevelFloor);
            if (jobXpNextLevelFloor < 0 || jobXpNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXpNextLevelFloor + ") on element jobXpNextLevelFloor.");
            }

            writer.WriteVarLong((long)jobXpNextLevelFloor);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobExperience.jobId.");
            }

            jobLevel = (byte)reader.ReadSByte();
            if (jobLevel < 0 || jobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + jobLevel + ") on element of JobExperience.jobLevel.");
            }

            jobXP = (long)reader.ReadVarUhLong();
            if (jobXP < 0 || jobXP > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXP + ") on element of JobExperience.jobXP.");
            }

            jobXpLevelFloor = (long)reader.ReadVarUhLong();
            if (jobXpLevelFloor < 0 || jobXpLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXpLevelFloor + ") on element of JobExperience.jobXpLevelFloor.");
            }

            jobXpNextLevelFloor = (long)reader.ReadVarUhLong();
            if (jobXpNextLevelFloor < 0 || jobXpNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + jobXpNextLevelFloor + ") on element of JobExperience.jobXpNextLevelFloor.");
            }

        }


    }
}








