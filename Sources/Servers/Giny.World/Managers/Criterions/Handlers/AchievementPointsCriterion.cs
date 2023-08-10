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

        public override short MaxValue => (short)(int.Parse(CriteriaValue) + 1);

        public override bool Eval(WorldClient client)
        {
            var required = short.Parse(CriteriaValue);

            switch (ComparaisonSymbol)
            {
                case SuperiorSymbol:
                    return client.Character.AchievementPoints > required;

                case InferiorSymbol:
                    return client.Character.AchievementPoints < required;

                case EqualSymbol:
                    return client.Character.AchievementPoints == required;
            }

            return false;
        }
        public override short GetCurrentValue(WorldClient client)
        {
            return (short)client.Character.AchievementPoints;
        }
    }
}
