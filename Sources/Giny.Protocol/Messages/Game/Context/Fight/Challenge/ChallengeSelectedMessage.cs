using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeSelectedMessage : NetworkMessage
    {
        public const ushort Id = 7816;
        public override ushort MessageId => Id;

        public ChallengeInformation challengeInformation;

        public ChallengeSelectedMessage()
        {
        }
        public ChallengeSelectedMessage(ChallengeInformation challengeInformation)
        {
            this.challengeInformation = challengeInformation;
        }
        public override void Serialize(IDataWriter writer)
        {
            challengeInformation.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeInformation = new ChallengeInformation();
            challengeInformation.Deserialize(reader);
        }

    }
}


