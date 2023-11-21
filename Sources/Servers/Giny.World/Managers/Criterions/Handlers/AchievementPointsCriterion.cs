using Giny.World.Managers.Criterias;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("Oa")]
    public class AchievementPointsCriterion : Criterion
    {
        public AchievementPointsCriterion(string criteriaFull) : base(criteriaFull)
        {
        }

        public override short MaxValue => (short)(int.Parse(Value) + 1);

        public override bool Eval(WorldClient client)
        {
            var required = short.Parse(Value);

            return ArithmeticEval(client.Character.AchievementPoints.Value);
            
        }
        public override short GetCurrentValue(WorldClient client)
        {
            return (short)client.Character.AchievementPoints;
        }
    }
}
