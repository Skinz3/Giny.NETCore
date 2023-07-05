using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GroupTeleportPlayerOfferMessage : NetworkMessage
    {
        public const ushort Id = 9570;
        public override ushort MessageId => Id;

        public double mapId;
        public short worldX;
        public short worldY;
        public int timeLeft;
        public long requesterId;
        public string requesterName;

        public GroupTeleportPlayerOfferMessage()
        {
        }
        public GroupTeleportPlayerOfferMessage(double mapId, short worldX, short worldY, int timeLeft, long requesterId, string requesterName)
        {
            this.mapId = mapId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.timeLeft = timeLeft;
            this.requesterId = requesterId;
            this.requesterName = requesterName;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
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
            writer.WriteUTF((string)requesterName);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of GroupTeleportPlayerOfferMessage.mapId.");
            }

            worldX = (short)reader.ReadShort();
            worldY = (short)reader.ReadShort();
            timeLeft = (int)reader.ReadVarUhInt();
            if (timeLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeft + ") on element of GroupTeleportPlayerOfferMessage.timeLeft.");
            }

            requesterId = (long)reader.ReadVarUhLong();
            if (requesterId < 0 || requesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element of GroupTeleportPlayerOfferMessage.requesterId.");
            }

            requesterName = (string)reader.ReadUTF();
        }

    }
}


