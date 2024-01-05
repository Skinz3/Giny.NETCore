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
    [CriterionHandler("Pk")]
    public class ItemSetCriterion : Criterion
    {
        public ItemSetCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            if (client.Character.Inventory.MaximumItemSetCount() < int.Parse(Value))
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
