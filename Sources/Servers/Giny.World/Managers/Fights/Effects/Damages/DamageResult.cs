using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    public class DamageResult
    {
        public int LifeLoss
        {
            get;
            private set;
        }
        public int ErodedLife
        {
            get;
            set;
        }
        public int ShieldLoss
        {
            get;
            set;
        }
        public int Total
        {
            get
            {
                return LifeLoss + ShieldLoss;
            }
        }
        public DamageResult(int lifeLoss, int erodedLife, int shieldLoss)
        {
            this.LifeLoss = lifeLoss;
            this.ErodedLife = erodedLife;
            this.ShieldLoss = shieldLoss;
        }

        public static DamageResult Zero()
        {
            return new DamageResult(0, 0, 0);
        }
    }
}
