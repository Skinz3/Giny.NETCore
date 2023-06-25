using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("NpcAction", "")]
    public class NpcAction : IDataObject , IIndexedData
    {        public const string MODULE = "NpcActions";

        public int Id => (int)id;

        public int id;
        public int realId;
        public uint nameId;

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
        public int RealId
        {
            get
            {
                return realId;
            }
            set
            {
                realId = value;
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

    }}
