using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightResultTaxCollectorListEntry : FightResultFighterListEntry
    {
        public new const ushort Id = 4665;
        public override ushort TypeId => Id;

        public BasicAllianceInformations allianceInfo;

        public FightResultTaxCollectorListEntry()
        {
        }
        public FightResultTaxCollectorListEntry(BasicAllianceInformations allianceInfo, short outcome, byte wave, FightLoot rewards, double id, bool alive)
        {
            this.allianceInfo = allianceInfo;
            this.outcome = outcome;
            this.wave = wave;
            this.rewards = rewards;
            this.id = id;
            this.alive = alive;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfo = new BasicAllianceInformations();
            allianceInfo.Deserialize(reader);
        }


    }
}


