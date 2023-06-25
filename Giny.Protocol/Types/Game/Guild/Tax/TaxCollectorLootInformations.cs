using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorLootInformations : TaxCollectorComplementaryInformations  
    { 
        public new const ushort Id = 1966;
        public override ushort TypeId => Id;

        public long kamas;
        public long experience;
        public int pods;
        public long itemsValue;

        public TaxCollectorLootInformations()
        {
        }
        public TaxCollectorLootInformations(long kamas,long experience,int pods,long itemsValue)
        {
            this.kamas = kamas;
            this.experience = experience;
            this.pods = pods;
            this.itemsValue = itemsValue;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarInt((int)pods);
            if (itemsValue < 0 || itemsValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element itemsValue.");
            }

            writer.WriteVarLong((long)itemsValue);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of TaxCollectorLootInformations.kamas.");
            }

            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of TaxCollectorLootInformations.experience.");
            }

            pods = (int)reader.ReadVarUhInt();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of TaxCollectorLootInformations.pods.");
            }

            itemsValue = (long)reader.ReadVarUhLong();
            if (itemsValue < 0 || itemsValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element of TaxCollectorLootInformations.itemsValue.");
            }

        }


    }
}








