using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Triggers
{
    public class Death : ITriggerToken
    {
        private Fighter Source
        {
            get;
            set;
        }
        private Fighter Target
        {
            get;
            set;
        }
        public Death(Fighter source, Fighter target)
        {
            this.Source = source;
            this.Target = target;   
        }


        public Fighter GetSource()
        {
            return Source;
        }
    }
}
