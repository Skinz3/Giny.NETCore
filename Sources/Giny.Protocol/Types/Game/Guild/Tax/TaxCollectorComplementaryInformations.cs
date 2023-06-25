using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorComplementaryInformations  
    { 
        public const ushort Id = 858;
        public virtual ushort TypeId => Id;


        public TaxCollectorComplementaryInformations()
        {
        }
        public virtual void Serialize(IDataWriter writer)
        {
        }
        public virtual void Deserialize(IDataReader reader)
        {
        }


    }
}








