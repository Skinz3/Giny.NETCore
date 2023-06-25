using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class InteractiveElementNamedSkill : InteractiveElementSkill  
    { 
        public new const ushort Id = 8189;
        public override ushort TypeId => Id;

        public int nameId;

        public InteractiveElementNamedSkill()
        {
        }
        public InteractiveElementNamedSkill(int nameId,int skillId,int skillInstanceUid)
        {
            this.nameId = nameId;
            this.skillId = skillId;
            this.skillInstanceUid = skillInstanceUid;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (nameId < 0)
            {
                throw new System.Exception("Forbidden value (" + nameId + ") on element nameId.");
            }

            writer.WriteVarInt((int)nameId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            nameId = (int)reader.ReadVarUhInt();
            if (nameId < 0)
            {
                throw new System.Exception("Forbidden value (" + nameId + ") on element of InteractiveElementNamedSkill.nameId.");
            }

        }


    }
}








