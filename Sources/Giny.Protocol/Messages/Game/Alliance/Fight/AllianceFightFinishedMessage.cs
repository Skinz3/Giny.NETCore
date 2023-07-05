using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightFinishedMessage : NetworkMessage
    {
        public const ushort Id = 3017;
        public override ushort MessageId => Id;

        public SocialFightInfo allianceFightInfo;

        public AllianceFightFinishedMessage()
        {
        }
        public AllianceFightFinishedMessage(SocialFightInfo allianceFightInfo)
        {
            this.allianceFightInfo = allianceFightInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceFightInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceFightInfo = new SocialFightInfo();
            allianceFightInfo.Deserialize(reader);
        }

    }
}


