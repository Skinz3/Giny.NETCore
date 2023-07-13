using Giny.Core.IO;
using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers;
using Giny.World.Managers.Breeds;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Approach
{
    class ApproachHandler /* todo = world queue */
    {
        static object m_locker = new object();

        [MessageHandler]
        public static void HandleAuthenticationTicketMessage(AuthenticationTicketMessage message, WorldClient client)
        {
            lock (m_locker)
            {
                var reader = new BigEndianReader(Encoding.ASCII.GetBytes(message.ticket));
                var count = reader.ReadByte();
                var ticket = reader.ReadUTFBytes(count);

                IPCManager.Instance.SendRequest(new AccountRequestMessage(ticket),
                delegate (AccountMessage msg)
                {
                    if (msg.Account != null)
                    {
                        client.Account = msg.Account;
                        client.OnAccountReceived();
                        client.Send(new AuthenticationTicketAcceptedMessage());
                    }
                    else
                    {
                        client.Send(new AuthenticationTicketRefusedMessage());
                        client.Disconnect();
                    }
                },
                delegate ()
                {
                    client.Send(new AuthenticationTicketRefusedMessage());
                    client.Disconnect();
                });
            }
        }
        [MessageHandler]
        public static void HandleReloginTokenRequestMessage(ReloginTokenRequestMessage message, WorldClient client)
        {
            client.Send(new ReloginTokenStatusMessage(false, string.Empty));
        }
    }
}
