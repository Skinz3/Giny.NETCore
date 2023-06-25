using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("cw")]
    public class WisdomCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
           return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Wisdom.Additional);
        }
    }
    [Criteria("CW")]
    public class TotalWisdomCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Wisdom.Total());
        }
    }
}
