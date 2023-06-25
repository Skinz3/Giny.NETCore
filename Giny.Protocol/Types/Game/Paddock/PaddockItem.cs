using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockItem : ObjectItemInRolePlay  
    { 
        public new const ushort Id = 7089;
        public override ushort TypeId => Id;

        public ItemDurability durability;

        public PaddockItem()
        {
        }
        public PaddockItem(ItemDurability durability,short cellId,short objectGID)
        {
            this.durability = durability;
            this.cellId = cellId;
            this.objectGID = objectGID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            durability.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            durability = new ItemDurability();
            durability.Deserialize(reader);
        }


    }
}








