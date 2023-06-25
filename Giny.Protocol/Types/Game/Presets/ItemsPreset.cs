using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ItemsPreset : Preset  
    { 
        public new const ushort Id = 559;
        public override ushort TypeId => Id;

        public ItemForPreset[] items;
        public bool mountEquipped;
        public EntityLook look;

        public ItemsPreset()
        {
        }
        public ItemsPreset(ItemForPreset[] items,bool mountEquipped,EntityLook look,short id)
        {
            this.items = items;
            this.mountEquipped = mountEquipped;
            this.look = look;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)items.Length);
            for (uint _i1 = 0;_i1 < items.Length;_i1++)
            {
                (items[_i1] as ItemForPreset).Serialize(writer);
            }

            writer.WriteBoolean((bool)mountEquipped);
            look.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            ItemForPreset _item1 = null;
            base.Deserialize(reader);
            uint _itemsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _itemsLen;_i1++)
            {
                _item1 = new ItemForPreset();
                _item1.Deserialize(reader);
                items[_i1] = _item1;
            }

            mountEquipped = (bool)reader.ReadBoolean();
            look = new EntityLook();
            look.Deserialize(reader);
        }


    }
}








