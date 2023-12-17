using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("Collectable", "")]
    public class Collectable : IDataObject, IIndexedData
    {
        public const string MODULE = "Collectables";

        public int Id => throw new NotImplementedException();

        public int entityId;
        public int name;
        public int typeId;
        public int gfxId;
        public int order;
        public int rarity;

        [D2OIgnore]
        public int EntityId
        {
            get
            {
                return entityId;
            }
            set
            {
                entityId = value;
            }
        }
        [D2OIgnore]
        public int Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [D2OIgnore]
        public int TypeId
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
        public int GfxId
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
        [D2OIgnore]
        public int Rarity
        {
            get
            {
                return rarity;
            }
            set
            {
                rarity = value;
            }
        }

    }
}

