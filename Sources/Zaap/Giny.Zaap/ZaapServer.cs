using Giny.Core.DesignPattern;
using Giny.Core.Network;
using Giny.Core.Network.Messages;
using Giny.Zaap.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap
{
    public class ZaapServer : Singleton<ZaapServer>
    {
        private List<ZaapClient> Clients
        {
            get;
            set;
        }
        private TcpServer Server
        {
            get;
            set;
        }
        public IAccountProvider AccountProvider
        {
            get;
            private set;
        }

        public void Start(int port, IAccountProvider accountProvider)
        {
            Clients = new List<ZaapClient>();
            Server = new TcpServer("127.0.0.1", port);
            AccountProvider = accountProvider;
            Server.OnSocketConnected += OnSocketConnected;
            Server.Start();
        }


        private void OnSocketConnected(System.Net.Sockets.Socket obj)
        {
            ZaapClient client = new ZaapClient(obj);
            AddClient(client);
        }

        public void AddClient(ZaapClient client)
        {
            lock (Clients)
            {
                Clients.Add(client);
            }
        }
        public void RemoveClient(ZaapClient client)
        {
            lock (Clients)
            {
                Clients.Remove(client);
            }
        }
    }
}
