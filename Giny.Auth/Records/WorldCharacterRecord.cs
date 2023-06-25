using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Records
{
    [Table("worldcharacters")]
    public class WorldCharacterRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, WorldCharacterRecord> WorldCharacters = new ConcurrentDictionary<long, WorldCharacterRecord>();

        private static UniqueLongIdProvider idProvider;

        [Primary]
        public long Id
        {
            get;
            set;
        }
        [Update]
        public long CharacterId
        {
            get;
            set;
        }
        public int AccountId
        {
            get;
            set;
        }
        [Update]
        public short ServerId
        {
            get;
            set;
        }
        [StartupInvoke(StartupInvokePriority.Last)]
        public static void Initialize()
        {
            long lastId = WorldCharacters.Count > 0 ? WorldCharacters.Keys.OrderByDescending(x => x).First() : 0;
            idProvider = new UniqueLongIdProvider(lastId);
        }
        public static bool Exist(long characterId, short serverId)
        {
            return WorldCharacters.Values.Any(x => x.CharacterId == characterId && x.ServerId == serverId);
        }
        public static long NextId()
        {
            return idProvider.Pop();
        }

        public static WorldCharacterRecord[] Get(int accountId)
        {
            return WorldCharacters.Values.Where(x => x.AccountId == accountId).ToArray();
        }

        public static WorldCharacterRecord Get(int accountId,long characterId)
        {
            return WorldCharacters.Values.FirstOrDefault(x => x.AccountId == accountId && x.CharacterId == characterId);
        }
        public static WorldCharacterRecord[] Get(short serverId)
        {
            return WorldCharacters.Values.Where(x => x.ServerId == serverId).ToArray();
        }
    }
}
