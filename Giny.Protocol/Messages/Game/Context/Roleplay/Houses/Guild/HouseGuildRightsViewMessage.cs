using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseGuildRightsViewMessage : NetworkMessage  
    { 
        public  const ushort Id = 6806;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;

        public HouseGuildRightsViewMessage()
        {
        }
        public HouseGuildRightsViewMessage(int houseId,int instanceId)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
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
        }
        public override void Deserialize(IDataReader reader)
        {
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseGuildRightsViewMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseGuildRightsViewMessage.instanceId.");
            }

        }


    }
}








