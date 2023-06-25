using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SoundUi", "")]
    public class SoundUi : IDataObject , IIndexedData
    {        public const string MODULE = "SoundUi";

        public int Id => (int)id;

        public uint id;
        public string uiName;
        public string openFile;
        public string closeFile;
        public List<SoundUiElement> subElements;

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
        public string UiName
        {
            get
            {
                return uiName;
            }
            set
            {
                uiName = value;
            }
        }
        [D2OIgnore]
        public string OpenFile
        {
            get
            {
                return openFile;
            }
            set
            {
                openFile = value;
            }
        }
        [D2OIgnore]
        public string CloseFile
        {
            get
            {
                return closeFile;
            }
            set
            {
                closeFile = value;
            }
        }
        [D2OIgnore]
        public List<SoundUiElement> SubElements
        {
            get
            {
                return subElements;
            }
            set
            {
                subElements = value;
            }
        }

    }}
