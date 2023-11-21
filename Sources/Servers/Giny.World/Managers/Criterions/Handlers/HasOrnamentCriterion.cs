using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("OR")]
    public class HasOrnamentCriterion : Criterion
    {
        public HasOrnamentCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            bool flag = client.Character.HasOrnament(short.Parse(Value));

            switch (Operator)
            {
                case CriterionComparaisonOperator.Equal:
                    return flag;
                case CriterionComparaisonOperator.Negation:
                    return !flag;
            }

            throw new NotImplementedException("Invalid operator to eval ornament criterion " + Operator);
        }
    }

}
