using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class InteractiveElementSkill  
    { 
        public const ushort Id = 4754;
        public virtual ushort TypeId => Id;

        public int skillId;
        public int skillInstanceUid;

        public InteractiveElementSkill()
        {
        }
        public InteractiveElementSkill(int skillId,int skillInstanceUid)
        {
            this.skillId = skillId;
            this.skillInstanceUid = skillInstanceUid;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarInt((int)skillId);
            if (skillInstanceUid < 0)
            {
                throw new System.Exception("Forbidden value (" + skillInstanceUid + ") on element skillInstanceUid.");
            }

            writer.WriteInt((int)skillInstanceUid);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            skillId = (int)reader.ReadVarUhInt();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of InteractiveElementSkill.skillId.");
            }

            skillInstanceUid = (int)reader.ReadInt();
            if (skillInstanceUid < 0)
            {
                throw new System.Exception("Forbidden value (" + skillInstanceUid + ") on element of InteractiveElementSkill.skillInstanceUid.");
            }

        }


    }
}








