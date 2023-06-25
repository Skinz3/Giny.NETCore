using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AlignmentWarEffortInformation  
    { 
        public const ushort Id = 9705;
        public virtual ushort TypeId => Id;

        public byte alignmentSide;
        public long alignmentWarEffort;

        public AlignmentWarEffortInformation()
        {
        }
        public AlignmentWarEffortInformation(byte alignmentSide,long alignmentWarEffort)
        {
            this.alignmentSide = alignmentSide;
            this.alignmentWarEffort = alignmentWarEffort;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)alignmentSide);
            if (alignmentWarEffort < 0 || alignmentWarEffort > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffort + ") on element alignmentWarEffort.");
            }

            writer.WriteVarLong((long)alignmentWarEffort);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            alignmentSide = (byte)reader.ReadByte();
            alignmentWarEffort = (long)reader.ReadVarUhLong();
            if (alignmentWarEffort < 0 || alignmentWarEffort > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffort + ") on element of AlignmentWarEffortInformation.alignmentWarEffort.");
            }

        }


    }
}








