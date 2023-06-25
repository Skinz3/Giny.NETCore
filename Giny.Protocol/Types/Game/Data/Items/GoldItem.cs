using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GoldItem : Item  
    { 
        public new const ushort Id = 7805;
        public override ushort TypeId => Id;

        public long sum;

        public GoldItem()
        {
        }
        public GoldItem(long sum)
        {
            this.sum = sum;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (sum < 0 || sum > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sum + ") on element sum.");
            }

            writer.WriteVarLong((long)sum);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            sum = (long)reader.ReadVarUhLong();
            if (sum < 0 || sum > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sum + ") on element of GoldItem.sum.");
            }

        }


    }
}








