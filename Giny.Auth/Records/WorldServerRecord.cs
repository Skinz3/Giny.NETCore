using Giny.Auth.Network;
using Giny.ORM;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Records
{
    [Table("WorldServers")]
    public class WorldServerRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, WorldServerRecord> WorldServers = new ConcurrentDictionary<long, WorldServerRecord>();

        long ITable.Id => Id;

        [Primary]
        public short Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        [Ignore]
        public ServerStatusEnum Status
        {
            get;
            set;
        }
        public GameServerTypeEnum Type
        {
            get;
            set;
        }
        public bool MonoAccount
        {
            get;
            set;
        }
        public string Host
        {
            get;
            set;
        }
        public short Port
        {
            get;
            set;
        }
        public WorldServerRecord()
        {
            this.Status = ServerStatusEnum.OFFLINE;
        }
        public GameServerInformations GetServerInformations(AuthClient client)
        {
            return new GameServerInformations(Id, (byte)Type, MonoAccount, (byte)Status, 0, true, client.GetCharactersSlots(Id), client.Account.CharacterSlots, 0);
        }
        public static GameServerInformations[] GetGameServerInformations(AuthClient client)
        {
            return WorldServers.Values.Select(x => x.GetServerInformations(client)).ToArray();
        }
        public static WorldServerRecord GetWorldServer(short id)
        {
            if (WorldServers.ContainsKey(id))
                return WorldServers[id];
            else
                return null;
        }
        public static bool CanBeAdded(WorldServerRecord server)
        {
            return !WorldServers.ContainsKey(server.Id) && !WorldServers.Values.Any(x => x.Host == server.Host && x.Port == server.Port);
        }
    }
}
