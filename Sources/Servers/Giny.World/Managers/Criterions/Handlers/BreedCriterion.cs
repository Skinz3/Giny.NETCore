using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("PG")]
    public class BreedCriterion : Criterion
    {
        public BreedCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return client.Character.Record.BreedId == sbyte.Parse(CriteriaValue);

        }
    }
}
