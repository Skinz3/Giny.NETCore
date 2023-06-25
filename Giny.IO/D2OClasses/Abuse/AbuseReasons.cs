using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AbuseReasons", "")]
    public class AbuseReasons : IDataObject , IIndexedData
    {        public const string MODULE = "AbuseReasons";

        public int Id => throw new NotImplementedException();

        public uint abuseReasonId;
        public uint mask;
        public int reasonTextId;

        [D2OIgnore]
        public uint AbuseReasonId
        {
            get
            {
                return abuseReasonId;
            }
            set
            {
                abuseReasonId = value;
            }
        }
        [D2OIgnore]
        public uint Mask
        {
            get
            {
                return mask;
            }
            set
            {
                mask = value;
            }
        }
        [D2OIgnore]
        public int ReasonTextId
        {
            get
            {
                return reasonTextId;
            }
            set
            {
                reasonTextId = value;
            }
        }

    }}
