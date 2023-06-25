using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ShortcutEmote : Shortcut  
    { 
        public new const ushort Id = 3071;
        public override ushort TypeId => Id;

        public short emoteId;

        public ShortcutEmote()
        {
        }
        public ShortcutEmote(short emoteId,byte slot)
        {
            this.emoteId = emoteId;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element emoteId.");
            }

            writer.WriteShort((short)emoteId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            emoteId = (short)reader.ReadUShort();
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element of ShortcutEmote.emoteId.");
            }

        }


    }
}








