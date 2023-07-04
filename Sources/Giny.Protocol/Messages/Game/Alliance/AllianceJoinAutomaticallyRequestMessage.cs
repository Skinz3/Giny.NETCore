using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceJoinAutomaticallyRequestMessage : NetworkMessage
    {
        public const ushort Id = 7226;
        public override ushort MessageId => Id;

        public int allianceId;

        public AllianceJoinAutomaticallyRequestMessage()
        {
        }
        public AllianceJoinAutomaticallyRequestMessage(int allianceId)
        {
            this.allianceId = allianceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)allianceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceId = (int)reader.ReadInt();
        }

    }
}


