using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SoundBones", "")]
    public class SoundBones : IDataObject , IIndexedData
    {        public const string MODULE = "SoundBones";

        public int Id => (int)id;

        public uint id;
        public List<string> keys;
        public List<List<SoundAnimation>> values;

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
        public List<string> Keys
        {
            get
            {
                return keys;
            }
            set
            {
                keys = value;
            }
        }
        [D2OIgnore]
        public List<List<SoundAnimation>> Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }

    }}
