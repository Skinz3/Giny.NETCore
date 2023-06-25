using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Network;
using Giny.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Network
{
    public class AuthServer : Singleton<AuthServer>
    {
        private List<AuthClient> Clients
        {
            get;
            set;
        }
        private TcpServer Server
        {
            get;
            set;
        }
        public AuthServer()
        {
            this.Clients = new List<AuthClient>();

        }
        public void Start(string ip, int port)
        {
            this.Server = new TcpServer(ip, port);
            this.Server.OnServerFailedToStart += OnServerFailedToStart;
            this.Server.OnServerStarted += OnServerStarted;
            this.Server.OnSocketConnected += OnClientConnected;
            this.Server.Start();
        }

        private void OnClientConnected(Socket acceptSocket)
        {
            Logger.Write("(Auth) client connected.");
            AuthClient client = new AuthClient(acceptSocket);
        }
        public void AddClient(AuthClient client)
        {
            Clients.Add(client);
        }
        public void RemoveClient(AuthClient client)
        {
            Clients.Remove(client);
        }
        private void OnServerFailedToStart(Exception obj)
        {
            Logger.Write("(Auth) Unable to start started : " + obj, Channels.Critical);
        }
        private void OnServerStarted()
        {
            Logger.Write("(Auth) Server started", Channels.Log);
        }
        public AuthClient GetClient(int accountId)
        {
            return Clients.FirstOrDefault(x => x.Account.Id  == accountId);
        }
        public IEnumerable<AuthClient> GetClients()
        {
            return Clients;
        }


    }
}
