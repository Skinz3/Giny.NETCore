using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ExternalNotification", "")]
    public class ExternalNotification : IDataObject , IIndexedData
    {        public const string MODULE = "ExternalNotifications";

        public int Id => (int)id;

        public int id;
        public int categoryId;
        public int iconId;
        public int colorId;
        public uint descriptionId;
        public bool defaultEnable;
        public bool defaultSound;
        public bool defaultNotify;
        public bool defaultMultiAccount;
        public string name;
        public uint messageId;

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
        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
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
        public int ColorId
        {
            get
            {
                return colorId;
            }
            set
            {
                colorId = value;
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
        public bool DefaultEnable
        {
            get
            {
                return defaultEnable;
            }
            set
            {
                defaultEnable = value;
            }
        }
        [D2OIgnore]
        public bool DefaultSound
        {
            get
            {
                return defaultSound;
            }
            set
            {
                defaultSound = value;
            }
        }
        [D2OIgnore]
        public bool DefaultNotify
        {
            get
            {
                return defaultNotify;
            }
            set
            {
                defaultNotify = value;
            }
        }
        [D2OIgnore]
        public bool DefaultMultiAccount
        {
            get
            {
                return defaultMultiAccount;
            }
            set
            {
                defaultMultiAccount = value;
            }
        }
        [D2OIgnore]
        public string Name
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

    }}
