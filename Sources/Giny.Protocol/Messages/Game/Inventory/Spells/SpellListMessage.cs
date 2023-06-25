using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SpellListMessage : NetworkMessage  
    { 
        public  const ushort Id = 967;
        public override ushort MessageId => Id;

        public bool spellPrevisualization;
        public SpellItem[] spells;

        public SpellListMessage()
        {
        }
        public SpellListMessage(bool spellPrevisualization,SpellItem[] spells)
        {
            this.spellPrevisualization = spellPrevisualization;
            this.spells = spells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)spellPrevisualization);
            writer.WriteShort((short)spells.Length);
            for (uint _i2 = 0;_i2 < spells.Length;_i2++)
            {
                (spells[_i2] as SpellItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            SpellItem _item2 = null;
            spellPrevisualization = (bool)reader.ReadBoolean();
            uint _spellsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _spellsLen;_i2++)
            {
                _item2 = new SpellItem();
                _item2.Deserialize(reader);
                spells[_i2] = _item2;
            }

        }


    }
}








