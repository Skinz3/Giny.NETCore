using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Notification", "")]
    public class Notification : IDataObject , IIndexedData
    {        public const string MODULE = "Notifications";

        public int Id => (int)id;

        public int id;
        public uint titleId;
        public uint messageId;
        public int iconId;
        public int typeId;
        public string trigger;

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
        public uint MessageId
        {
            get
            {
                return messageId;
            }
            set
            {
                messageId = value;
            }
        }
        [D2OIgnore]
        public int IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
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
        public string Trigger
        {
            get
            {
                return trigger;
            }
            set
            {
                trigger = value;
            }
        }

    }}
