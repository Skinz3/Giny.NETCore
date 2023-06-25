using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Network.IPC;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.World.Network
{
    public class IPCManager : Singleton<IPCManager>
    {
        public bool Connected
        {
            get;
            set;
        }
        private IPCClient Client
        {
            get;
            set;
        }

        public void ConnectToAuth()
        {
            Client = new IPCClient();
            Client.Connect(ConfigFile.Instance.IPCHost, ConfigFile.Instance.IPCPort);
        }

        public void SendRequest<T>(IPCMessage message, IPCRequestManager.RequestCallbackDelegate<T> sucess, IPCRequestManager.RequestCallbackErrorDelegate error) where T : IPCMessage
        {
            IPCRequestManager.SendRequest(Client, message, false, sucess, error);
        }
        public void Send(IPCMessage message)
        {
            Client.Send(message);
        }
        public void OnLostConnection()
        {
            Connected = false;
            Client.Disconnect();
            Client = null;


            Logger.Write("Lost connection to IPC Server. Trying again in 2s", Channels.Warning);

            Thread.Sleep(2000);

            ConnectToAuth();
        }

        public void OnConnected()
        {
            Logger.Write("Connected to IPCServer");
            Client.Send(new HandshakeMessage(ConfigFile.Instance.ServerId));
            Connected = true;

            if (!WorldServer.Instance.Started)
            {
                WorldServer.Instance.Start(ConfigFile.Instance.Host, ConfigFile.Instance.Port);
            }
            else
            {
                WorldServer.Instance.SendServerStatusToAuth();
            }

        }

        public void OnFailToConnect()
        {
            Logger.Write("Unable to connect to IPC Server. Trying again in 2s", Channels.Warning);
            Thread.Sleep(2000);
            ConnectToAuth();
        }
    }
}
