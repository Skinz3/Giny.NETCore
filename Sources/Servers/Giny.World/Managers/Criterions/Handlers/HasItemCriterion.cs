using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("PO")]
    public class HasItemCriterion : Criterion
    {
        public HasItemCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            var criteria = Text.Remove(0, 3).Split(',');
            int quantity = 1;
            short gid = short.Parse(criteria[0]);

            if (criteria.Length > 1)
            {
                quantity = int.Parse(criteria[1]);
            }

            if (Operator == CriterionComparaisonOperator.Equal)
            {
                return client.Character.Inventory.Exist(gid, quantity);
            }
            else if (Operator == CriterionComparaisonOperator.Negation)
            {
                return !client.Character.Inventory.Exist(gid, quantity);
            }
            else if (Operator == CriterionComparaisonOperator.X)
            {
                foreach (var item in client.Character.Inventory.GetEquipedItems())
                {
                    if (item.GId == gid)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                throw new Exception("Invalid comparaison symbol. (HasItemCriteria)");
            }

        }
    }

}
