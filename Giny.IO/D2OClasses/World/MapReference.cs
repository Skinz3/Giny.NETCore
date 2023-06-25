using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MapReference", "")]
    public class MapReference : IDataObject , IIndexedData
    {        public const string MODULE = "MapReferences";

        public int Id => (int)id;

        public int id;
        public double mapId;
        public int cellId;

        [D2OIgnore]
        public int Id_
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
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
        [D2OIgnore]
        public int CellId
        {
            get
            {
                return cellId;
            }
            set
            {
                cellId = value;
            }
        }

    }}
