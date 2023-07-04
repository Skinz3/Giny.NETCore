using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceListApplicationModifiedMessage : NetworkMessage
    {
        public const ushort Id = 9993;
        public override ushort MessageId => Id;

        public SocialApplicationInformation apply;
        public byte state;
        public long playerId;

        public AllianceListApplicationModifiedMessage()
        {
        }
        public AllianceListApplicationModifiedMessage(SocialApplicationInformation apply, byte state, long playerId)
        {
            this.apply = apply;
            this.state = state;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            apply.Serialize(writer);
            writer.WriteByte((byte)state);
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            apply = new SocialApplicationInformation();
            apply.Deserialize(reader);
            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of AllianceListApplicationModifiedMessage.state.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AllianceListApplicationModifiedMessage.playerId.");
            }

        }

    }
}


