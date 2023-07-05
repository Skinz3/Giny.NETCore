using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("ServerSeason", "")]
    public class ServerSeason : IDataObject, IIndexedData
    {
        public const string MODULE = "ServerSeasons";

        public int Id => throw new NotImplementedException();

        public int uid;
        public uint seasonNumber;
        public string information;
        public double beginning;
        public double closure;
        public double resetDate;
        public uint flagObjectId;

        [D2OIgnore]
        public int Uid
        {
            get
            {
                return uid;
            }
            set
            {
                uid = value;
            }
        }
        [D2OIgnore]
        public uint SeasonNumber
        {
            get
            {
                return seasonNumber;
            }
            set
            {
                seasonNumber = value;
            }
        }
        [D2OIgnore]
        public string Information
        {
            get
            {
                return information;
            }
            set
            {
                information = value;
            }
        }
        [D2OIgnore]
        public double Beginning
        {
            get
            {
                return beginning;
            }
            set
            {
                beginning = value;
            }
        }
        [D2OIgnore]
        public double Closure
        {
            get
            {
                return closure;
            }
            set
            {
                closure = value;
            }
        }
        [D2OIgnore]
        public double ResetDate
        {
            get
            {
                return resetDate;
            }
            set
            {
                resetDate = value;
            }
        }
        [D2OIgnore]
        public uint FlagObjectId
        {
            get
            {
                return flagObjectId;
            }
            set
            {
                flagObjectId = value;
            }
        }

    }
}

