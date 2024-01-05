using System;
using ProtoBuf;

namespace Giny.World.Records.Guilds
{
    [ProtoContract]
    public class GuildBulletinRecord
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

        [ProtoMember(5)]
        public int LastNotifiedTimestamp
        {
            get;
            set;
        }

        public GuildBulletinRecord()
        {
            this.Content = string.Empty;
            this.MemberName = string.Empty;
        }
    }
}