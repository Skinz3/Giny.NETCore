using Giny.Core;
using Giny.Core.Network;
using Giny.Core.Network.IPC;
using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Network
{
    public class IPCClient : Client
    {
        public IPCClient()
        {

        }
        public override void OnConnected()
        {
            IPCManager.Instance.OnConnected();
        }

        public override void OnConnectionClosed()
        {
            IPCManager.Instance.OnLostConnection();
        }

        public override void OnDisconnected()
        {

        }
        public void SendIPCAnswer(IPCMessage message, IPCMessage requestMessage)
        {
            message.requestId = requestMessage.requestId;
            message.authSide = requestMessage.authSide;
            Send(message);
        }
        public override void OnFailToConnect(Exception ex)
        {
            IPCManager.Instance.OnFailToConnect();
        }

        public override void OnMessageReceived(NetworkMessage message)
        {
            var ipcMessage = message as IPCMessage;

            if (ConfigFile.Instance.LogProtocol)
                Logger.Write("(IPC) Received " + message);

            if ((!ipcMessage.authSide) && ipcMessage.requestId > -1)
            {
                IPCRequestManager.ReceiveRequest(ipcMessage);
            }
            else
            {
                ProtocolMessageManager.HandleMessage(message, this);
            }
        }

        public override void OnSended(IAsyncResult result)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write("(IPC) Send " + result.AsyncState);
        }

        public override void OnMessageUnhandled(NetworkMessage message)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write(string.Format("No Handler: ({0}) {1}", message.MessageId, message.ToString()), Channels.Warning);
        }

        public override void OnHandlingError(NetworkMessage message, Delegate handler, Exception ex)
        {
            if (ConfigFile.Instance.LogProtocol)
                Logger.Write(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Method.Name, ex.ToString()), Channels.Warning);
        }
    }
}
