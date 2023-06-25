using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class BasicItemCollection : ItemCollection<CharacterItemRecord>
    {
        public BasicItemCollection()
        {
        }

        public BasicItemCollection(IEnumerable<CharacterItemRecord> items) : base(items)
        {
        }
    }
}
