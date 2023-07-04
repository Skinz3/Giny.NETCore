using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TeleportPlayerAnswerMessage : NetworkMessage
    {
        public const ushort Id = 2730;
        public override ushort MessageId => Id;

        public bool accept;
        public long requesterId;

        public TeleportPlayerAnswerMessage()
        {
        }
        public TeleportPlayerAnswerMessage(bool accept, long requesterId)
        {
            this.accept = accept;
            this.requesterId = requesterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)accept);
            if (requesterId < 0 || requesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element requesterId.");
            }

            writer.WriteVarLong((long)requesterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            accept = (bool)reader.ReadBoolean();
            requesterId = (long)reader.ReadVarUhLong();
            if (requesterId < 0 || requesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element of TeleportPlayerAnswerMessage.requesterId.");
            }

        }

    }
}


