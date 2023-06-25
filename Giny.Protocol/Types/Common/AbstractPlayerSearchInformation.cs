using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AbstractPlayerSearchInformation  
    { 
        public const ushort Id = 4580;
        public virtual ushort TypeId => Id;


        public AbstractPlayerSearchInformation()
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








