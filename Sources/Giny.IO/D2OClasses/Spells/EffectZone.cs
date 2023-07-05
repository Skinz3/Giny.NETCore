using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("EffectZone", "")]
    public class EffectZone : IDataObject, IIndexedData
    {

        public int Id => (int)id;

        public uint id;
        public bool isDefaultPreviewZoneHidden;
        public string casterMask;
        public string activationMask;

        public string rawDisplayZone;

        public string rawActivationZone;

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
        public bool IsDefaultPreviewZoneHidden
        {
            get
            {
                return isDefaultPreviewZoneHidden;
            }
            set
            {
                isDefaultPreviewZoneHidden = value;
            }
        }
        [D2OIgnore]
        public string CasterMask
        {
            get
            {
                return casterMask;
            }
            set
            {
                casterMask = value;
            }
        }
        [D2OIgnore]
        public string ActivationMask
        {
            get
            {
                return activationMask;
            }
            set
            {
                activationMask = value;
            }
        }

        [D2OIgnore]
        public string RawDisplayZone
        {
            get
            {
                return rawDisplayZone;
            }
            set
            {
                rawDisplayZone = value;
            }
        }

        [D2OIgnore]
        public string RawActivationZone
        {
            get
            {
                return rawActivationZone;
            }
            set
            {
                rawActivationZone = value;
            }
        }

    }
}

