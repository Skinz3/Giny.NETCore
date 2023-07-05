using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationReceivedMessage : NetworkMessage
    {
        public const ushort Id = 8915;
        public override ushort MessageId => Id;

        public string playerName;
        public long playerId;

        public AllianceApplicationReceivedMessage()
        {
        }
        public AllianceApplicationReceivedMessage(string playerName, long playerId)
        {
            this.playerName = playerName;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)playerName);
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerName = (string)reader.ReadUTF();
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AllianceApplicationReceivedMessage.playerId.");
            }

        }

    }
}


