using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("ca")]
    public class AgilityCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Agility.Additional);
        }
    }
    [Criteria("CA")]
    public class TotalAgilityCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Agility.Total());
        }
    }
}
