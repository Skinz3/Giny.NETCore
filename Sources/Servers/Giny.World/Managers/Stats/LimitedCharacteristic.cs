using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public abstract class LimitCharacteristic : Characteristic
    {
        //ignore
        public abstract short Limit
        {
            get;
        }

        //ignore
        public abstract bool ContextLimit
        {
            get;
        }

        public override short Total()
        {
            short total = base.Total();
            return total > Limit ? Limit : total;
        }
        public override short TotalInContext()
        {
            if (ContextLimit)
            {
                short total = base.TotalInContext();
                return total > Limit ? Limit : total;
            }
            else
            {
                return base.TotalInContext();
            }
        }
        public override string ToString()
        {
            return "TotalContext: " + TotalInContext() + " Total:" + Total();
        }
    }
}
