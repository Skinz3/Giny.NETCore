using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PrismSubareaEmptyInfo  
    { 
        public const ushort Id = 3717;
        public virtual ushort TypeId => Id;

        public short subAreaId;
        public int allianceId;

        public PrismSubareaEmptyInfo()
        {
        }
        public PrismSubareaEmptyInfo(short subAreaId,int allianceId)
        {
            this.subAreaId = subAreaId;
            this.allianceId = allianceId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismSubareaEmptyInfo.subAreaId.");
            }

            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of PrismSubareaEmptyInfo.allianceId.");
            }

        }


    }
}








