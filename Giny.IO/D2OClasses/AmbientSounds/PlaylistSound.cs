using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("PlaylistSound", "")]
    public class PlaylistSound : IDataObject , IIndexedData
    {        public const string MODULE = "PlaylistSounds";

        public int Id => throw new NotImplementedException();

        public string id;
        public int volume;
        public int channel;
        public int soundOrder;

        [D2OIgnore]
        public string Id_
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
        public int Volume
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
        [D2OIgnore]
        public int Channel
        {
            get
            {
                return channel;
            }
            set
            {
                channel = value;
            }
        }
        [D2OIgnore]
        public int SoundOrder
        {
            get
            {
                return soundOrder;
            }
            set
            {
                soundOrder = value;
            }
        }

    }}
