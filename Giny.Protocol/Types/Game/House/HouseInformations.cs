using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HouseInformations  
    { 
        public const ushort Id = 1025;
        public virtual ushort TypeId => Id;

        public int houseId;
        public short modelId;

        public HouseInformations()
        {
        }
        public HouseInformations(int houseId,short modelId)
        {
            this.houseId = houseId;
            this.modelId = modelId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element modelId.");
            }

            writer.WriteVarShort((short)modelId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseInformations.houseId.");
            }

            modelId = (short)reader.ReadVarUhShort();
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element of HouseInformations.modelId.");
            }

        }


    }
}








