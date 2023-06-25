using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("PointOfInterestCategory", "")]
    public class PointOfInterestCategory : IDataObject , IIndexedData
    {        public const string MODULE = "PointOfInterestCategory";

        public int Id => (int)id;

        public uint id;
        public uint actionLabelId;

        [D2OIgnore]
        public uint Id_
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
        public uint ActionLabelId
        {
            get
            {
                return actionLabelId;
            }
            set
            {
                actionLabelId = value;
            }
        }

    }}
