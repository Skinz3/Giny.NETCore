using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Sign", "")]
    public class Sign : IDataObject , IIndexedData
    {        public const string MODULE = "Signs";

        public int Id => (int)id;

        public int id;
        public string @params;
        public int skillId;
        public uint textKey;

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
        public string Params
        {
            get
            {
                return @params;
            }
            set
            {
                @params = value;
            }
        }
        [D2OIgnore]
        public int SkillId
        {
            get
            {
                return skillId;
            }
            set
            {
                skillId = value;
            }
        }
        [D2OIgnore]
        public uint TextKey
        {
            get
            {
                return textKey;
            }
            set
            {
                textKey = value;
            }
        }

    }}
