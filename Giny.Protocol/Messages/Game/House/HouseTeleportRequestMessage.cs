using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseTeleportRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4212;
        public override ushort MessageId => Id;

        public int houseId;
        public int houseInstanceId;

        public HouseTeleportRequestMessage()
        {
        }
        public HouseTeleportRequestMessage(int houseId,int houseInstanceId)
        {
            this.houseId = houseId;
            this.houseInstanceId = houseInstanceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            if (houseInstanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseInstanceId + ") on element houseInstanceId.");
            }

            writer.WriteInt((int)houseInstanceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseTeleportRequestMessage.houseId.");
            }

            houseInstanceId = (int)reader.ReadInt();
            if (houseInstanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseInstanceId + ") on element of HouseTeleportRequestMessage.houseInstanceId.");
            }

        }


    }
}








