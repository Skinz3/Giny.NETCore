using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreedRoleByBreed", "")]
    public class BreedRoleByBreed : IDataObject , IIndexedData
    {        public const string MODULE = "BreedRoleByBreeds";

        public int Id => throw new NotImplementedException();

        public int breedId;
        public int roleId;
        public uint descriptionId;
        public int value;
        public int order;

        [D2OIgnore]
        public int BreedId
        {
            get
            {
                return breedId;
            }
            set
            {
                breedId = value;
            }
        }
        [D2OIgnore]
        public int RoleId
        {
            get
            {
                return roleId;
            }
            set
            {
                roleId = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                value = value;
            }
        }
        [D2OIgnore]
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

    }}
