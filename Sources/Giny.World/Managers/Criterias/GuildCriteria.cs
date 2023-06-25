using Giny.Core.DesignPattern;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("Pw")]
    public class HasGuildCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return client.Character.HasGuild;
        }
    }
    [Criteria("Py")]
    public class GuildLevelCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            if (!client.Character.HasGuild)
            {
                return false;
            }
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Guild.Level);
        }
    }
}
