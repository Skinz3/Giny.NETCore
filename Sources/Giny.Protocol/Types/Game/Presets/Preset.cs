using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class Preset  
    { 
        public const ushort Id = 4056;
        public virtual ushort TypeId => Id;

        public short id;

        public Preset()
        {
        }
        public Preset(short id)
        {
            this.id = id;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)id);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadShort();
        }


    }
}








