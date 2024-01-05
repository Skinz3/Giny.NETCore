using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions
{
    public abstract class Node
    {
        public abstract bool Eval(WorldClient client);

        public abstract bool Eval(Fighter fighter);

        public abstract IEnumerable<Criterion> FindCriterionHandlers();

    }
}
