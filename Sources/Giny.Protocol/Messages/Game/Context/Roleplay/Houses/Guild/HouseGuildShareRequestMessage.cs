using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseGuildShareRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 455;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;
        public bool enable;
        public int rights;

        public HouseGuildShareRequestMessage()
        {
        }
        public HouseGuildShareRequestMessage(int houseId,int instanceId,bool enable,int rights)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
            this.enable = enable;
            this.rights = rights;
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
            writer.WriteBoolean((bool)enable);
            if (rights < 0)
            {
                throw new System.Exception("Forbidden value (" + rights + ") on element rights.");
            }

            writer.WriteVarInt((int)rights);
        }
        public override void Deserialize(IDataReader reader)
        {
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseGuildShareRequestMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseGuildShareRequestMessage.instanceId.");
            }

            enable = (bool)reader.ReadBoolean();
            rights = (int)reader.ReadVarUhInt();
            if (rights < 0)
            {
                throw new System.Exception("Forbidden value (" + rights + ") on element of HouseGuildShareRequestMessage.rights.");
            }

        }


    }
}








