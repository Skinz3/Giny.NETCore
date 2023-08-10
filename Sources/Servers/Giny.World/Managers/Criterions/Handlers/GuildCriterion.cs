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
    [Criterion("Pw")]
    public class HasGuildCriterion : Criterion
    {
        public HasGuildCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return client.Character.HasGuild;
        }
    }
    [Criterion("Py")]
    public class GuildLevelCriterion : Criterion
    {
        public GuildLevelCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

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
