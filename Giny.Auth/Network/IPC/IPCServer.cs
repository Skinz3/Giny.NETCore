using Giny.Auth.Network;
using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Network;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Network.IPC
{
    public class IPCServer : Singleton<IPCServer>
    {
        private Dictionary<short, IPCClient> Clients
        {
            get;
            set;
        }
        private TcpServer Server
        {
            get;
            set;
        }
        public IPCServer()
        {
            this.Clients = new Dictionary<short, IPCClient>();

        }
        public void Start(string ip, int port)
        {
            this.Server = new TcpServer(ip, port);
            this.Server.OnServerFailedToStart += OnServerFailedToStart;
            this.Server.OnServerStarted += OnServerStarted;
            this.Server.OnSocketConnected += OnClientConnected;
            this.Server.Start();
        }
        public void SendRequest<T>(short serverId, IPCMessage message, IPCRequestManager.RequestCallbackDelegate<T> sucess, IPCRequestManager.RequestCallbackErrorDelegate error) where T : IPCMessage
        {
            var client = GetClient(serverId);

            if (client == null)
            {
                Logger.Write("Unable to send " + message + " to server " + serverId + ". The server is not connected", Channels.Warning);
                return;
            }
            SendRequest<T>(client, message, sucess, error);
        }
        public void SendRequest<T>(IPCClient client, IPCMessage message, IPCRequestManager.RequestCallbackDelegate<T> sucess, IPCRequestManager.RequestCallbackErrorDelegate error) where T : IPCMessage
        {
            IPCRequestManager.SendRequest(client, message, true, sucess, error);
        }
        private void OnClientConnected(Socket acceptSocket)
        {
            Logger.Write("(IPC) client connected.");
            IPCClient client = new IPCClient(acceptSocket);
        }
        public IPCClient GetClient(short serverId)
        {
            if (Clients.ContainsKey(serverId))
                return Clients[serverId];
            else
                return null;
        }
        public bool IsWorldConnected(short serverId)
        {
            return Clients.ContainsKey(serverId);
        }
        public bool AddClient(short serverId, IPCClient client)
        {
            if (Clients.ContainsKey(serverId))
            {
                Logger.Write("A server try to connect but he is already registred", Channels.Warning);
                return false;
            }
            else
            {
                Clients.Add(serverId, client);
                return true;
            }
        }
        public void RemoveClient(short serverId)
        {
            if (Clients.ContainsKey(serverId))
                Clients.Remove(serverId);
        }
        private void OnServerStarted()
        {
            Logger.Write("(IPC) Server started", Channels.Log);
        }
        private void OnServerFailedToStart(Exception obj)
        {
            Logger.Write("(IPC) Unable to start server : " + obj, Channels.Critical);
        }
    }
}
