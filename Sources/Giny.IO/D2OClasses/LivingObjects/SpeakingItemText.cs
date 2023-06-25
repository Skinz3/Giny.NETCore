using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpeakingItemText", "")]
    public class SpeakingItemText : IDataObject , IIndexedData
    {        public const string MODULE = "SpeakingItemsText";

        public int Id => throw new NotImplementedException();

        public int textId;
        public double textProba;
        public uint textStringId;
        public int textLevel;
        public int textSound;
        public string textRestriction;

        [D2OIgnore]
        public int TextId
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
        public double TextProba
        {
            get
            {
                return textProba;
            }
            set
            {
                textProba = value;
            }
        }
        [D2OIgnore]
        public uint TextStringId
        {
            get
            {
                return textStringId;
            }
            set
            {
                textStringId = value;
            }
        }
        [D2OIgnore]
        public int TextLevel
        {
            get
            {
                return textLevel;
            }
            set
            {
                textLevel = value;
            }
        }
        [D2OIgnore]
        public int TextSound
        {
            get
            {
                return textSound;
            }
            set
            {
                textSound = value;
            }
        }
        [D2OIgnore]
        public string TextRestriction
        {
            get
            {
                return textRestriction;
            }
            set
            {
                textRestriction = value;
            }
        }

    }}
