using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceModificationEmblemValidMessage : NetworkMessage
    {
        public const ushort Id = 359;
        public override ushort MessageId => Id;

        public SocialEmblem allianceEmblem;

        public AllianceModificationEmblemValidMessage()
        {
        }
        public AllianceModificationEmblemValidMessage(SocialEmblem allianceEmblem)
        {
            this.allianceEmblem = allianceEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceEmblem = new SocialEmblem();
            allianceEmblem.Deserialize(reader);
        }

    }
}


