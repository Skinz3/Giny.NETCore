using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public interface IFightResult
    {
        bool Alive
        {
            get;
        }
        int Id
        {
            get;
        }
        int Prospecting
        {
            get;
        }
        int Wisdom
        {
            get;
        }
        int Level
        {
            get;
        }
        Loot Loot
        {
            get;
        }
        FightOutcomeEnum Outcome
        {
            get;
        }
        Fight Fight
        {
            get;
        }

        bool CanLoot(FightTeam looters);

        FightResultListEntry GetFightResultListEntry();

        void Apply();
    }
}
