using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("GuildTagsType", "")]
    public class GuildTagsType : SocialTagsType, IIndexedData
    {
        public const string MODULE = "GuildTagsTypes";

        public int Id => throw new NotImplementedException();



    }
}

