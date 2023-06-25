using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class NamedPartyTeamWithOutcome  
    { 
        public const ushort Id = 7068;
        public virtual ushort TypeId => Id;

        public NamedPartyTeam team;
        public short outcome;

        public NamedPartyTeamWithOutcome()
        {
        }
        public NamedPartyTeamWithOutcome(NamedPartyTeam team,short outcome)
        {
            this.team = team;
            this.outcome = outcome;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            team.Serialize(writer);
            writer.WriteVarShort((short)outcome);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            team = new NamedPartyTeam();
            team.Deserialize(reader);
            outcome = (short)reader.ReadVarUhShort();
            if (outcome < 0)
            {
                throw new System.Exception("Forbidden value (" + outcome + ") on element of NamedPartyTeamWithOutcome.outcome.");
            }

        }


    }
}








