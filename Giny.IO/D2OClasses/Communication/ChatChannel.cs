using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ChatChannel", "")]
    public class ChatChannel : IDataObject , IIndexedData
    {        public const string MODULE = "ChatChannels";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint descriptionId;
        public string shortcut;
        public string shortcutKey;
        public bool isPrivate;
        public bool allowObjects;

        [D2OIgnore]
        public uint Id_
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
        public string Shortcut
        {
            get
            {
                return shortcut;
            }
            set
            {
                shortcut = value;
            }
        }
        [D2OIgnore]
        public string ShortcutKey
        {
            get
            {
                return shortcutKey;
            }
            set
            {
                shortcutKey = value;
            }
        }
        [D2OIgnore]
        public bool IsPrivate
        {
            get
            {
                return isPrivate;
            }
            set
            {
                isPrivate = value;
            }
        }
        [D2OIgnore]
        public bool AllowObjects
        {
            get
            {
                return allowObjects;
            }
            set
            {
                allowObjects = value;
            }
        }

    }}
