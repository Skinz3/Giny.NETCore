using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [CriterionHandler("ca")]
    public class AgilityCriterion : Criterion
    {
        public AgilityCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.Agility.Additional);
        }
    }
    [CriterionHandler("CA")]
    public class TotalAgilityCriterion : Criterion
    {
        public TotalAgilityCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.Agility.Total());
        }
    }
}
