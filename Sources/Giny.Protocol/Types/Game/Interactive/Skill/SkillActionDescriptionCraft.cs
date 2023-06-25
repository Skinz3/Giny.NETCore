using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SkillActionDescriptionCraft : SkillActionDescription  
    { 
        public new const ushort Id = 2607;
        public override ushort TypeId => Id;

        public byte probability;

        public SkillActionDescriptionCraft()
        {
        }
        public SkillActionDescriptionCraft(byte probability,short skillId)
        {
            this.probability = probability;
            this.skillId = skillId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (probability < 0)
            {
                throw new System.Exception("Forbidden value (" + probability + ") on element probability.");
            }

            writer.WriteByte((byte)probability);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            probability = (byte)reader.ReadByte();
            if (probability < 0)
            {
                throw new System.Exception("Forbidden value (" + probability + ") on element of SkillActionDescriptionCraft.probability.");
            }

        }


    }
}








