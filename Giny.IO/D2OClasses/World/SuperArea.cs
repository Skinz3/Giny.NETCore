using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SuperArea", "")]
    public class SuperArea : IDataObject , IIndexedData
    {        public const string MODULE = "SuperAreas";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint worldmapId;
        public bool hasWorldMap;

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
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public uint WorldmapId
        {
            get
            {
                return worldmapId;
            }
            set
            {
                worldmapId = value;
            }
        }
        [D2OIgnore]
        public bool HasWorldMap
        {
            get
            {
                return hasWorldMap;
            }
            set
            {
                hasWorldMap = value;
            }
        }

    }}
