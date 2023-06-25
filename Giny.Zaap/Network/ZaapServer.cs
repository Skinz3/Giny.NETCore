using Giny.Core.DesignPattern;
using Giny.Core.Network;
using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Network
{
    public class ZaapServer
    {
        private TcpServer Server
        {
            get;
            set;
        }

        internal void Start()
        {
            this.Server = new TcpServer("127.0.0.1", AppState.Port);
            this.Server.OnSocketConnected += Server_OnSocketConnected;
            Server.Start();

        }

        private void Server_OnSocketConnected(System.Net.Sockets.Socket obj)
        {
            var zaapClient = new ZaapClient(obj, AppState.Username, AppState.Password);
        }
    }
}
