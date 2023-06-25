using Giny.Auth.Network;
using Giny.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Handlers
{
    class BasicHandler
    {
        public static void HandleBasicPing(BasicPingMessage message, AuthClient client)
        {
            client.Send(new BasicPongMessage(message.quiet));
        }
    }
}
