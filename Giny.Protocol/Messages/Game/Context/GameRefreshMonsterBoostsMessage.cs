using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRefreshMonsterBoostsMessage : NetworkMessage  
    { 
        public  const ushort Id = 8657;
        public override ushort MessageId => Id;

        public MonsterBoosts[] monsterBoosts;
        public MonsterBoosts[] familyBoosts;

        public GameRefreshMonsterBoostsMessage()
        {
        }
        public GameRefreshMonsterBoostsMessage(MonsterBoosts[] monsterBoosts,MonsterBoosts[] familyBoosts)
        {
            this.monsterBoosts = monsterBoosts;
            this.familyBoosts = familyBoosts;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)monsterBoosts.Length);
            for (uint _i1 = 0;_i1 < monsterBoosts.Length;_i1++)
            {
                (monsterBoosts[_i1] as MonsterBoosts).Serialize(writer);
            }

            writer.WriteShort((short)familyBoosts.Length);
            for (uint _i2 = 0;_i2 < familyBoosts.Length;_i2++)
            {
                (familyBoosts[_i2] as MonsterBoosts).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MonsterBoosts _item1 = null;
            MonsterBoosts _item2 = null;
            uint _monsterBoostsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _monsterBoostsLen;_i1++)
            {
                _item1 = new MonsterBoosts();
                _item1.Deserialize(reader);
                monsterBoosts[_i1] = _item1;
            }

            uint _familyBoostsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _familyBoostsLen;_i2++)
            {
                _item2 = new MonsterBoosts();
                _item2.Deserialize(reader);
                familyBoosts[_i2] = _item2;
            }

        }


    }
}








