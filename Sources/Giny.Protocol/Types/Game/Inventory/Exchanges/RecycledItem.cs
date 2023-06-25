using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class RecycledItem  
    { 
        public const ushort Id = 2393;
        public virtual ushort TypeId => Id;

        public short id;
        public uint qty;

        public RecycledItem()
        {
        }
        public RecycledItem(short id,uint qty)
        {
            this.id = id;
            this.qty = qty;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            if (qty < 0 || qty > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + qty + ") on element qty.");
            }

            writer.WriteUInt((uint)qty);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of RecycledItem.id.");
            }

            qty = (uint)reader.ReadUInt();
            if (qty < 0 || qty > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + qty + ") on element of RecycledItem.qty.");
            }

        }


    }
}








