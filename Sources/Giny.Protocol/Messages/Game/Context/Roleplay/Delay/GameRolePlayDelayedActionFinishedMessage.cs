using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameRolePlayDelayedActionFinishedMessage : NetworkMessage
    {
        public const ushort Id = 4342;
        public override ushort MessageId => Id;

        public double delayedCharacterId;
        public byte delayTypeId;

        public GameRolePlayDelayedActionFinishedMessage()
        {
        }
        public GameRolePlayDelayedActionFinishedMessage(double delayedCharacterId, byte delayTypeId)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (delayedCharacterId < -9007199254740992 || delayedCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + delayedCharacterId + ") on element delayedCharacterId.");
            }

            writer.WriteDouble((double)delayedCharacterId);
            writer.WriteByte((byte)delayTypeId);
        }
        public override void Deserialize(IDataReader reader)
        {
            delayedCharacterId = (double)reader.ReadDouble();
            if (delayedCharacterId < -9007199254740992 || delayedCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + delayedCharacterId + ") on element of GameRolePlayDelayedActionFinishedMessage.delayedCharacterId.");
            }

            delayTypeId = (byte)reader.ReadByte();
            if (delayTypeId < 0)
            {
                throw new System.Exception("Forbidden value (" + delayTypeId + ") on element of GameRolePlayDelayedActionFinishedMessage.delayTypeId.");
            }

        }

    }
}


