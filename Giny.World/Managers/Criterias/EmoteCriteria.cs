using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("PE")]
    public class EmoteCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return !client.Character.Record.KnownEmotes.Contains((byte)Int32.Parse(CriteriaValue));
        }
    }
}
