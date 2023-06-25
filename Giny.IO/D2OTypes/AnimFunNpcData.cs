using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2OTypes
{
    // <summary>
    /// Manually dumped.
    /// </summary>
    [D2OClass("AnimFunNpcData", "com.ankamagames.Giny.datacenter.npcs")]
    public class AnimFunNpcData : AnimFunData
    {
        public List<AnimFunNpcData> subAnimFunData;
    }
}
