using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public abstract class FormulaCharacteristic : DetailedCharacteristic
    {
        public delegate short ComputeTotalDelegate();

        protected ComputeTotalDelegate TotalFunction
        {
            get;
            set;
        }

        public FormulaCharacteristic()
        {

        }

        public abstract void Initialize(EntityStats stats);

        public override short Total()
        {
            return TotalFunction();
        }
    }
}
