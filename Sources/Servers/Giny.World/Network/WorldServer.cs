using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using Giny.Core.Network;
using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Network
{
    public class WorldServer : Singleton<WorldServer>
    {
        private readonly object m_locker = new object();

        private readonly object m_statusLocker = new object();

        private List<WorldClient> Clients
        {
            get;
            set;
        }

        private ServerStatusEnum Status
        {
            get;
            set;
        } = ServerStatusEnum.STARTING;


        private TcpServer Server
        {
            get;
            set;
        }
        public bool Started
        {
            get;
            private set;
        }

        public int MaximumClients
        {
            get;
            private set;
        }


        public WorldServer()
        {
            this.Clients = new List<WorldClient>();
            this.Started = false;
        }


        public void Start(string ip, int port)
        {
            this.Server = new TcpServer(ip, port);
            this.Server.OnSocketConnected += OnClientConnected;
            this.Server.OnServerFailedToStart += OnServerFailedToStart;
            this.Server.OnServerStarted += OnServerStarted;
            this.Server.Start();
            this.Started = true;
        }

        public bool IsOnline(long characterId)
        {
            return GetOnlineClients().Any(x => x.Character.Id == characterId);
        }

        public void Foreach(Action<WorldClient> action)
        {

            foreach (var client in GetOnlineClients())
            {
                action(client);
            }

        }
        public IEnumerable<WorldClient> GetOnlineClients()
        {
            lock (m_locker)
            {
                return Clients.Where(x => x.InGame).ToArray();
            }
        }
        public IEnumerable<WorldClient> GetClients()
        {
            lock (m_locker)
            {
                return Clients.ToArray();
            }
        }
        /// <summary>
        /// Returns a client who is not necessarily connected in game 
        /// </summary>
        public WorldClient GetClient(Func<WorldClient, bool> predicate)
        {
            lock (m_locker)
            {
                return Clients.FirstOrDefault(predicate);
            }
        }
        public WorldClient GetClient(AbstractPlayerSearchInformation target)
        {
            if (target is PlayerSearchCharacterNameInformation)
            {
                return GetOnlineClient(x => x.Character.Name == ((PlayerSearchCharacterNameInformation)target).name);
            }
            if (target is PlayerSearchTagInformation)
            {
                throw new NotImplementedException("tags not implemented.");
            }

            return null;
        }

        /// <summary>
        /// Returns a client who is connected in game 
        /// </summary>
        public WorldClient GetOnlineClient(Func<WorldClient, bool> predicate)
        {
            lock (m_locker)
            {
                return Clients.Where(x => x.InGame).FirstOrDefault(predicate);
            }
        }
        private void OnClientConnected(Socket acceptSocket)
        {
            if (ConfigManager<WorldConfig>.Instance.LogProtocol)
            {
                Logger.Write("(World) New client connected.");
            }

            WorldClient client = new WorldClient(acceptSocket);

            lock (m_locker)
            {
                Clients.Add(client);

                if (Clients.Count > MaximumClients)
                {
                    MaximumClients = Clients.Count;
                }
            }
        }

        public GameServerTypeEnum GetServerType()
        {
            return ConfigManager<WorldConfig>.Instance.ServerType;
        }
        public bool IsEpicOrHardcore()
        {
            return GetServerType() == GameServerTypeEnum.SERVER_TYPE_EPIC || GetServerType() == GameServerTypeEnum.SERVER_TYPE_HARDCORE;
        }
        public void RemoveClient(WorldClient client)
        {
            lock (m_locker)
            {
                if (ConfigManager<WorldConfig>.Instance.LogProtocol)
                {
                    Logger.Write("(World) Client disconnected.", Channels.Info);
                }

                Clients.Remove(client);
            }
        }
        private void OnServerFailedToStart(Exception ex)
        {
            Logger.Write("(World) Unable to start WorldServer : " + ex, Channels.Critical);
            SetServerStatus(ServerStatusEnum.OFFLINE);
        }
        public void SendServerStatusToAuth()
        {
            IPCManager.Instance.Send(new IPCServerStatusUpdateMessage(Status));
        }
        public void SetServerStatus(ServerStatusEnum status)
        {
            lock (m_statusLocker)
            {
                this.Status = status;

                if (IPCManager.Instance.Connected)
                {
                    SendServerStatusToAuth();
                }
            }

        }
        public ServerStatusEnum GetServerStatus()
        {
            lock (m_statusLocker)
            {
                return Status;
            }
        }
        public void Send(NetworkMessage message)
        {
            foreach (var client in GetOnlineClients())
            {
                client.Send(message);
            }
        }
        private void OnServerStarted()
        {
            Logger.Write("(World) World Server started", Channels.Log);
            SetServerStatus(ServerStatusEnum.ONLINE);
        }
    }
}
