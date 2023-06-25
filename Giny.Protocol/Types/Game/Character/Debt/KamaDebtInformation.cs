using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class KamaDebtInformation : DebtInformation  
    { 
        public new const ushort Id = 5055;
        public override ushort TypeId => Id;

        public long kamas;

        public KamaDebtInformation()
        {
        }
        public KamaDebtInformation(long kamas,double id,double timestamp)
        {
            this.kamas = kamas;
            this.id = id;
            this.timestamp = timestamp;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of KamaDebtInformation.kamas.");
            }

        }


    }
}








