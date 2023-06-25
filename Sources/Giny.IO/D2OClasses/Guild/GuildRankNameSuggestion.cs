using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("GuildRankNameSuggestion", "")]
    public class GuildRankNameSuggestion : IDataObject , IIndexedData
    {        public const string MODULE = "GuildRankNameSuggestions";

        public int Id => throw new NotImplementedException();

        public string uiKey;

        [D2OIgnore]
        public string UiKey
        {
            get
            {
                return uiKey;
            }
            set
            {
                uiKey = value;
            }
        }

    }}
