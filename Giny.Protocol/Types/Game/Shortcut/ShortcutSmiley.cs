using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ShortcutSmiley : Shortcut  
    { 
        public new const ushort Id = 4874;
        public override ushort TypeId => Id;

        public short smileyId;

        public ShortcutSmiley()
        {
        }
        public ShortcutSmiley(short smileyId,byte slot)
        {
            this.smileyId = smileyId;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element smileyId.");
            }

            writer.WriteVarShort((short)smileyId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            smileyId = (short)reader.ReadVarUhShort();
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element of ShortcutSmiley.smileyId.");
            }

        }


    }
}








