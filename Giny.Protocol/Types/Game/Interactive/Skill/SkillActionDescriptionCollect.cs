using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SkillActionDescriptionCollect : SkillActionDescriptionTimed  
    { 
        public new const ushort Id = 8067;
        public override ushort TypeId => Id;

        public short min;
        public short max;

        public SkillActionDescriptionCollect()
        {
        }
        public SkillActionDescriptionCollect(short min,short max,short skillId,byte time)
        {
            this.min = min;
            this.max = max;
            this.skillId = skillId;
            this.time = time;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (min < 0)
            {
                throw new System.Exception("Forbidden value (" + min + ") on element min.");
            }

            writer.WriteVarShort((short)min);
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element max.");
            }

            writer.WriteVarShort((short)max);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            min = (short)reader.ReadVarUhShort();
            if (min < 0)
            {
                throw new System.Exception("Forbidden value (" + min + ") on element of SkillActionDescriptionCollect.min.");
            }

            max = (short)reader.ReadVarUhShort();
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element of SkillActionDescriptionCollect.max.");
            }

        }


    }
}








