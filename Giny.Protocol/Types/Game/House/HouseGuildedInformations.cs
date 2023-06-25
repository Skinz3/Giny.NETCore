using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HouseGuildedInformations : HouseInstanceInformations  
    { 
        public new const ushort Id = 7677;
        public override ushort TypeId => Id;

        public GuildInformations guildInfo;

        public HouseGuildedInformations()
        {
        }
        public HouseGuildedInformations(GuildInformations guildInfo,int instanceId,bool secondHand,bool isLocked,AccountTagInformation ownerTag,bool hasOwner,long price,bool isSaleLocked)
        {
            this.guildInfo = guildInfo;
            this.instanceId = instanceId;
            this.secondHand = secondHand;
            this.isLocked = isLocked;
            this.ownerTag = ownerTag;
            this.hasOwner = hasOwner;
            this.price = price;
            this.isSaleLocked = isSaleLocked;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
        }


    }
}








