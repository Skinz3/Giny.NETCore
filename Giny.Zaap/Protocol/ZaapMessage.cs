using Giny.Core.IO;
using Giny.Zaap.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Protocol
{
    public abstract class ZaapMessage
    {
        
        public abstract void Serialize(TProtocol protocol, BigEndianWriter writer);
        public abstract void Deserialize(TProtocol protocol, BigEndianReader reader);

        
    }
}
