using Giny.Core.DesignPattern;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast.Units
{
    public class Healing : ITriggerToken
    {
        public short Delta
        {
            get;
            set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }

        public Healing(Fighter source, Fighter target, short delta)
        {
            this.Source = source;
            this.Target = target;
            this.Delta = delta;
        }


        public Fighter GetSource()
        {
            return Source;
        }
    }
}
