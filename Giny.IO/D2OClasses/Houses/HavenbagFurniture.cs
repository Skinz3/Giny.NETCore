using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("HavenbagFurniture", "")]
    public class HavenbagFurniture : IDataObject , IIndexedData
    {        public const string MODULE = "HavenbagFurnitures";

        public int Id => throw new NotImplementedException();

        public int typeId;
        public int themeId;
        public int elementId;
        public int color;
        public int skillId;
        public int layerId;
        public bool blocksMovement;
        public bool isStackable;
        public uint cellsWidth;
        public uint cellsHeight;
        public uint order;

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
        public int ThemeId
        {
            get
            {
                return themeId;
            }
            set
            {
                themeId = value;
            }
        }
        [D2OIgnore]
        public int ElementId
        {
            get
            {
                return elementId;
            }
            set
            {
                elementId = value;
            }
        }
        [D2OIgnore]
        public int Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
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
        public int LayerId
        {
            get
            {
                return layerId;
            }
            set
            {
                layerId = value;
            }
        }
        [D2OIgnore]
        public bool BlocksMovement
        {
            get
            {
                return blocksMovement;
            }
            set
            {
                blocksMovement = value;
            }
        }
        [D2OIgnore]
        public bool IsStackable
        {
            get
            {
                return isStackable;
            }
            set
            {
                isStackable = value;
            }
        }
        [D2OIgnore]
        public uint CellsWidth
        {
            get
            {
                return cellsWidth;
            }
            set
            {
                cellsWidth = value;
            }
        }
        [D2OIgnore]
        public uint CellsHeight
        {
            get
            {
                return cellsHeight;
            }
            set
            {
                cellsHeight = value;
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

    }}
