using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ShortcutObjectItem : ShortcutObject  
    { 
        public new const ushort Id = 6224;
        public override ushort TypeId => Id;

        public int itemUID;
        public int itemGID;

        public ShortcutObjectItem()
        {
        }
        public ShortcutObjectItem(int itemUID,int itemGID,byte slot)
        {
            this.itemUID = itemUID;
            this.itemGID = itemGID;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)itemUID);
            writer.WriteInt((int)itemGID);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            itemUID = (int)reader.ReadInt();
            itemGID = (int)reader.ReadInt();
        }


    }
}








