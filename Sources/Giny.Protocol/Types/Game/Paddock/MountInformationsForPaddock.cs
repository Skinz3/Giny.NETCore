using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MountInformationsForPaddock  
    { 
        public const ushort Id = 7701;
        public virtual ushort TypeId => Id;

        public short modelId;
        public string name;
        public string ownerName;

        public MountInformationsForPaddock()
        {
        }
        public MountInformationsForPaddock(short modelId,string name,string ownerName)
        {
            this.modelId = modelId;
            this.name = name;
            this.ownerName = ownerName;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element modelId.");
            }

            writer.WriteVarShort((short)modelId);
            writer.WriteUTF((string)name);
            writer.WriteUTF((string)ownerName);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            modelId = (short)reader.ReadVarUhShort();
            if (modelId < 0)
            {
                throw new System.Exception("Forbidden value (" + modelId + ") on element of MountInformationsForPaddock.modelId.");
            }

            name = (string)reader.ReadUTF();
            ownerName = (string)reader.ReadUTF();
        }


    }
}








