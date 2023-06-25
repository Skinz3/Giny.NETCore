using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class Idol  
    { 
        public const ushort Id = 9682;
        public virtual ushort TypeId => Id;

        public short id;
        public short xpBonusPercent;
        public short dropBonusPercent;

        public Idol()
        {
        }
        public Idol(short id,short xpBonusPercent,short dropBonusPercent)
        {
            this.id = id;
            this.xpBonusPercent = xpBonusPercent;
            this.dropBonusPercent = dropBonusPercent;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            if (xpBonusPercent < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonusPercent + ") on element xpBonusPercent.");
            }

            writer.WriteVarShort((short)xpBonusPercent);
            if (dropBonusPercent < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonusPercent + ") on element dropBonusPercent.");
            }

            writer.WriteVarShort((short)dropBonusPercent);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of Idol.id.");
            }

            xpBonusPercent = (short)reader.ReadVarUhShort();
            if (xpBonusPercent < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonusPercent + ") on element of Idol.xpBonusPercent.");
            }

            dropBonusPercent = (short)reader.ReadVarUhShort();
            if (dropBonusPercent < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonusPercent + ") on element of Idol.dropBonusPercent.");
            }

        }


    }
}








