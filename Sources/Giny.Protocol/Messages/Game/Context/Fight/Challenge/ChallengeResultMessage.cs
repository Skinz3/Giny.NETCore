using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeResultMessage : NetworkMessage
    {
        public const ushort Id = 6302;
        public override ushort MessageId => Id;

        public int challengeId;
        public bool success;

        public ChallengeResultMessage()
        {
        }
        public ChallengeResultMessage(int challengeId, bool success)
        {
            this.challengeId = challengeId;
            this.success = success;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element challengeId.");
            }

            writer.WriteVarInt((int)challengeId);
            writer.WriteBoolean((bool)success);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeId = (int)reader.ReadVarUhInt();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeResultMessage.challengeId.");
            }

            success = (bool)reader.ReadBoolean();
        }

    }
}


