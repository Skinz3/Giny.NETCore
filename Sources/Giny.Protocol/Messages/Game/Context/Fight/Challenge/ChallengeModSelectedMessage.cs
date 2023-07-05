using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeModSelectedMessage : NetworkMessage
    {
        public const ushort Id = 2631;
        public override ushort MessageId => Id;

        public byte challengeMod;

        public ChallengeModSelectedMessage()
        {
        }
        public ChallengeModSelectedMessage(byte challengeMod)
        {
            this.challengeMod = challengeMod;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)challengeMod);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeMod = (byte)reader.ReadByte();
            if (challengeMod < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeMod + ") on element of ChallengeModSelectedMessage.challengeMod.");
            }

        }

    }
}


