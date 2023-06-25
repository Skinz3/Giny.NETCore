using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Playlist", "")]
    public class Playlist : IDataObject , IIndexedData
    {        public const string MODULE = "Playlists";

        public int Id => (int)id;

        public int id;
        public int type;
        public List<PlaylistSound> sounds;
        public bool startRandom;
        public bool startRandomOnce;
        public int crossfadeDuration;
        public bool random;

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
        public int Type
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
        public List<PlaylistSound> Sounds
        {
            get
            {
                return sounds;
            }
            set
            {
                sounds = value;
            }
        }
        [D2OIgnore]
        public bool StartRandom
        {
            get
            {
                return startRandom;
            }
            set
            {
                startRandom = value;
            }
        }
        [D2OIgnore]
        public bool StartRandomOnce
        {
            get
            {
                return startRandomOnce;
            }
            set
            {
                startRandomOnce = value;
            }
        }
        [D2OIgnore]
        public int CrossfadeDuration
        {
            get
            {
                return crossfadeDuration;
            }
            set
            {
                crossfadeDuration = value;
            }
        }
        [D2OIgnore]
        public bool Random
        {
            get
            {
                return random;
            }
            set
            {
                random = value;
            }
        }

    }}
