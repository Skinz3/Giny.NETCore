using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ServerSessionConstant  
    { 
        public const ushort Id = 9635;
        public virtual ushort TypeId => Id;

        public short id;

        public ServerSessionConstant()
        {
        }
        public ServerSessionConstant(short id)
        {
            this.id = id;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ServerSessionConstant.id.");
            }

        }


    }
}








