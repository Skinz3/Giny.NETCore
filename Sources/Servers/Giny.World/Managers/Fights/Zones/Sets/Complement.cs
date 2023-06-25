using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones.Sets
{
    public class Complement : Set
    {
        public Set A
        {
            get;
            set;
        }

        public Set Container
        {
            get;
            set;
        }

        public Complement(Set A, Set Container)
        {
            this.A = A;
            this.Container = Container;
        }

        public override IEnumerable<MapPoint> EnumerateSet()
        {
            return Container.EnumerateSet().Except(A.EnumerateSet());
        }

        public override bool BelongToSet(MapPoint point)
        {
            return !A.BelongToSet(point) && Container.BelongToSet(point);
        }
    }
}
