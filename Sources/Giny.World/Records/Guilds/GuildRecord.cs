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
    public class GuildRecord : ITable
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

        [ProtoSerialize]
        public GuildEmblemRecord Emblem
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
        [ProtoSerialize]
        public List<GuildMemberRecord> Members
        {
            get;
            set;
        }

        [Update]
        [ProtoSerialize]
        public GuildMotd Motd
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
