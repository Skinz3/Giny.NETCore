using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AlignmentRankJntGift", "")]
    public class AlignmentRankJntGift : IDataObject , IIndexedData
    {        public const string MODULE = "AlignmentRankJntGift";

        public int Id => (int)id;

        public int id;
        public List<int> gifts;
        public List<int> levels;

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
        public List<int> Gifts
        {
            get
            {
                return gifts;
            }
            set
            {
                gifts = value;
            }
        }
        [D2OIgnore]
        public List<int> Levels
        {
            get
            {
                return levels;
            }
            set
            {
                levels = value;
            }
        }

    }}
