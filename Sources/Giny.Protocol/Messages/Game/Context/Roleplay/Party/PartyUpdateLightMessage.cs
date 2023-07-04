using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PartyUpdateLightMessage : AbstractPartyEventMessage
    {
        public new const ushort Id = 5445;
        public override ushort MessageId => Id;

        public long id;
        public int lifePoints;
        public int maxLifePoints;
        public int prospecting;
        public byte regenRate;

        public PartyUpdateLightMessage()
        {
        }
        public PartyUpdateLightMessage(long id, int lifePoints, int maxLifePoints, int prospecting, byte regenRate, int partyId)
        {
            this.id = id;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (id < 0 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element lifePoints.");
            }

            writer.WriteVarInt((int)lifePoints);
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element maxLifePoints.");
            }

            writer.WriteVarInt((int)maxLifePoints);
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element prospecting.");
            }

            writer.WriteVarInt((int)prospecting);
            if (regenRate < 0 || regenRate > 255)
            {
                throw new System.Exception("Forbidden value (" + regenRate + ") on element regenRate.");
            }

            writer.WriteByte((byte)regenRate);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of PartyUpdateLightMessage.id.");
            }

            lifePoints = (int)reader.ReadVarUhInt();
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element of PartyUpdateLightMessage.lifePoints.");
            }

            maxLifePoints = (int)reader.ReadVarUhInt();
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element of PartyUpdateLightMessage.maxLifePoints.");
            }

            prospecting = (int)reader.ReadVarUhInt();
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element of PartyUpdateLightMessage.prospecting.");
            }

            regenRate = (byte)reader.ReadSByte();
            if (regenRate < 0 || regenRate > 255)
            {
                throw new System.Exception("Forbidden value (" + regenRate + ") on element of PartyUpdateLightMessage.regenRate.");
            }

        }

    }
}


