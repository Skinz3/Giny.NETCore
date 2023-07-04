using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildRecruitmentInformation : SocialRecruitmentInformation
    {
        public new const ushort Id = 5988;
        public override ushort TypeId => Id;

        public int minSuccess;
        public bool minSuccessFacultative;

        public GuildRecruitmentInformation()
        {
        }
        public GuildRecruitmentInformation(int minSuccess, bool minSuccessFacultative, int socialId, byte recruitmentType, string recruitmentTitle, string recruitmentText, int[] selectedLanguages, int[] selectedCriterion, short minLevel, bool minLevelFacultative, bool invalidatedByModeration, string lastEditPlayerName, double lastEditDate, bool recruitmentAutoLocked)
        {
            this.minSuccess = minSuccess;
            this.minSuccessFacultative = minSuccessFacultative;
            this.socialId = socialId;
            this.recruitmentType = recruitmentType;
            this.recruitmentTitle = recruitmentTitle;
            this.recruitmentText = recruitmentText;
            this.selectedLanguages = selectedLanguages;
            this.selectedCriterion = selectedCriterion;
            this.minLevel = minLevel;
            this.minLevelFacultative = minLevelFacultative;
            this.invalidatedByModeration = invalidatedByModeration;
            this.lastEditPlayerName = lastEditPlayerName;
            this.lastEditDate = lastEditDate;
            this.recruitmentAutoLocked = recruitmentAutoLocked;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (minSuccess < 0)
            {
                throw new System.Exception("Forbidden value (" + minSuccess + ") on element minSuccess.");
            }

            writer.WriteVarInt((int)minSuccess);
            writer.WriteBoolean((bool)minSuccessFacultative);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            minSuccess = (int)reader.ReadVarUhInt();
            if (minSuccess < 0)
            {
                throw new System.Exception("Forbidden value (" + minSuccess + ") on element of GuildRecruitmentInformation.minSuccess.");
            }

            minSuccessFacultative = (bool)reader.ReadBoolean();
        }


    }
}


