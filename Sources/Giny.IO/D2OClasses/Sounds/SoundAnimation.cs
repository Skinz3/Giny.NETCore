using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SoundAnimation", "")]
    public class SoundAnimation : IDataObject , IIndexedData
    {        public const string MODULE = "SoundAnimations";

        public int Id => (int)id;

        public uint id;
        public string name;
        public string label;
        public string filename;
        public int volume;
        public int rolloff;
        public int automationDuration;
        public int automationVolume;
        public int automationFadeIn;
        public int automationFadeOut;
        public bool noCutSilence;
        public uint startFrame;

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
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }
        [D2OIgnore]
        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
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
        public int Rolloff
        {
            get
            {
                return rolloff;
            }
            set
            {
                rolloff = value;
            }
        }
        [D2OIgnore]
        public int AutomationDuration
        {
            get
            {
                return automationDuration;
            }
            set
            {
                automationDuration = value;
            }
        }
        [D2OIgnore]
        public int AutomationVolume
        {
            get
            {
                return automationVolume;
            }
            set
            {
                automationVolume = value;
            }
        }
        [D2OIgnore]
        public int AutomationFadeIn
        {
            get
            {
                return automationFadeIn;
            }
            set
            {
                automationFadeIn = value;
            }
        }
        [D2OIgnore]
        public int AutomationFadeOut
        {
            get
            {
                return automationFadeOut;
            }
            set
            {
                automationFadeOut = value;
            }
        }
        [D2OIgnore]
        public bool NoCutSilence
        {
            get
            {
                return noCutSilence;
            }
            set
            {
                noCutSilence = value;
            }
        }
        [D2OIgnore]
        public uint StartFrame
        {
            get
            {
                return startFrame;
            }
            set
            {
                startFrame = value;
            }
        }

    }}
