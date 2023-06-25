using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("NamingRule", "")]
    public class NamingRule : IDataObject , IIndexedData
    {        public const string MODULE = "NamingRules";

        public int Id => (int)id;

        public uint id;
        public uint minLength;
        public uint maxLength;
        public string regexp;

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
        public uint MinLength
        {
            get
            {
                return minLength;
            }
            set
            {
                minLength = value;
            }
        }
        [D2OIgnore]
        public uint MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                maxLength = value;
            }
        }
        [D2OIgnore]
        public string Regexp
        {
            get
            {
                return regexp;
            }
            set
            {
                regexp = value;
            }
        }

    }}
