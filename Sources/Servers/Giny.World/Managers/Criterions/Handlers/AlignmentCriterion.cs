using Giny.Core.DesignPattern;
using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("PP")]
    public class AlignmentCriterion : Criterion
    {
        public AlignmentCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        [WIP] // alignement
        public override bool Eval(WorldClient client)
        {
            return false;
        }
    }
}
