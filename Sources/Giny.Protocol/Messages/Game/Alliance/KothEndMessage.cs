using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class KothEndMessage : NetworkMessage
    {
        public const ushort Id = 173;
        public override ushort MessageId => Id;

        public KothWinner winner;

        public KothEndMessage()
        {
        }
        public KothEndMessage(KothWinner winner)
        {
            this.winner = winner;
        }
        public override void Serialize(IDataWriter writer)
        {
            winner.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            winner = new KothWinner();
            winner.Deserialize(reader);
        }

    }
}


