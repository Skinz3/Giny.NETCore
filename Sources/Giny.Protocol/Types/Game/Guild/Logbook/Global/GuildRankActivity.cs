using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildRankActivity : GuildLogbookEntryBasicInformation
    {
        public new const ushort Id = 7599;
        public override ushort TypeId => Id;

        public byte rankActivityType;
        public RankMinimalInformation guildRankMinimalInfos;

        public GuildRankActivity()
        {
        }
        public GuildRankActivity(byte rankActivityType, RankMinimalInformation guildRankMinimalInfos, int id, double date)
        {
            this.rankActivityType = rankActivityType;
            this.guildRankMinimalInfos = guildRankMinimalInfos;
            this.id = id;
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)rankActivityType);
            guildRankMinimalInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rankActivityType = (byte)reader.ReadByte();
            if (rankActivityType < 0)
            {
                throw new System.Exception("Forbidden value (" + rankActivityType + ") on element of GuildRankActivity.rankActivityType.");
            }

            guildRankMinimalInfos = new RankMinimalInformation();
            guildRankMinimalInfos.Deserialize(reader);
        }


    }
}


