using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CensoredContent", "")]
    public class CensoredContent : IDataObject , IIndexedData
    {        public const string MODULE = "CensoredContents";

        public int Id => throw new NotImplementedException();

        public int type;
        public int oldValue;
        public int newValue;
        public string lang;

        [D2OIgnore]
        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        [D2OIgnore]
        public int OldValue
        {
            get
            {
                return oldValue;
            }
            set
            {
                oldValue = value;
            }
        }
        [D2OIgnore]
        public int NewValue
        {
            get
            {
                return newValue;
            }
            set
            {
                newValue = value;
            }
        }
        [D2OIgnore]
        public string Lang
        {
            get
            {
                return lang;
            }
            set
            {
                lang = value;
            }
        }

    }}
