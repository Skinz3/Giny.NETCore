using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class KothWinner
    {
        public const ushort Id = 629;
        public virtual ushort TypeId => Id;


        public KothWinner()
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


