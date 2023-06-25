using Giny.Core.DesignPattern;
using Giny.Core.Network;
using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap
{
    public class ZaapServer
    {
        private string Username
        {
            get;
            set;
        }
        private string Password
        {
            get;
            set;
        }
        private TcpServer Server
        {
            get;
            set;
        }

        public void Start(int port)
        {
            Server = new TcpServer("127.0.0.1", port);
            Server.OnSocketConnected += Server_OnSocketConnected;
            Server.Start();
        }

        public void SetCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        private void Server_OnSocketConnected(System.Net.Sockets.Socket obj)
        {
            var zaapClient = new ZaapClient(obj, Username, Password);
        }
    }
}
