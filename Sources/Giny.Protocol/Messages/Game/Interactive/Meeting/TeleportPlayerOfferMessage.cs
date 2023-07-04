using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TeleportPlayerOfferMessage : NetworkMessage
    {
        public const ushort Id = 4442;
        public override ushort MessageId => Id;

        public double mapId;
        public string message;
        public int timeLeft;
        public long requesterId;

        public TeleportPlayerOfferMessage()
        {
        }
        public TeleportPlayerOfferMessage(double mapId, string message, int timeLeft, long requesterId)
        {
            this.mapId = mapId;
            this.message = message;
            this.timeLeft = timeLeft;
            this.requesterId = requesterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteUTF((string)message);
            if (timeLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeft + ") on element timeLeft.");
            }

            writer.WriteVarInt((int)timeLeft);
            if (requesterId < 0 || requesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element requesterId.");
            }

            writer.WriteVarLong((long)requesterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of TeleportPlayerOfferMessage.mapId.");
            }

            message = (string)reader.ReadUTF();
            timeLeft = (int)reader.ReadVarUhInt();
            if (timeLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeft + ") on element of TeleportPlayerOfferMessage.timeLeft.");
            }

            requesterId = (long)reader.ReadVarUhLong();
            if (requesterId < 0 || requesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element of TeleportPlayerOfferMessage.requesterId.");
            }

        }

    }
}


