using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class IndexedEntityLook  
    { 
        public const ushort Id = 8126;
        public virtual ushort TypeId => Id;

        public EntityLook look;
        public byte index;

        public IndexedEntityLook()
        {
        }
        public IndexedEntityLook(EntityLook look,byte index)
        {
            this.look = look;
            this.index = index;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            look.Serialize(writer);
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element index.");
            }

            writer.WriteByte((byte)index);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            look = new EntityLook();
            look.Deserialize(reader);
            index = (byte)reader.ReadByte();
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element of IndexedEntityLook.index.");
            }

        }


    }
}








