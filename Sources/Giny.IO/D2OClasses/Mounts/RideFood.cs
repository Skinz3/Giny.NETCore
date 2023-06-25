using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("RideFood", "")]
    public class RideFood : IDataObject , IIndexedData
    {        public const string MODULE = "RideFood";

        public int Id => throw new NotImplementedException();

        public uint gid;
        public uint typeId;
        public uint familyId;

        [D2OIgnore]
        public uint Gid
        {
            get
            {
                return gid;
            }
            set
            {
                gid = value;
            }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
            }
        }
        [D2OIgnore]
        public uint FamilyId
        {
            get
            {
                return familyId;
            }
            set
            {
                familyId = value;
            }
        }

    }}
