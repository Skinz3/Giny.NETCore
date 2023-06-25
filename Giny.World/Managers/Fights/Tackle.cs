using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights
{
    public class Tackle
    {
        public short ApLoss
        {
            get;
            private set;
        }
        public short MpLoss
        {
            get;
            private set;
        }
        public Tackle(short apLoss, short mpLoss)
        {
            this.ApLoss = apLoss;
            this.MpLoss = mpLoss;
        }
        public bool Consistent()
        {
            return ApLoss > 0 || MpLoss > 0;
        }
    }
}
