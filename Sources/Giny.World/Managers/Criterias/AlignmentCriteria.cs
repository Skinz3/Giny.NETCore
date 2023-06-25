using Giny.Core.DesignPattern;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("PP")]
    public class AlignmentCriteria : Criteria
    {
        [WIP] // alignement
        public override bool Eval(WorldClient client)
        {
            return false;
        }
    }
}
