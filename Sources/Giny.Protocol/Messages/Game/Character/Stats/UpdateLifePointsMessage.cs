using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateLifePointsMessage : NetworkMessage  
    { 
        public  const ushort Id = 8920;
        public override ushort MessageId => Id;

        public int lifePoints;
        public int maxLifePoints;

        public UpdateLifePointsMessage()
        {
        }
        public UpdateLifePointsMessage(int lifePoints,int maxLifePoints)
        {
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
        }
        public override void Serialize(IDataWriter writer)
        {
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
        }
        public override void Deserialize(IDataReader reader)
        {
            lifePoints = (int)reader.ReadVarUhInt();
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element of UpdateLifePointsMessage.lifePoints.");
            }

            maxLifePoints = (int)reader.ReadVarUhInt();
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element of UpdateLifePointsMessage.maxLifePoints.");
            }

        }


    }
}








