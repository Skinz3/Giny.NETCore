using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SoundUiElement", "")]
    public class SoundUiElement : IDataObject , IIndexedData
    {        public const string MODULE = "SoundUiElement";

        public int Id => (int)id;

        public uint id;
        public string name;
        public uint hookId;
        public string file;
        public uint volume;

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
        public uint HookId
        {
            get
            {
                return hookId;
            }
            set
            {
                hookId = value;
            }
        }
        [D2OIgnore]
        public string File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }
        [D2OIgnore]
        public uint Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }

    }}
