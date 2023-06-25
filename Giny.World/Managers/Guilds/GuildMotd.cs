using Giny.Core.DesignPattern;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Guilds
{
    [ProtoContract]
    public class GuildMotd
    {
        [ProtoMember(1)]
        public string Content
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public int Timestamp
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public long MemberId
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public string MemberName
        {
            get;
            set;
        }

        public GuildMotd()
        {
            this.Content = string.Empty;
            this.MemberName = string.Empty;
        }
    }
}
