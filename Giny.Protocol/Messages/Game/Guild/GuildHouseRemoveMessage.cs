using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildHouseRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 1663;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;
        public bool secondHand;

        public GuildHouseRemoveMessage()
        {
        }
        public GuildHouseRemoveMessage(int houseId,int instanceId,bool secondHand)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
            this.secondHand = secondHand;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }

            writer.WriteInt((int)instanceId);
            writer.WriteBoolean((bool)secondHand);
        }
        public override void Deserialize(IDataReader reader)
        {
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of GuildHouseRemoveMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of GuildHouseRemoveMessage.instanceId.");
            }

            secondHand = (bool)reader.ReadBoolean();
        }


    }
}








