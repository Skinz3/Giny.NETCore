using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class SocialFightLeaveRequestMessage : NetworkMessage
    {
        public const ushort Id = 4465;
        public override ushort MessageId => Id;

        public SocialFightInfo socialFightInfo;

        public SocialFightLeaveRequestMessage()
        {
        }
        public SocialFightLeaveRequestMessage(SocialFightInfo socialFightInfo)
        {
            this.socialFightInfo = socialFightInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            socialFightInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            socialFightInfo = new SocialFightInfo();
            socialFightInfo.Deserialize(reader);
        }

    }
}


