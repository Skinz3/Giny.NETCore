using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShortcutBarRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 9231;
        public override ushort MessageId => Id;

        public byte barType;
        public byte slot;

        public ShortcutBarRemovedMessage()
        {
        }
        public ShortcutBarRemovedMessage(byte barType,byte slot)
        {
            this.barType = barType;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)barType);
            if (slot < 0 || slot > 99)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element slot.");
            }

            writer.WriteByte((byte)slot);
        }
        public override void Deserialize(IDataReader reader)
        {
            barType = (byte)reader.ReadByte();
            if (barType < 0)
            {
                throw new System.Exception("Forbidden value (" + barType + ") on element of ShortcutBarRemovedMessage.barType.");
            }

            slot = (byte)reader.ReadByte();
            if (slot < 0 || slot > 99)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element of ShortcutBarRemovedMessage.slot.");
            }

        }


    }
}








