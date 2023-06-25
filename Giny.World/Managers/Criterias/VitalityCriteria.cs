using Giny.Protocol.Custom.Enums;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    [Criteria("cv")]
    public class VitalityCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats[CharacteristicEnum.VITALITY].Additional);
        }
    }
    [Criteria("CV")]
    public class TotalVitalityCriteria : Criteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats[CharacteristicEnum.VITALITY].Total());
        }
    }
}
