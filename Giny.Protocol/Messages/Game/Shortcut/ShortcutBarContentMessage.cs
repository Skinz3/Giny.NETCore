using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShortcutBarContentMessage : NetworkMessage  
    { 
        public  const ushort Id = 3367;
        public override ushort MessageId => Id;

        public byte barType;
        public Shortcut[] shortcuts;

        public ShortcutBarContentMessage()
        {
        }
        public ShortcutBarContentMessage(byte barType,Shortcut[] shortcuts)
        {
            this.barType = barType;
            this.shortcuts = shortcuts;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)barType);
            writer.WriteShort((short)shortcuts.Length);
            for (uint _i2 = 0;_i2 < shortcuts.Length;_i2++)
            {
                writer.WriteShort((short)(shortcuts[_i2] as Shortcut).TypeId);
                (shortcuts[_i2] as Shortcut).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            Shortcut _item2 = null;
            barType = (byte)reader.ReadByte();
            if (barType < 0)
            {
                throw new System.Exception("Forbidden value (" + barType + ") on element of ShortcutBarContentMessage.barType.");
            }

            uint _shortcutsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _shortcutsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<Shortcut>((short)_id2);
                _item2.Deserialize(reader);
                shortcuts[_i2] = _item2;
            }

        }


    }
}








