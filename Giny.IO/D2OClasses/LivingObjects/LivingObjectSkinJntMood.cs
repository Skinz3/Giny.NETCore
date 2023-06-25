using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("LivingObjectSkinJntMood", "")]
    public class LivingObjectSkinJntMood : IDataObject , IIndexedData
    {        public const string MODULE = "LivingObjectSkinJntMood";

        public int Id => throw new NotImplementedException();

        public int skinId;
        public List<List<int>> moods;

        [D2OIgnore]
        public int SkinId
        {
            get
            {
                return skinId;
            }
            set
            {
                skinId = value;
            }
        }
        [D2OIgnore]
        public List<List<int>> Moods
        {
            get
            {
                return moods;
            }
            set
            {
                moods = value;
            }
        }

    }}
