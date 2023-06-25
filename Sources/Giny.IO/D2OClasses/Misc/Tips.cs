using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Tips", "")]
    public class Tips : IDataObject , IIndexedData
    {        public const string MODULE = "Tips";

        public int Id => (int)id;

        public int id;
        public uint descId;

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
        public uint DescId
        {
            get
            {
                return descId;
            }
            set
            {
                descId = value;
            }
        }

    }}
