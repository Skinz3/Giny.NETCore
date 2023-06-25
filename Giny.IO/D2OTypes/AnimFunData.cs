using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2OTypes
{
    /// <summary>
    /// Manually dumped.
    /// </summary>
    [D2OClass("AnimFunData", "com.ankamagames.Giny.types.data")]
    public class AnimFunData : IDataObject
    {
        public int animId;
        public int entityId;
        public string animName;
        public int animWeight;
    }
}
