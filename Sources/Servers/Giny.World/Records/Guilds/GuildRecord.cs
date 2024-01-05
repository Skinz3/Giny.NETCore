using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using Giny.World.Managers.Guilds;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Guilds
{
    [Table("guilds")]
    public class GuildRecord : IRecord
    {
        [Container]
        private static ConcurrentDictionary<long, GuildRecord> Guilds = new ConcurrentDictionary<long, GuildRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [Blob]
        public GuildEmblemRecord Emblem
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public List<GuildRankRecord> Ranks
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }


        public long Experience
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public List<GuildMemberRecord> Members
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public GuildMotdRecord Motd
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public GuildBulletinRecord Bulletin
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public List<GuildGlobalActivityRecord> GlobalActivities
        {
            get;
            set;
        }

        [Update]
        [Blob]
        public List<GuildChestActivityRecord> ChestActivities
        {
            get;
            set;
        }

        public static bool Exists(string guildName)
        {
            return Guilds.Values.Any(x => x.Name == guildName);
        }
        public static bool Exists(GuildEmblemRecord emblem)
        {
            return Guilds.Values.Any(x => x.Emblem.Equals(emblem));
        }

        public static IEnumerable<GuildRecord> GetGuilds()
        {
            return Guilds.Values;
        }

        public GuildMemberRecord GetMember(long id)
        {
            return Members.FirstOrDefault(x => x.CharacterId == id);
        }
    }
}