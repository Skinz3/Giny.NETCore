using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyEntityUpdateLightMessage : PartyUpdateLightMessage  
    { 
        public new const ushort Id = 4442;
        public override ushort MessageId => Id;

        public byte indexId;

        public PartyEntityUpdateLightMessage()
        {
        }
        public PartyEntityUpdateLightMessage(byte indexId,int partyId,long id,int lifePoints,int maxLifePoints,short prospecting,byte regenRate)
        {
            this.indexId = indexId;
            this.partyId = partyId;
            this.id = id;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (indexId < 0)
            {
                throw new System.Exception("Forbidden value (" + indexId + ") on element indexId.");
            }

            writer.WriteByte((byte)indexId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            indexId = (byte)reader.ReadByte();
            if (indexId < 0)
            {
                throw new System.Exception("Forbidden value (" + indexId + ") on element of PartyEntityUpdateLightMessage.indexId.");
            }

        }


    }
}








