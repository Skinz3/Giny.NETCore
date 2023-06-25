using Giny.Core;
using Giny.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public class Jet
    {
        public double Min
        {
            get;
            set;
        }
        public double Max
        {
            get;
            set;
        }

        public Jet(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        public void ValidateBounds()
        {
            if (Min < 0)
            {
                Min = 0;
            }
            if (Max < 0)
            {
                Max = 0;
            }

            if (Min > Max)
            {
                Logger.Write("Unable to compute jet Min :" + Min + " Max :" + Max, Channels.Critical);

                Max = 1;
                Min = 1;

            }
        }
        public short Generate(bool hasRandDownModifier, bool hasRandUpModifier)
        {
            if (Min == Max)
            {
                return (short)Max;
            }
            else
            {
                if (hasRandDownModifier)
                {
                    return (short)Min;
                }
                if (hasRandUpModifier)
                {
                    return (short)Max;
                }

                return (short)new AsyncRandom().Next((short)Min, (short)Max + 1);
            }
        }
    }
}
