using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AllianceRecruitmentInformation : SocialRecruitmentInformation
    {
        public new const ushort Id = 5237;
        public override ushort TypeId => Id;


        public AllianceRecruitmentInformation()
        {
        }
        public AllianceRecruitmentInformation(int socialId, byte recruitmentType, string recruitmentTitle, string recruitmentText, int[] selectedLanguages, int[] selectedCriterion, short minLevel, bool minLevelFacultative, bool invalidatedByModeration, string lastEditPlayerName, double lastEditDate, bool recruitmentAutoLocked)
        {
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
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}


