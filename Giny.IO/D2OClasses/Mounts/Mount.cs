using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Mount", "")]
    public class Mount : IDataObject , IIndexedData
    {        public const string MODULE = "Mounts";

        public int Id => (int)id;

        public uint id;
        public uint familyId;
        public uint nameId;
        public string look;
        public uint certificateId;
        public List<EffectInstance> effects;

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
        public uint FamilyId
        {
            get
            {
                return familyId;
            }
            set
            {
                familyId = value;
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
        public string Look
        {
            get
            {
                return look;
            }
            set
            {
                look = value;
            }
        }
        [D2OIgnore]
        public uint CertificateId
        {
            get
            {
                return certificateId;
            }
            set
            {
                certificateId = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstance> Effects
        {
            get
            {
                return effects;
            }
            set
            {
                effects = value;
            }
        }

    }}
