using System;
using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using ProtoBuf;

namespace Giny.World.Records.Guilds
{
    [ProtoContract]
    public class GuildGlobalActivityRecord
    {


        [ProtoMember(1)]
        public DateTime Date
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public GuildGlobalActivityTypeEnum Type
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public string Param1
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public string Param2
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public string Param3
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public string Param4
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public string Param5
        {
            get;
            set;
        }

        public GuildGlobalActivityRecord()
        {

        }

        public GuildGlobalActivityRecord(DateTime date, GuildGlobalActivityTypeEnum type, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            Date = date;
            Type = type;
            Param1 = param1;
            Param2 = param2;
            Param3 = param3;
            Param4 = param4;
            Param5 = param5;
        }
    }
}