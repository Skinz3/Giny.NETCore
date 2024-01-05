using System;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using ProtoBuf;

namespace Giny.World.Records.Guilds
{
    [ProtoContract]
    public class GuildChestActivityRecord
    {
        [ProtoMember(1)]
        public DateTime Date
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public ChestEventTypeEnum Type
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
        public int Quantity
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public string Objects
        {
            get;
            set;
        }

        public GuildChestActivityRecord()
        {

        }
    }
}