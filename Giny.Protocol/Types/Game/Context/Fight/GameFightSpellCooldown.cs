using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightSpellCooldown  
    { 
        public const ushort Id = 1808;
        public virtual ushort TypeId => Id;

        public int spellId;
        public byte cooldown;

        public GameFightSpellCooldown()
        {
        }
        public GameFightSpellCooldown(int spellId,byte cooldown)
        {
            this.spellId = spellId;
            this.cooldown = cooldown;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)spellId);
            if (cooldown < 0)
            {
                throw new System.Exception("Forbidden value (" + cooldown + ") on element cooldown.");
            }

            writer.WriteByte((byte)cooldown);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            spellId = (int)reader.ReadInt();
            cooldown = (byte)reader.ReadByte();
            if (cooldown < 0)
            {
                throw new System.Exception("Forbidden value (" + cooldown + ") on element of GameFightSpellCooldown.cooldown.");
            }

        }


    }
}








