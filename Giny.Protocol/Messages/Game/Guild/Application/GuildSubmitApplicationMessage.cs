using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildSubmitApplicationMessage : NetworkMessage  
    { 
        public  const ushort Id = 6315;
        public override ushort MessageId => Id;

        public string applyText;
        public int guildId;
        public int timeSpent;
        public string filterLanguage;
        public string filterAmbiance;
        public string filterPlaytime;
        public string filterInterest;
        public string filterMinMaxGuildLevel;
        public string filterRecruitmentType;
        public string filterMinMaxCharacterLevel;
        public string filterMinMaxAchievement;
        public string filterSearchName;
        public string filterLastSort;

        public GuildSubmitApplicationMessage()
        {
        }
        public GuildSubmitApplicationMessage(string applyText,int guildId,int timeSpent,string filterLanguage,string filterAmbiance,string filterPlaytime,string filterInterest,string filterMinMaxGuildLevel,string filterRecruitmentType,string filterMinMaxCharacterLevel,string filterMinMaxAchievement,string filterSearchName,string filterLastSort)
        {
            this.applyText = applyText;
            this.guildId = guildId;
            this.timeSpent = timeSpent;
            this.filterLanguage = filterLanguage;
            this.filterAmbiance = filterAmbiance;
            this.filterPlaytime = filterPlaytime;
            this.filterInterest = filterInterest;
            this.filterMinMaxGuildLevel = filterMinMaxGuildLevel;
            this.filterRecruitmentType = filterRecruitmentType;
            this.filterMinMaxCharacterLevel = filterMinMaxCharacterLevel;
            this.filterMinMaxAchievement = filterMinMaxAchievement;
            this.filterSearchName = filterSearchName;
            this.filterLastSort = filterLastSort;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)applyText);
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
            if (timeSpent < 0)
            {
                throw new System.Exception("Forbidden value (" + timeSpent + ") on element timeSpent.");
            }

            writer.WriteVarInt((int)timeSpent);
            writer.WriteUTF((string)filterLanguage);
            writer.WriteUTF((string)filterAmbiance);
            writer.WriteUTF((string)filterPlaytime);
            writer.WriteUTF((string)filterInterest);
            writer.WriteUTF((string)filterMinMaxGuildLevel);
            writer.WriteUTF((string)filterRecruitmentType);
            writer.WriteUTF((string)filterMinMaxCharacterLevel);
            writer.WriteUTF((string)filterMinMaxAchievement);
            writer.WriteUTF((string)filterSearchName);
            writer.WriteUTF((string)filterLastSort);
        }
        public override void Deserialize(IDataReader reader)
        {
            applyText = (string)reader.ReadUTF();
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of GuildSubmitApplicationMessage.guildId.");
            }

            timeSpent = (int)reader.ReadVarUhInt();
            if (timeSpent < 0)
            {
                throw new System.Exception("Forbidden value (" + timeSpent + ") on element of GuildSubmitApplicationMessage.timeSpent.");
            }

            filterLanguage = (string)reader.ReadUTF();
            filterAmbiance = (string)reader.ReadUTF();
            filterPlaytime = (string)reader.ReadUTF();
            filterInterest = (string)reader.ReadUTF();
            filterMinMaxGuildLevel = (string)reader.ReadUTF();
            filterRecruitmentType = (string)reader.ReadUTF();
            filterMinMaxCharacterLevel = (string)reader.ReadUTF();
            filterMinMaxAchievement = (string)reader.ReadUTF();
            filterSearchName = (string)reader.ReadUTF();
            filterLastSort = (string)reader.ReadUTF();
        }


    }
}








