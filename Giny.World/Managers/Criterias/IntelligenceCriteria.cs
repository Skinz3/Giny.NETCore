using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("ci")]
    public class IntelligenceCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Intelligence.Additional);
        }
    }
    [Criteria("CI")]
    public class TotalIntelligenceCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Intelligence.Total());
        }
    }
}
