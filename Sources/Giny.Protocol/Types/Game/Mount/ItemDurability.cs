using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ItemDurability  
    { 
        public const ushort Id = 6808;
        public virtual ushort TypeId => Id;

        public short durability;
        public short durabilityMax;

        public ItemDurability()
        {
        }
        public ItemDurability(short durability,short durabilityMax)
        {
            this.durability = durability;
            this.durabilityMax = durabilityMax;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)durability);
            writer.WriteShort((short)durabilityMax);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            durability = (short)reader.ReadShort();
            durabilityMax = (short)reader.ReadShort();
        }


    }
}








