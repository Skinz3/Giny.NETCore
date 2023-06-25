using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class RecruitmentInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 9761;
        public override ushort MessageId => Id;

        public GuildRecruitmentInformation recruitmentData;

        public RecruitmentInformationMessage()
        {
        }
        public RecruitmentInformationMessage(GuildRecruitmentInformation recruitmentData)
        {
            this.recruitmentData = recruitmentData;
        }
        public override void Serialize(IDataWriter writer)
        {
            recruitmentData.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruitmentData = new GuildRecruitmentInformation();
            recruitmentData.Deserialize(reader);
        }


    }
}








