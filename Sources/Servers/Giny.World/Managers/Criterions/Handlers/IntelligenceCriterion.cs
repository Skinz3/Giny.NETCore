using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("ci")]
    public class IntelligenceCriterion : Criterion
    {
        public IntelligenceCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.Intelligence.Additional);
        }
    }
    [Criterion("CI")]
    public class TotalIntelligenceCriterion : Criterion
    {
        public TotalIntelligenceCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.Intelligence.Total());
        }
    }
}
