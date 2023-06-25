using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AbstractCharacterInformation  
    { 
        public const ushort Id = 776;
        public virtual ushort TypeId => Id;

        public long id;

        public AbstractCharacterInformation()
        {
        }
        public AbstractCharacterInformation(long id)
        {
            this.id = id;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of AbstractCharacterInformation.id.");
            }

        }


    }
}








