using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CreatureBoneOverride", "")]
    public class CreatureBoneOverride : IDataObject , IIndexedData
    {        public const string MODULE = "CreatureBonesOverrides";

        public int Id => throw new NotImplementedException();

        public int boneId;
        public int creatureBoneId;

        [D2OIgnore]
        public int BoneId
        {
            get
            {
                return boneId;
            }
            set
            {
                boneId = value;
            }
        }
        [D2OIgnore]
        public int CreatureBoneId
        {
            get
            {
                return creatureBoneId;
            }
            set
            {
                creatureBoneId = value;
            }
        }

    }}
