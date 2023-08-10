using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Criterias;
using Giny.World.Network;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("BI")]
    public class NotEquipableCriterion : Criterion
    {
        public NotEquipableCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return client.Account.Role == ServerRoleEnum.Administrator;
        }
    }
}
