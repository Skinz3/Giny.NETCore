using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceUpdateRecruitmentInformationMessage : NetworkMessage
    {
        public const ushort Id = 4203;
        public override ushort MessageId => Id;

        public AllianceRecruitmentInformation recruitmentData;

        public AllianceUpdateRecruitmentInformationMessage()
        {
        }
        public AllianceUpdateRecruitmentInformationMessage(AllianceRecruitmentInformation recruitmentData)
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


