using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Job", "")]
    public class Job : IDataObject , IIndexedData
    {        public const string MODULE = "Jobs";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int iconId;
        public bool hasLegendaryCraft;

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
        public int IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public bool HasLegendaryCraft
        {
            get
            {
                return hasLegendaryCraft;
            }
            set
            {
                hasLegendaryCraft = value;
            }
        }

    }}
