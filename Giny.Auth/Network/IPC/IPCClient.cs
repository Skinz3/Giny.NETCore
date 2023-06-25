using Giny.Auth.Handlers;
using Giny.Auth.Network;
using Giny.Auth.Records;
using Giny.Core;
using Giny.Core.Network;
using Giny.Core.Network.IPC;
using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Network.IPC
{
    public class IPCClient : Client
    {
        public WorldServerRecord WorldServerRecord
        {
            get;
            set;
        }

        public IPCClient(Socket socket) : base(socket)
        {

        }
        public override void OnConnected()
        {
            throw new NotImplementedException();
        }

        public override void OnConnectionClosed()
        {
            OnDisconnected();
        }

        public override void OnFailToConnect(Exception ex)
        {
            throw new NotImplementedException();
        }
        public void SendIPCAnswer(IPCMessage message, IPCMessage requestMessage)
        {
            message.requestId = requestMessage.requestId;
            message.authSide = requestMessage.authSide;
            Send(message);
        }
        public override void OnMessageReceived(NetworkMessage message)
        {
            var ipcMessage = message as IPCMessage;

            Logger.Write("(IPC) Received " + message);

            if ((ipcMessage.authSide) && ipcMessage.requestId > -1)
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
            Logger.Write("(IPC) Send " + result.AsyncState);
        }
        public override void OnMessageUnhandled(NetworkMessage message)
        {
            Logger.Write(string.Format("No Handler: ({0}) {1}", message.MessageId, message.ToString()), Channels.Warning);
        }
        public override void OnHandlingError(NetworkMessage message, Delegate handler, Exception ex)
        {
            Logger.Write(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Method.Name, ex.ToString()), Channels.Warning);
        }
        public override void OnDisconnected()
        {
            Logger.Write("(IPC) client disconnected.");

            if (WorldServerRecord != null)
            {
                WorldServerRecord.Status = ServerStatusEnum.OFFLINE;
                IPCHandler.OnServerStatusUpdated(WorldServerRecord);
                IPCServer.Instance.RemoveClient(WorldServerRecord.Id);
            }
        }

        
    }
}
