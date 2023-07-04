using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildPlayerRankUpdateActivity : GuildLogbookEntryBasicInformation
    {
        public new const ushort Id = 1236;
        public override ushort TypeId => Id;

        public RankMinimalInformation guildRankMinimalInfos;
        public long sourcePlayerId;
        public long targetPlayerId;
        public string sourcePlayerName;
        public string targetPlayerName;

        public GuildPlayerRankUpdateActivity()
        {
        }
        public GuildPlayerRankUpdateActivity(RankMinimalInformation guildRankMinimalInfos, long sourcePlayerId, long targetPlayerId, string sourcePlayerName, string targetPlayerName, int id, double date)
        {
            this.guildRankMinimalInfos = guildRankMinimalInfos;
            this.sourcePlayerId = sourcePlayerId;
            this.targetPlayerId = targetPlayerId;
            this.sourcePlayerName = sourcePlayerName;
            this.targetPlayerName = targetPlayerName;
            this.id = id;
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildRankMinimalInfos.Serialize(writer);
            if (sourcePlayerId < 0 || sourcePlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + sourcePlayerId + ") on element sourcePlayerId.");
            }

            writer.WriteVarLong((long)sourcePlayerId);
            if (targetPlayerId < 0 || targetPlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetPlayerId + ") on element targetPlayerId.");
            }

            writer.WriteVarLong((long)targetPlayerId);
            writer.WriteUTF((string)sourcePlayerName);
            writer.WriteUTF((string)targetPlayerName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildRankMinimalInfos = new RankMinimalInformation();
            guildRankMinimalInfos.Deserialize(reader);
            sourcePlayerId = (long)reader.ReadVarUhLong();
            if (sourcePlayerId < 0 || sourcePlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + sourcePlayerId + ") on element of GuildPlayerRankUpdateActivity.sourcePlayerId.");
            }

            targetPlayerId = (long)reader.ReadVarUhLong();
            if (targetPlayerId < 0 || targetPlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetPlayerId + ") on element of GuildPlayerRankUpdateActivity.targetPlayerId.");
            }

            sourcePlayerName = (string)reader.ReadUTF();
            targetPlayerName = (string)reader.ReadUTF();
        }


    }
}


