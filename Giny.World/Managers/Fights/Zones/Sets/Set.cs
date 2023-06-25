using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones.Sets
{
    public abstract class Set
    {
        public abstract IEnumerable<MapPoint> EnumerateSet();
        public abstract bool BelongToSet(MapPoint point);

        public IEnumerable<MapPoint> EnumerateValidPoints()
        {
            return EnumerateSet().Where(x => x != null && x.IsInMap());
        }

        public Set IntersectWith(Set A)
        {
            return new Intersection(A, this);
        }
        public Set UnionWith(Set A)
        {
            return new Union(A, this);
        }

        public Set Substract(Set A)
        {
            return new Complement(this, A);
        }
    }
}
