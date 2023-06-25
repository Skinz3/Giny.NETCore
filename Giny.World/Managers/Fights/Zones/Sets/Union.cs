using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones.Sets
{
    public class Union : Set
    {
        public Union(Set A, Set B)
        {
            this.A = A;
            this.B = B;
        }
        public Set A
        {
            get;
            private set;
        }

        public Set B
        {
            get;
            private set;
        }

        public override IEnumerable<MapPoint> EnumerateSet()
        {
            return A.EnumerateSet().Union(B.EnumerateSet());
        }

        public override bool BelongToSet(MapPoint point)
        {
            return A.BelongToSet(point) || B.BelongToSet(point);
        }
    }
}
