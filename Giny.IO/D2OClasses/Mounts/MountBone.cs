using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MountBone", "")]
    public class MountBone : IDataObject , IIndexedData
    {        public const string MODULE = "MountBones";

        public int Id => (int)id;

        public uint id;

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

    }}
