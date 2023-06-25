using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MonsterBoosts  
    { 
        public const ushort Id = 183;
        public virtual ushort TypeId => Id;

        public int id;
        public short xpBoost;
        public short dropBoost;

        public MonsterBoosts()
        {
        }
        public MonsterBoosts(int id,short xpBoost,short dropBoost)
        {
            this.id = id;
            this.xpBoost = xpBoost;
            this.dropBoost = dropBoost;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarInt((int)id);
            if (xpBoost < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBoost + ") on element xpBoost.");
            }

            writer.WriteVarShort((short)xpBoost);
            if (dropBoost < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBoost + ") on element dropBoost.");
            }

            writer.WriteVarShort((short)dropBoost);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (int)reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of MonsterBoosts.id.");
            }

            xpBoost = (short)reader.ReadVarUhShort();
            if (xpBoost < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBoost + ") on element of MonsterBoosts.xpBoost.");
            }

            dropBoost = (short)reader.ReadVarUhShort();
            if (dropBoost < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBoost + ") on element of MonsterBoosts.dropBoost.");
            }

        }


    }
}








