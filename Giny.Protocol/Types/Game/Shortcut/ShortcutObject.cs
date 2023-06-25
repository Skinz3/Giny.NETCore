using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ShortcutObject : Shortcut  
    { 
        public new const ushort Id = 892;
        public override ushort TypeId => Id;


        public ShortcutObject()
        {
        }
        public ShortcutObject(byte slot)
        {
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








