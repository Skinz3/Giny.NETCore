using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CensoredWord", "")]
    public class CensoredWord : IDataObject , IIndexedData
    {        public const string MODULE = "CensoredWords";

        public int Id => (int)id;

        public uint id;
        public uint listId;
        public string language;
        public string word;
        public bool deepLooking;

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
        public uint ListId
        {
            get
            {
                return listId;
            }
            set
            {
                listId = value;
            }
        }
        [D2OIgnore]
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }
        [D2OIgnore]
        public string Word
        {
            get
            {
                return word;
            }
            set
            {
                word = value;
            }
        }
        [D2OIgnore]
        public bool DeepLooking
        {
            get
            {
                return deepLooking;
            }
            set
            {
                deepLooking = value;
            }
        }

    }}
