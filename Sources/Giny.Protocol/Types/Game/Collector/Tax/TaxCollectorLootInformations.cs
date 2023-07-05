using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TaxCollectorLootInformations : TaxCollectorComplementaryInformations
    {
        public new const ushort Id = 5296;
        public override ushort TypeId => Id;

        public int pods;
        public long itemsValue;

        public TaxCollectorLootInformations()
        {
        }
        public TaxCollectorLootInformations(int pods, long itemsValue)
        {
            this.pods = pods;
            this.itemsValue = itemsValue;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarInt((int)pods);
            if (itemsValue < 0 || itemsValue > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element itemsValue.");
            }

            writer.WriteVarLong((long)itemsValue);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            pods = (int)reader.ReadVarUhInt();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of TaxCollectorLootInformations.pods.");
            }

            itemsValue = (long)reader.ReadVarUhLong();
            if (itemsValue < 0 || itemsValue > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element of TaxCollectorLootInformations.itemsValue.");
            }

        }


    }
}


