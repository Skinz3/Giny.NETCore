using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("TL")]
    public class HasTitleCriterion : Criterion
    {
        public HasTitleCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            bool obj = client.Character.HasTitle(short.Parse(CriteriaValue));

            return ComparaisonSymbol == '=' ? obj : !obj;
        }
    }
}
