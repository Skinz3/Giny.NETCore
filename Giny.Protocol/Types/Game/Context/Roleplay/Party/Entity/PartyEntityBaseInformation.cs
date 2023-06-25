using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyEntityBaseInformation  
    { 
        public const ushort Id = 8549;
        public virtual ushort TypeId => Id;

        public byte indexId;
        public byte entityModelId;
        public EntityLook entityLook;

        public PartyEntityBaseInformation()
        {
        }
        public PartyEntityBaseInformation(byte indexId,byte entityModelId,EntityLook entityLook)
        {
            this.indexId = indexId;
            this.entityModelId = entityModelId;
            this.entityLook = entityLook;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (indexId < 0)
            {
                throw new System.Exception("Forbidden value (" + indexId + ") on element indexId.");
            }

            writer.WriteByte((byte)indexId);
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element entityModelId.");
            }

            writer.WriteByte((byte)entityModelId);
            entityLook.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            indexId = (byte)reader.ReadByte();
            if (indexId < 0)
            {
                throw new System.Exception("Forbidden value (" + indexId + ") on element of PartyEntityBaseInformation.indexId.");
            }

            entityModelId = (byte)reader.ReadByte();
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element of PartyEntityBaseInformation.entityModelId.");
            }

            entityLook = new EntityLook();
            entityLook.Deserialize(reader);
        }


    }
}








