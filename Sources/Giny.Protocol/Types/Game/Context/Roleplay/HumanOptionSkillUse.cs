using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionSkillUse : HumanOption  
    { 
        public new const ushort Id = 1568;
        public override ushort TypeId => Id;

        public int elementId;
        public short skillId;
        public double skillEndTime;

        public HumanOptionSkillUse()
        {
        }
        public HumanOptionSkillUse(int elementId,short skillId,double skillEndTime)
        {
            this.elementId = elementId;
            this.skillId = skillId;
            this.skillEndTime = skillEndTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element elementId.");
            }

            writer.WriteVarInt((int)elementId);
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarShort((short)skillId);
            if (skillEndTime < -9.00719925474099E+15 || skillEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + skillEndTime + ") on element skillEndTime.");
            }

            writer.WriteDouble((double)skillEndTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            elementId = (int)reader.ReadVarUhInt();
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element of HumanOptionSkillUse.elementId.");
            }

            skillId = (short)reader.ReadVarUhShort();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of HumanOptionSkillUse.skillId.");
            }

            skillEndTime = (double)reader.ReadDouble();
            if (skillEndTime < -9.00719925474099E+15 || skillEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + skillEndTime + ") on element of HumanOptionSkillUse.skillEndTime.");
            }

        }


    }
}








