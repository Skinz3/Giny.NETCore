using Giny.Protocol.Custom.Enums;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using Giny.World.Managers.Criterias;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("PT")]
    public class HasSpellCriterion : Criterion
    {
        public HasSpellCriterion(string raw) : base(raw)
        {

        }
        public override bool Eval(WorldClient client)
        {
            var flag = client.Character.HasSpell(short.Parse(Value));

            switch (Operator)
            {
                case CriterionComparaisonOperator.Equal: return flag;
                case CriterionComparaisonOperator.Negation: return !flag;
            }

            throw new NotImplementedException("Unknown criterion operator for has spell criterion " + Operator);
        }
    }
}
