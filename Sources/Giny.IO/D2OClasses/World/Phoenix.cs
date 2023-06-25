using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Phoenix", "")]
    public class Phoenix : IDataObject , IIndexedData
    {        public const string MODULE = "Phoenixes";

        public int Id => throw new NotImplementedException();

        public double mapId;

        [D2OIgnore]
        public double MapId
        {
            get
            {
                return mapId;
            }
            set
            {
                mapId = value;
            }
        }

    }}
