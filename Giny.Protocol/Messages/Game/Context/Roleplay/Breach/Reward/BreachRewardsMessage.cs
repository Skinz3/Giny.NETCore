using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachRewardsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6368;
        public override ushort MessageId => Id;

        public BreachReward[] rewards;

        public BreachRewardsMessage()
        {
        }
        public BreachRewardsMessage(BreachReward[] rewards)
        {
            this.rewards = rewards;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)rewards.Length);
            for (uint _i1 = 0;_i1 < rewards.Length;_i1++)
            {
                (rewards[_i1] as BreachReward).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            BreachReward _item1 = null;
            uint _rewardsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _rewardsLen;_i1++)
            {
                _item1 = new BreachReward();
                _item1.Deserialize(reader);
                rewards[_i1] = _item1;
            }

        }


    }
}








