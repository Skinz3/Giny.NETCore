using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Extensions
{
    public static class MathExtensions
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value > max)
            {
                return max;
            }
            if (value < min)
            {
                return min;
            }
            return value;
        }
    }
}
