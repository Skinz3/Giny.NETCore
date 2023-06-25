using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ArenaLeagueReward", "")]
    public class ArenaLeagueReward : IDataObject , IIndexedData
    {        public const string MODULE = "ArenaLeagueRewards";

        public int Id => (int)id;

        public int id;
        public uint seasonId;
        public uint leagueId;
        public List<uint> titlesRewards;
        public bool endSeasonRewards;

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
        public uint SeasonId
        {
            get
            {
                return seasonId;
            }
            set
            {
                seasonId = value;
            }
        }
        [D2OIgnore]
        public uint LeagueId
        {
            get
            {
                return leagueId;
            }
            set
            {
                leagueId = value;
            }
        }
        [D2OIgnore]
        public List<uint> TitlesRewards
        {
            get
            {
                return titlesRewards;
            }
            set
            {
                titlesRewards = value;
            }
        }
        [D2OIgnore]
        public bool EndSeasonRewards
        {
            get
            {
                return endSeasonRewards;
            }
            set
            {
                endSeasonRewards = value;
            }
        }

    }}
