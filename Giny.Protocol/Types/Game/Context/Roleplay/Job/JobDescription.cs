using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobDescription  
    { 
        public const ushort Id = 6947;
        public virtual ushort TypeId => Id;

        public byte jobId;
        public SkillActionDescription[] skills;

        public JobDescription()
        {
        }
        public JobDescription(byte jobId,SkillActionDescription[] skills)
        {
            this.jobId = jobId;
            this.skills = skills;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            writer.WriteShort((short)skills.Length);
            for (uint _i2 = 0;_i2 < skills.Length;_i2++)
            {
                writer.WriteShort((short)(skills[_i2] as SkillActionDescription).TypeId);
                (skills[_i2] as SkillActionDescription).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            SkillActionDescription _item2 = null;
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobDescription.jobId.");
            }

            uint _skillsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _skillsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<SkillActionDescription>((short)_id2);
                _item2.Deserialize(reader);
                skills[_i2] = _item2;
            }

        }


    }
}








