using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationPresenceMessage : NetworkMessage
    {
        public const ushort Id = 3867;
        public override ushort MessageId => Id;

        public bool isApplication;

        public AllianceApplicationPresenceMessage()
        {
        }
        public AllianceApplicationPresenceMessage(bool isApplication)
        {
            this.isApplication = isApplication;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)isApplication);
        }
        public override void Deserialize(IDataReader reader)
        {
            isApplication = (bool)reader.ReadBoolean();
        }

    }
}


