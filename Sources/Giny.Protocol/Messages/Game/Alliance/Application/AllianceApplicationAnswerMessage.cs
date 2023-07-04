using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationAnswerMessage : NetworkMessage
    {
        public const ushort Id = 5494;
        public override ushort MessageId => Id;

        public bool accepted;
        public long playerId;

        public AllianceApplicationAnswerMessage()
        {
        }
        public AllianceApplicationAnswerMessage(bool accepted, long playerId)
        {
            this.accepted = accepted;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)accepted);
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            accepted = (bool)reader.ReadBoolean();
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AllianceApplicationAnswerMessage.playerId.");
            }

        }

    }
}


