using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class EntityInformation  
    { 
        public const ushort Id = 9471;
        public virtual ushort TypeId => Id;

        public short id;
        public int experience;
        public bool status;

        public EntityInformation()
        {
        }
        public EntityInformation(short id,int experience,bool status)
        {
            this.id = id;
            this.experience = experience;
            this.status = status;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            if (experience < 0)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarInt((int)experience);
            writer.WriteBoolean((bool)status);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of EntityInformation.id.");
            }

            experience = (int)reader.ReadVarUhInt();
            if (experience < 0)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of EntityInformation.experience.");
            }

            status = (bool)reader.ReadBoolean();
        }


    }
}








