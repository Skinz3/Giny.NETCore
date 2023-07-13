using Giny.World.Managers.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public class InitiativeCharacteristic : FormulaCharacteristic
    {
        public InitiativeCharacteristic()
        {
        }

        public override void Initialize(EntityStats stats)
        {
            this.TotalFunction = () =>
            {
                return stats[Protocol.Custom.Enums.CharacteristicEnum.INTELLIGENCE].TotalInContext();
            };
        }
    }
}
