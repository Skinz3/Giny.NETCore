using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildPlayerRankUpdateActivity : GuildLogbookEntryBasicInformation  
    { 
        public new const ushort Id = 9478;
        public override ushort TypeId => Id;

        public GuildRankMinimalInformation guildRankMinimalInfos;
        public long playerId;
        public string playerName;

        public GuildPlayerRankUpdateActivity()
        {
        }
        public GuildPlayerRankUpdateActivity(GuildRankMinimalInformation guildRankMinimalInfos,long playerId,string playerName,int id,double date)
        {
            this.guildRankMinimalInfos = guildRankMinimalInfos;
            this.playerId = playerId;
            this.playerName = playerName;
            this.id = id;
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildRankMinimalInfos.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildRankMinimalInfos = new GuildRankMinimalInformation();
            guildRankMinimalInfos.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildPlayerRankUpdateActivity.playerId.");
            }

            playerName = (string)reader.ReadUTF();
        }


    }
}








