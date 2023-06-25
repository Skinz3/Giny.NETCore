using Giny.Core.IO;
using Giny.Uplauncher.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Uplauncher.Protocol
{
    public class ConnectResult : ZaapMessage
    {
        public ConnectResult()
        {

        }

        public override void Deserialize(TProtocol protocol, BigEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(TProtocol protocol, BigEndianWriter writer)
        {
            protocol.WriteFieldBegin(new TField("success", TType.STRING, 0), writer);

            string toSend = "success";

            writer.WriteInt(toSend.Length);
            writer.WriteUTFBytes(toSend);

            protocol.WriteFieldStop(writer);
        }

    }
}
