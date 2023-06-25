using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("PopupButton", "")]
    public class PopupButton : IDataObject , IIndexedData
    {
        public int Id => (int)id;

        public int id;
        public uint popupId;
        public uint type;
        public uint textId;
        public uint actionType;
        public string actionValue;

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
        public uint PopupId
        {
            get
            {
                return popupId;
            }
            set
            {
                popupId = value;
            }
        }
        [D2OIgnore]
        public uint Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        [D2OIgnore]
        public uint TextId
        {
            get
            {
                return textId;
            }
            set
            {
                textId = value;
            }
        }
        [D2OIgnore]
        public uint ActionType
        {
            get
            {
                return actionType;
            }
            set
            {
                actionType = value;
            }
        }
        [D2OIgnore]
        public string ActionValue
        {
            get
            {
                return actionValue;
            }
            set
            {
                actionValue = value;
            }
        }

    }}
