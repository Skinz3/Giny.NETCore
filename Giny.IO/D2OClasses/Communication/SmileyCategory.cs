using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SmileyCategory", "")]
    public class SmileyCategory : IDataObject , IIndexedData
    {        public const string MODULE = "SmileyCategories";

        public int Id => (int)id;

        public int id;
        public uint order;
        public string gfxId;
        public bool isFake;

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
        public uint Order
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
        [D2OIgnore]
        public string GfxId
        {
            get
            {
                return gfxId;
            }
            set
            {
                gfxId = value;
            }
        }
        [D2OIgnore]
        public bool IsFake
        {
            get
            {
                return isFake;
            }
            set
            {
                isFake = value;
            }
        }

    }}
