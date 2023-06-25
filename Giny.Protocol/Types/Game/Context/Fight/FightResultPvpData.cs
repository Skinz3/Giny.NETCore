using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultPvpData : FightResultAdditionalData  
    { 
        public new const ushort Id = 9448;
        public override ushort TypeId => Id;

        public byte grade;
        public short minHonorForGrade;
        public short maxHonorForGrade;
        public short honor;
        public short honorDelta;

        public FightResultPvpData()
        {
        }
        public FightResultPvpData(byte grade,short minHonorForGrade,short maxHonorForGrade,short honor,short honorDelta)
        {
            this.grade = grade;
            this.minHonorForGrade = minHonorForGrade;
            this.maxHonorForGrade = maxHonorForGrade;
            this.honor = honor;
            this.honorDelta = honorDelta;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (grade < 0 || grade > 255)
            {
                throw new System.Exception("Forbidden value (" + grade + ") on element grade.");
            }

            writer.WriteByte((byte)grade);
            if (minHonorForGrade < 0 || minHonorForGrade > 20000)
            {
                throw new System.Exception("Forbidden value (" + minHonorForGrade + ") on element minHonorForGrade.");
            }

            writer.WriteVarShort((short)minHonorForGrade);
            if (maxHonorForGrade < 0 || maxHonorForGrade > 20000)
            {
                throw new System.Exception("Forbidden value (" + maxHonorForGrade + ") on element maxHonorForGrade.");
            }

            writer.WriteVarShort((short)maxHonorForGrade);
            if (honor < 0 || honor > 20000)
            {
                throw new System.Exception("Forbidden value (" + honor + ") on element honor.");
            }

            writer.WriteVarShort((short)honor);
            writer.WriteVarShort((short)honorDelta);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            grade = (byte)reader.ReadSByte();
            if (grade < 0 || grade > 255)
            {
                throw new System.Exception("Forbidden value (" + grade + ") on element of FightResultPvpData.grade.");
            }

            minHonorForGrade = (short)reader.ReadVarUhShort();
            if (minHonorForGrade < 0 || minHonorForGrade > 20000)
            {
                throw new System.Exception("Forbidden value (" + minHonorForGrade + ") on element of FightResultPvpData.minHonorForGrade.");
            }

            maxHonorForGrade = (short)reader.ReadVarUhShort();
            if (maxHonorForGrade < 0 || maxHonorForGrade > 20000)
            {
                throw new System.Exception("Forbidden value (" + maxHonorForGrade + ") on element of FightResultPvpData.maxHonorForGrade.");
            }

            honor = (short)reader.ReadVarUhShort();
            if (honor < 0 || honor > 20000)
            {
                throw new System.Exception("Forbidden value (" + honor + ") on element of FightResultPvpData.honor.");
            }

            honorDelta = (short)reader.ReadVarShort();
        }


    }
}








