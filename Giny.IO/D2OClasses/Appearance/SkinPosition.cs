using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SkinPosition", "")]
    public class SkinPosition : IDataObject , IIndexedData
    {        public const string MODULE = "SkinPositions";

        public int Id => (int)id;

        public uint id;
        public List<TransformData> transformation;
        public List<string> clip;
        public List<uint> skin;

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
        public List<TransformData> Transformation
        {
            get
            {
                return transformation;
            }
            set
            {
                transformation = value;
            }
        }
        [D2OIgnore]
        public List<string> Clip
        {
            get
            {
                return clip;
            }
            set
            {
                clip = value;
            }
        }
        [D2OIgnore]
        public List<uint> Skin
        {
            get
            {
                return skin;
            }
            set
            {
                skin = value;
            }
        }

    }}
