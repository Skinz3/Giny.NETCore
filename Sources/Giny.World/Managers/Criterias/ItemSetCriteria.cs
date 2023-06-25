using Giny.Core.DesignPattern;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("Pk")]
    public class ItemSetCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            if (client.Character.Inventory.MaximumItemSetCount() < int.Parse(CriteriaValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
