using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MapCoordinates", "")]
    public class MapCoordinates : IDataObject , IIndexedData
    {        public const string MODULE = "MapCoordinates";

        public int Id => throw new NotImplementedException();

        public uint compressedCoords;
        public List<double> mapIds;

        [D2OIgnore]
        public uint CompressedCoords
        {
            get
            {
                return compressedCoords;
            }
            set
            {
                compressedCoords = value;
            }
        }
        [D2OIgnore]
        public List<double> MapIds
        {
            get
            {
                return mapIds;
            }
            set
            {
                mapIds = value;
            }
        }

    }}
