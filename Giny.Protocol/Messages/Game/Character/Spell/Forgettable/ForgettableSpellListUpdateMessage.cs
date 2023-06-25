using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ForgettableSpellListUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 584;
        public override ushort MessageId => Id;

        public byte action;
        public ForgettableSpellItem[] spells;

        public ForgettableSpellListUpdateMessage()
        {
        }
        public ForgettableSpellListUpdateMessage(byte action,ForgettableSpellItem[] spells)
        {
            this.action = action;
            this.spells = spells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)action);
            writer.WriteShort((short)spells.Length);
            for (uint _i2 = 0;_i2 < spells.Length;_i2++)
            {
                (spells[_i2] as ForgettableSpellItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ForgettableSpellItem _item2 = null;
            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of ForgettableSpellListUpdateMessage.action.");
            }

            uint _spellsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _spellsLen;_i2++)
            {
                _item2 = new ForgettableSpellItem();
                _item2.Deserialize(reader);
                spells[_i2] = _item2;
            }

        }


    }
}








