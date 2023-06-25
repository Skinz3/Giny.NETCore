using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PrismInformation  
    { 
        public const ushort Id = 2401;
        public virtual ushort TypeId => Id;

        public byte typeId;
        public byte state;
        public int nextVulnerabilityDate;
        public int placementDate;
        public int rewardTokenCount;

        public PrismInformation()
        {
        }
        public PrismInformation(byte typeId,byte state,int nextVulnerabilityDate,int placementDate,int rewardTokenCount)
        {
            this.typeId = typeId;
            this.state = state;
            this.nextVulnerabilityDate = nextVulnerabilityDate;
            this.placementDate = placementDate;
            this.rewardTokenCount = rewardTokenCount;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (typeId < 0)
            {
                throw new System.Exception("Forbidden value (" + typeId + ") on element typeId.");
            }

            writer.WriteByte((byte)typeId);
            writer.WriteByte((byte)state);
            if (nextVulnerabilityDate < 0)
            {
                throw new System.Exception("Forbidden value (" + nextVulnerabilityDate + ") on element nextVulnerabilityDate.");
            }

            writer.WriteInt((int)nextVulnerabilityDate);
            if (placementDate < 0)
            {
                throw new System.Exception("Forbidden value (" + placementDate + ") on element placementDate.");
            }

            writer.WriteInt((int)placementDate);
            if (rewardTokenCount < 0)
            {
                throw new System.Exception("Forbidden value (" + rewardTokenCount + ") on element rewardTokenCount.");
            }

            writer.WriteVarInt((int)rewardTokenCount);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            typeId = (byte)reader.ReadByte();
            if (typeId < 0)
            {
                throw new System.Exception("Forbidden value (" + typeId + ") on element of PrismInformation.typeId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of PrismInformation.state.");
            }

            nextVulnerabilityDate = (int)reader.ReadInt();
            if (nextVulnerabilityDate < 0)
            {
                throw new System.Exception("Forbidden value (" + nextVulnerabilityDate + ") on element of PrismInformation.nextVulnerabilityDate.");
            }

            placementDate = (int)reader.ReadInt();
            if (placementDate < 0)
            {
                throw new System.Exception("Forbidden value (" + placementDate + ") on element of PrismInformation.placementDate.");
            }

            rewardTokenCount = (int)reader.ReadVarUhInt();
            if (rewardTokenCount < 0)
            {
                throw new System.Exception("Forbidden value (" + rewardTokenCount + ") on element of PrismInformation.rewardTokenCount.");
            }

        }


    }
}








