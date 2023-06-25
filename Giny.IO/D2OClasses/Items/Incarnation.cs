using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Incarnation", "")]
    public class Incarnation : IDataObject , IIndexedData
    {        public const string MODULE = "Incarnation";

        public int Id => (int)id;

        public uint id;
        public uint maleBoneId;
        public uint femaleBoneId;
        public string lookMale;
        public string lookFemale;

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
        public uint MaleBoneId
        {
            get
            {
                return maleBoneId;
            }
            set
            {
                maleBoneId = value;
            }
        }
        [D2OIgnore]
        public uint FemaleBoneId
        {
            get
            {
                return femaleBoneId;
            }
            set
            {
                femaleBoneId = value;
            }
        }
        [D2OIgnore]
        public string LookMale
        {
            get
            {
                return lookMale;
            }
            set
            {
                lookMale = value;
            }
        }
        [D2OIgnore]
        public string LookFemale
        {
            get
            {
                return lookFemale;
            }
            set
            {
                lookFemale = value;
            }
        }

    }}
