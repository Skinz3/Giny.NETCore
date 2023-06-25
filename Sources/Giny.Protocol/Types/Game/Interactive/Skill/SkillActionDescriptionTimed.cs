using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SkillActionDescriptionTimed : SkillActionDescription  
    { 
        public new const ushort Id = 687;
        public override ushort TypeId => Id;

        public byte time;

        public SkillActionDescriptionTimed()
        {
        }
        public SkillActionDescriptionTimed(byte time,short skillId)
        {
            this.time = time;
            this.skillId = skillId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (time < 0 || time > 255)
            {
                throw new System.Exception("Forbidden value (" + time + ") on element time.");
            }

            writer.WriteByte((byte)time);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            time = (byte)reader.ReadSByte();
            if (time < 0 || time > 255)
            {
                throw new System.Exception("Forbidden value (" + time + ") on element of SkillActionDescriptionTimed.time.");
            }

        }


    }
}








