using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Interactive", "")]
    public class Interactive : IDataObject , IIndexedData
    {        public const string MODULE = "Interactives";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int actionId;
        public bool displayTooltip;

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
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public int ActionId
        {
            get
            {
                return actionId;
            }
            set
            {
                actionId = value;
            }
        }
        [D2OIgnore]
        public bool DisplayTooltip
        {
            get
            {
                return displayTooltip;
            }
            set
            {
                displayTooltip = value;
            }
        }

    }}
