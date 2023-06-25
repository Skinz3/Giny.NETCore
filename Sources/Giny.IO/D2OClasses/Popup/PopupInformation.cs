using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("PopupInformation", "")]
    public class PopupInformation : IDataObject , IIndexedData
    {        public const string MODULE = "PopupInformations";

        public int Id => (int)id;

        public int id;
        public uint parentId;
        public uint titleId;
        public uint descriptionId;
        public string illuName;
        public List<PopupButton> buttons;
        public string criterion;
        public uint cacheType;
        public bool autoTrigger;

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
        public uint ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                parentId = value;
            }
        }
        [D2OIgnore]
        public uint TitleId
        {
            get
            {
                return titleId;
            }
            set
            {
                titleId = value;
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
        public string IlluName
        {
            get
            {
                return illuName;
            }
            set
            {
                illuName = value;
            }
        }
        [D2OIgnore]
        public List<PopupButton> Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;
            }
        }
        [D2OIgnore]
        public string Criterion
        {
            get
            {
                return criterion;
            }
            set
            {
                criterion = value;
            }
        }
        [D2OIgnore]
        public uint CacheType
        {
            get
            {
                return cacheType;
            }
            set
            {
                cacheType = value;
            }
        }
        [D2OIgnore]
        public bool AutoTrigger
        {
            get
            {
                return autoTrigger;
            }
            set
            {
                autoTrigger = value;
            }
        }

    }}
