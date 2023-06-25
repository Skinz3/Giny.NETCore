using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Breed", "")]
    public class Breed : IDataObject , IIndexedData
    {        public const string MODULE = "Breeds";

        public int Id => (int)id;

        public int id;
        public uint shortNameId;
        public uint longNameId;
        public uint descriptionId;
        public uint gameplayDescriptionId;
        public uint gameplayClassDescriptionId;
        public string maleLook;
        public string femaleLook;
        public uint creatureBonesId;
        public int maleArtwork;
        public int femaleArtwork;
        public List<List<uint>> statsPointsForStrength;
        public List<List<uint>> statsPointsForIntelligence;
        public List<List<uint>> statsPointsForChance;
        public List<List<uint>> statsPointsForAgility;
        public List<List<uint>> statsPointsForVitality;
        public List<List<uint>> statsPointsForWisdom;
        public List<uint> breedSpellsId;
        public List<BreedRoleByBreed> breedRoles;
        public List<uint> maleColors;
        public List<uint> femaleColors;
        public uint spawnMap;
        public uint complexity;
        public uint sortIndex;

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
        public uint ShortNameId
        {
            get
            {
                return shortNameId;
            }
            set
            {
                shortNameId = value;
            }
        }
        [D2OIgnore]
        public uint LongNameId
        {
            get
            {
                return longNameId;
            }
            set
            {
                longNameId = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint GameplayDescriptionId
        {
            get
            {
                return gameplayDescriptionId;
            }
            set
            {
                gameplayDescriptionId = value;
            }
        }
        [D2OIgnore]
        public uint GameplayClassDescriptionId
        {
            get
            {
                return gameplayClassDescriptionId;
            }
            set
            {
                gameplayClassDescriptionId = value;
            }
        }
        [D2OIgnore]
        public string MaleLook
        {
            get
            {
                return maleLook;
            }
            set
            {
                maleLook = value;
            }
        }
        [D2OIgnore]
        public string FemaleLook
        {
            get
            {
                return femaleLook;
            }
            set
            {
                femaleLook = value;
            }
        }
        [D2OIgnore]
        public uint CreatureBonesId
        {
            get
            {
                return creatureBonesId;
            }
            set
            {
                creatureBonesId = value;
            }
        }
        [D2OIgnore]
        public int MaleArtwork
        {
            get
            {
                return maleArtwork;
            }
            set
            {
                maleArtwork = value;
            }
        }
        [D2OIgnore]
        public int FemaleArtwork
        {
            get
            {
                return femaleArtwork;
            }
            set
            {
                femaleArtwork = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForStrength
        {
            get
            {
                return statsPointsForStrength;
            }
            set
            {
                statsPointsForStrength = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForIntelligence
        {
            get
            {
                return statsPointsForIntelligence;
            }
            set
            {
                statsPointsForIntelligence = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForChance
        {
            get
            {
                return statsPointsForChance;
            }
            set
            {
                statsPointsForChance = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForAgility
        {
            get
            {
                return statsPointsForAgility;
            }
            set
            {
                statsPointsForAgility = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForVitality
        {
            get
            {
                return statsPointsForVitality;
            }
            set
            {
                statsPointsForVitality = value;
            }
        }
        [D2OIgnore]
        public List<List<uint>> StatsPointsForWisdom
        {
            get
            {
                return statsPointsForWisdom;
            }
            set
            {
                statsPointsForWisdom = value;
            }
        }
        [D2OIgnore]
        public List<uint> BreedSpellsId
        {
            get
            {
                return breedSpellsId;
            }
            set
            {
                breedSpellsId = value;
            }
        }
        [D2OIgnore]
        public List<BreedRoleByBreed> BreedRoles
        {
            get
            {
                return breedRoles;
            }
            set
            {
                breedRoles = value;
            }
        }
        [D2OIgnore]
        public List<uint> MaleColors
        {
            get
            {
                return maleColors;
            }
            set
            {
                maleColors = value;
            }
        }
        [D2OIgnore]
        public List<uint> FemaleColors
        {
            get
            {
                return femaleColors;
            }
            set
            {
                femaleColors = value;
            }
        }
        [D2OIgnore]
        public uint SpawnMap
        {
            get
            {
                return spawnMap;
            }
            set
            {
                spawnMap = value;
            }
        }
        [D2OIgnore]
        public uint Complexity
        {
            get
            {
                return complexity;
            }
            set
            {
                complexity = value;
            }
        }
        [D2OIgnore]
        public uint SortIndex
        {
            get
            {
                return sortIndex;
            }
            set
            {
                sortIndex = value;
            }
        }

    }}
