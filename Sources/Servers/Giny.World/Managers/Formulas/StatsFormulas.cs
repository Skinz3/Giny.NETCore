using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Formulas
{
    public class StatsFormulas : Singleton<StatsFormulas>
    {
        public int TotalInitiative(EntityStats stats)
        {
            double num1 = stats.Total() + stats[CharacteristicEnum.INITIATIVE].Total();
            double num2 = stats.LifePoints / (double)stats.MaxLifePoints;
            double value = num1 * num2;
            return value > 0 ? (int)value : 0;
        }

        public int TotalWeight(Character character)
        {
            return 1000 + (character.Record.Stats.Strength.Total() * 5) + character.Record.Stats[CharacteristicEnum.WEIGHT].Total();
        }
    }
}
