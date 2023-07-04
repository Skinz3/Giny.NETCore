using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeFightJoinRefusedMessage : NetworkMessage
    {
        public const ushort Id = 7150;
        public override ushort MessageId => Id;

        public long playerId;
        public byte reason;

        public ChallengeFightJoinRefusedMessage()
        {
        }
        public ChallengeFightJoinRefusedMessage(long playerId, byte reason)
        {
            this.playerId = playerId;
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteByte((byte)reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of ChallengeFightJoinRefusedMessage.playerId.");
            }

            reason = (byte)reader.ReadByte();
        }

    }
}


