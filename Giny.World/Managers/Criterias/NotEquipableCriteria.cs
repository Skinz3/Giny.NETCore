using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Protocol.Custom.Enums;
using Giny.World.Network;

namespace Giny.World.Managers.Criterias
{
    [Criteria("BI")]
    public class NotEquipableCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return client.Account.Role == ServerRoleEnum.Administrator;
        }
    }
}
