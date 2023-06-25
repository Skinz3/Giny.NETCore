using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SubEntity  
    { 
        public const ushort Id = 3179;
        public virtual ushort TypeId => Id;

        public byte bindingPointCategory;
        public byte bindingPointIndex;
        public EntityLook subEntityLook;

        public SubEntity()
        {
        }
        public SubEntity(byte bindingPointCategory,byte bindingPointIndex,EntityLook subEntityLook)
        {
            this.bindingPointCategory = bindingPointCategory;
            this.bindingPointIndex = bindingPointIndex;
            this.subEntityLook = subEntityLook;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)bindingPointCategory);
            if (bindingPointIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + bindingPointIndex + ") on element bindingPointIndex.");
            }

            writer.WriteByte((byte)bindingPointIndex);
            subEntityLook.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            bindingPointCategory = (byte)reader.ReadByte();
            if (bindingPointCategory < 0)
            {
                throw new System.Exception("Forbidden value (" + bindingPointCategory + ") on element of SubEntity.bindingPointCategory.");
            }

            bindingPointIndex = (byte)reader.ReadByte();
            if (bindingPointIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + bindingPointIndex + ") on element of SubEntity.bindingPointIndex.");
            }

            subEntityLook = new EntityLook();
            subEntityLook.Deserialize(reader);
        }


    }
}








