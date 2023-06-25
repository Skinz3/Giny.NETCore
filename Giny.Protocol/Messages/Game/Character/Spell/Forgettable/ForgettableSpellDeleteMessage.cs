using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ForgettableSpellDeleteMessage : NetworkMessage  
    { 
        public  const ushort Id = 3832;
        public override ushort MessageId => Id;

        public byte reason;
        public int[] spells;

        public ForgettableSpellDeleteMessage()
        {
        }
        public ForgettableSpellDeleteMessage(byte reason,int[] spells)
        {
            this.reason = reason;
            this.spells = spells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)reason);
            writer.WriteShort((short)spells.Length);
            for (uint _i2 = 0;_i2 < spells.Length;_i2++)
            {
                if (spells[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + spells[_i2] + ") on element 2 (starting at 1) of spells.");
                }

                writer.WriteInt((int)spells[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            reason = (byte)reader.ReadByte();
            if (reason < 0)
            {
                throw new System.Exception("Forbidden value (" + reason + ") on element of ForgettableSpellDeleteMessage.reason.");
            }

            uint _spellsLen = (uint)reader.ReadUShort();
            spells = new int[_spellsLen];
            for (uint _i2 = 0;_i2 < _spellsLen;_i2++)
            {
                _val2 = (uint)reader.ReadInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of spells.");
                }

                spells[_i2] = (int)_val2;
            }

        }


    }
}








