using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [CriterionHandler("cv")]
    public class VitalityCriterion : Criterion
    {
        public VitalityCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.VITALITY).Additional);
        }
    }
    [CriterionHandler("CV")]
    public class TotalVitalityCriterion : Criterion
    {
        public TotalVitalityCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override bool Eval(WorldClient client)
        {
            return ArithmeticEval(client.Character.Record.Stats.GetCharacteristic<DetailedCharacteristic>(CharacteristicEnum.VITALITY).Total());
        }
    }
}
