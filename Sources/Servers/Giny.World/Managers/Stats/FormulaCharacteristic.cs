using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public class FormulaCharacteristic : DetailedCharacteristic
    {
        public delegate short ComputeTotalDelegate(FormulaCharacteristic characteristic);

        private ComputeTotalDelegate TotalFunction
        {
            get;
            set;
        }

        public FormulaCharacteristic(ComputeTotalDelegate totalFunction)
        {
            TotalFunction = totalFunction;
        }

        public override Characteristic Clone()
        {
            return new DetailedCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
        public override short Total()
        {
            return TotalFunction(this);
        }
        public static new FormulaCharacteristic Zero(ComputeTotalDelegate totalFunction)
        {
            return New(totalFunction, 0);
        }
        public static new FormulaCharacteristic New(ComputeTotalDelegate totalFunction, short @base)
        {
            return new FormulaCharacteristic(totalFunction)
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0,
            };
        }

    }
}
