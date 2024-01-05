using System;
using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using ProtoBuf;

namespace Giny.World.Records.Guilds
{

    //int order,int gfxId,bool modifiable,int[] rights,int id,string name

    [ProtoContract]
    public class GuildRankRecord
    {
        [ProtoMember(1)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int Order
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public int GfxId
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public bool Modifiable
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public GuildRightsEnum[] Rights
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public string Name
        {
            get;
            set;
        }

        public GuildRankRecord()
        {

        }
        public GuildRankRecord(int order, int gfxId, bool modifiable, GuildRightsEnum[] rights, int id, string name)
        {
            this.Order = order;
            this.GfxId = gfxId;
            this.Modifiable = modifiable;
            this.Rights = rights;
            this.Id = id;
            this.Name = name;
        }

        public RankInformation ToGuildRankInformation()
        {
            return new RankInformation()
            {
                order = Order,
                gfxId = GfxId,
                modifiable = Modifiable,
                rights = (Rights ?? Enumerable.Empty<GuildRightsEnum>()).Select(x => (int)x).ToArray(),
                id = Id,
                name = Name
            };
        }

        public RankMinimalInformation ToGuildRankMinimalInformation()
        {
            return new RankMinimalInformation()
            {
                id = Id,
                name = Name
            };
        }

        public override string ToString()
        {
            string rightsString = Rights != null ? string.Join(", ", Rights) : "null";

            return $"Id: {Id}, Order: {Order}, GfxId: {GfxId}, Modifiable: {Modifiable}, " +
                   $"Rights: [{rightsString}], Name: {Name}";
        }
    }
}