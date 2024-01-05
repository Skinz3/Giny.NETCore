using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [CriterionHandler("CP")]
    public class ActionPointsCriterion : Criterion
    {
        public ActionPointsCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.ActionPoints.Total());
        }
    }
}
