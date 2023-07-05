using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("GuildRight", "")]
    public class GuildRight : SocialRight, IIndexedData
    {
        public const string MODULE = "GuildRights";

        public int Id => throw new NotImplementedException();



    }
}

