using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MapScrollAction", "")]
    public class MapScrollAction : IDataObject , IIndexedData
    {        public const string MODULE = "MapScrollActions";

        public int Id => throw new NotImplementedException();

        public double id;
        public bool rightExists;
        public bool bottomExists;
        public bool leftExists;
        public bool topExists;
        public double rightMapId;
        public double bottomMapId;
        public double leftMapId;
        public double topMapId;

        [D2OIgnore]
        public double Id_
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
        public bool RightExists
        {
            get
            {
                return rightExists;
            }
            set
            {
                rightExists = value;
            }
        }
        [D2OIgnore]
        public bool BottomExists
        {
            get
            {
                return bottomExists;
            }
            set
            {
                bottomExists = value;
            }
        }
        [D2OIgnore]
        public bool LeftExists
        {
            get
            {
                return leftExists;
            }
            set
            {
                leftExists = value;
            }
        }
        [D2OIgnore]
        public bool TopExists
        {
            get
            {
                return topExists;
            }
            set
            {
                topExists = value;
            }
        }
        [D2OIgnore]
        public double RightMapId
        {
            get
            {
                return rightMapId;
            }
            set
            {
                rightMapId = value;
            }
        }
        [D2OIgnore]
        public double BottomMapId
        {
            get
            {
                return bottomMapId;
            }
            set
            {
                bottomMapId = value;
            }
        }
        [D2OIgnore]
        public double LeftMapId
        {
            get
            {
                return leftMapId;
            }
            set
            {
                leftMapId = value;
            }
        }
        [D2OIgnore]
        public double TopMapId
        {
            get
            {
                return topMapId;
            }
            set
            {
                topMapId = value;
            }
        }

    }}
