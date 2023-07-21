using Giny.Core;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones.Sets;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast.Units
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
            Min = min;
            Max = max;
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

        public void ComputeShapeEfficiencyModifiers(Fighter target, SpellEffectHandler effectHandler)
        {
            double efficiency = effectHandler.Zone.GetShapeEfficiency(target.Cell, effectHandler.CastHandler.Cast.TargetCell);
            Min = Min * efficiency;
            Max = Max * efficiency;
        }
        public void ApplyMultiplicator(int value)
        {
            Min = Min * (value / 100d);
            Max = Max * (value / 100d);
        }
        public int Generate(Random random, bool hasRandDownModifier, bool hasRandUpModifier)
        {
            if (Min == Max)
            {
                return (int)Max;
            }
            else
            {
                if (hasRandDownModifier)
                {
                    return (int)Min;
                }
                if (hasRandUpModifier)
                {
                    return (int)Max;
                }


                return (int)random.Next((int)Min, (int)Max + 1);
            }
        }
    }
}
