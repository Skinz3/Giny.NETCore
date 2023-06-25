using Giny.Core.IO;
using Giny.Zaap.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Protocol
{
    public class ZaapMustUpdateGetResult : ZaapMessage
    {
        public bool Success
        {
            get;
            private set;
        }

        public ZaapMustUpdateGetResult(bool success)
        {
            this.Success = success;
        }

        public override void Deserialize(TProtocol protocol, BigEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(TProtocol protocol, BigEndianWriter writer)
        {
            protocol.WriteFieldBegin(new TField("success", TType.BOOL, 0), writer);

            writer.WriteBoolean(false);

            protocol.WriteFieldStop(writer);

        }
    }
}
