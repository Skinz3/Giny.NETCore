using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachGameFightEndMessage : GameFightEndMessage  
    { 
        public new const ushort Id = 1483;
        public override ushort MessageId => Id;

        public int budget;

        public BreachGameFightEndMessage()
        {
        }
        public BreachGameFightEndMessage(int budget,int duration,short rewardRate,short lootShareLimitMalus,FightResultListEntry[] results,NamedPartyTeamWithOutcome[] namedPartyTeamsOutcomes)
        {
            this.budget = budget;
            this.duration = duration;
            this.rewardRate = rewardRate;
            this.lootShareLimitMalus = lootShareLimitMalus;
            this.results = results;
            this.namedPartyTeamsOutcomes = namedPartyTeamsOutcomes;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)budget);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            budget = (int)reader.ReadInt();
        }


    }
}








