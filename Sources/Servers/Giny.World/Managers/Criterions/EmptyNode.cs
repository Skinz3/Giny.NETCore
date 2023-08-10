using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions
{
    public class EmptyNode : Node
    {
        public override bool Eval(WorldClient client)
        {
            return true;
        }

        public override IEnumerable<Criterion> FindCriterionHandlers()
        {
            return new Criterion[0];
        }
    }
}
