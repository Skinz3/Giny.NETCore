using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceRecruitmentInformationMessage : NetworkMessage
    {
        public const ushort Id = 9279;
        public override ushort MessageId => Id;

        public AllianceRecruitmentInformation recruitmentData;

        public AllianceRecruitmentInformationMessage()
        {
        }
        public AllianceRecruitmentInformationMessage(AllianceRecruitmentInformation recruitmentData)
        {
            this.recruitmentData = recruitmentData;
        }
        public override void Serialize(IDataWriter writer)
        {
            recruitmentData.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruitmentData = new AllianceRecruitmentInformation();
            recruitmentData.Deserialize(reader);
        }

    }
}


