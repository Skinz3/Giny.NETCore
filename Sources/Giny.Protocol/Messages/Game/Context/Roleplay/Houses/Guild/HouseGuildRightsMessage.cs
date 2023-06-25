using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseGuildRightsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6131;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;
        public bool secondHand;
        public GuildInformations guildInfo;
        public int rights;

        public HouseGuildRightsMessage()
        {
        }
        public HouseGuildRightsMessage(int houseId,int instanceId,bool secondHand,GuildInformations guildInfo,int rights)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
            this.secondHand = secondHand;
            this.guildInfo = guildInfo;
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
            writer.WriteBoolean((bool)secondHand);
            guildInfo.Serialize(writer);
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
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseGuildRightsMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseGuildRightsMessage.instanceId.");
            }

            secondHand = (bool)reader.ReadBoolean();
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
            rights = (int)reader.ReadVarUhInt();
            if (rights < 0)
            {
                throw new System.Exception("Forbidden value (" + rights + ") on element of HouseGuildRightsMessage.rights.");
            }

        }


    }
}








