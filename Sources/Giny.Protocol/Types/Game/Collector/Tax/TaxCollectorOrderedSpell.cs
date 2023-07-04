using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TaxCollectorOrderedSpell
    {
        public const ushort Id = 9074;
        public virtual ushort TypeId => Id;

        public int spellId;
        public byte slot;

        public TaxCollectorOrderedSpell()
        {
        }
        public TaxCollectorOrderedSpell(int spellId, byte slot)
        {
            this.spellId = spellId;
            this.slot = slot;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarInt((int)spellId);
            if (slot < 0 || slot > 5)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element slot.");
            }

            writer.WriteByte((byte)slot);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            spellId = (int)reader.ReadVarUhInt();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of TaxCollectorOrderedSpell.spellId.");
            }

            slot = (byte)reader.ReadByte();
            if (slot < 0 || slot > 5)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element of TaxCollectorOrderedSpell.slot.");
            }

        }


    }
}


