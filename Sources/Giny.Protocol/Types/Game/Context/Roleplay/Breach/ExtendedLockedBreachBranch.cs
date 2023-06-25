using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ExtendedLockedBreachBranch : ExtendedBreachBranch  
    { 
        public new const ushort Id = 3173;
        public override ushort TypeId => Id;

        public int unlockPrice;

        public ExtendedLockedBreachBranch()
        {
        }
        public ExtendedLockedBreachBranch(int unlockPrice,byte room,int element,MonsterInGroupLightInformations[] bosses,double map,short score,short relativeScore,MonsterInGroupLightInformations[] monsters,BreachReward[] rewards,int modifier,int prize)
        {
            this.unlockPrice = unlockPrice;
            this.room = room;
            this.element = element;
            this.bosses = bosses;
            this.map = map;
            this.score = score;
            this.relativeScore = relativeScore;
            this.monsters = monsters;
            this.rewards = rewards;
            this.modifier = modifier;
            this.prize = prize;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (unlockPrice < 0)
            {
                throw new System.Exception("Forbidden value (" + unlockPrice + ") on element unlockPrice.");
            }

            writer.WriteVarInt((int)unlockPrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            unlockPrice = (int)reader.ReadVarUhInt();
            if (unlockPrice < 0)
            {
                throw new System.Exception("Forbidden value (" + unlockPrice + ") on element of ExtendedLockedBreachBranch.unlockPrice.");
            }

        }


    }
}








