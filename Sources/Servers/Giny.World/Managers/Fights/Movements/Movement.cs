using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Movements
{
    public class Movement : ITriggerToken
    {
        public MovementType Type
        {
            get;
            private set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public short MpCost
        {
            get;
            private set;
        }
        public Movement(MovementType type, Fighter source, short mpCost)
        {
            this.Type = type;
            this.Source = source;
            this.MpCost = mpCost;
        }

        public Fighter GetSource()
        {
            return Source;
        }
    }
}
