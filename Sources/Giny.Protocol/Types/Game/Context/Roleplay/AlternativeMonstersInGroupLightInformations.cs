using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AlternativeMonstersInGroupLightInformations  
    { 
        public const ushort Id = 1054;
        public virtual ushort TypeId => Id;

        public int playerCount;
        public MonsterInGroupLightInformations[] monsters;

        public AlternativeMonstersInGroupLightInformations()
        {
        }
        public AlternativeMonstersInGroupLightInformations(int playerCount,MonsterInGroupLightInformations[] monsters)
        {
            this.playerCount = playerCount;
            this.monsters = monsters;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)playerCount);
            writer.WriteShort((short)monsters.Length);
            for (uint _i2 = 0;_i2 < monsters.Length;_i2++)
            {
                (monsters[_i2] as MonsterInGroupLightInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            MonsterInGroupLightInformations _item2 = null;
            playerCount = (int)reader.ReadInt();
            uint _monstersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _monstersLen;_i2++)
            {
                _item2 = new MonsterInGroupLightInformations();
                _item2.Deserialize(reader);
                monsters[_i2] = _item2;
            }

        }


    }
}








