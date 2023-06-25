using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ServerTemporisSeason", "")]
    public class ServerTemporisSeason : IDataObject , IIndexedData
    {        public const string MODULE = "ServerTemporisSeasons";

        public int Id => throw new NotImplementedException();

        public int uid;
        public uint seasonNumber;
        public string information;
        public double beginning;
        public double closure;

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

    }}
