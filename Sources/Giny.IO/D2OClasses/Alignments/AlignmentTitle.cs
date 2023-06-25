using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AlignmentTitle", "")]
    public class AlignmentTitle : IDataObject , IIndexedData
    {        public const string MODULE = "AlignmentTitles";

        public int Id => throw new NotImplementedException();

        public int sideId;
        public List<int> namesId;
        public List<int> shortsId;

        [D2OIgnore]
        public int SideId
        {
            get
            {
                return sideId;
            }
            set
            {
                sideId = value;
            }
        }
        [D2OIgnore]
        public List<int> NamesId
        {
            get
            {
                return namesId;
            }
            set
            {
                namesId = value;
            }
        }
        [D2OIgnore]
        public List<int> ShortsId
        {
            get
            {
                return shortsId;
            }
            set
            {
                shortsId = value;
            }
        }

    }}
