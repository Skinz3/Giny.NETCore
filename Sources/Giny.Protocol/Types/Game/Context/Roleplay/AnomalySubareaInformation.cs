using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AnomalySubareaInformation  
    { 
        public const ushort Id = 6099;
        public virtual ushort TypeId => Id;

        public short subAreaId;
        public short rewardRate;
        public bool hasAnomaly;
        public long anomalyClosingTime;

        public AnomalySubareaInformation()
        {
        }
        public AnomalySubareaInformation(short subAreaId,short rewardRate,bool hasAnomaly,long anomalyClosingTime)
        {
            this.subAreaId = subAreaId;
            this.rewardRate = rewardRate;
            this.hasAnomaly = hasAnomaly;
            this.anomalyClosingTime = anomalyClosingTime;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteVarShort((short)rewardRate);
            writer.WriteBoolean((bool)hasAnomaly);
            if (anomalyClosingTime < 0 || anomalyClosingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + anomalyClosingTime + ") on element anomalyClosingTime.");
            }

            writer.WriteVarLong((long)anomalyClosingTime);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of AnomalySubareaInformation.subAreaId.");
            }

            rewardRate = (short)reader.ReadVarShort();
            hasAnomaly = (bool)reader.ReadBoolean();
            anomalyClosingTime = (long)reader.ReadVarUhLong();
            if (anomalyClosingTime < 0 || anomalyClosingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + anomalyClosingTime + ") on element of AnomalySubareaInformation.anomalyClosingTime.");
            }

        }


    }
}








