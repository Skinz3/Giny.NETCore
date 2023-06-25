using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockBuyableInformations  
    { 
        public const ushort Id = 1474;
        public virtual ushort TypeId => Id;

        public long price;
        public bool locked;

        public PaddockBuyableInformations()
        {
        }
        public PaddockBuyableInformations(long price,bool locked)
        {
            this.price = price;
            this.locked = locked;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
            writer.WriteBoolean((bool)locked);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of PaddockBuyableInformations.price.");
            }

            locked = (bool)reader.ReadBoolean();
        }


    }
}








