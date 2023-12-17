using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Look;
using Giny.World.Records.Spells;
using ProtoBuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Breeds
{
    [Table("breeds")]
    [D2OClass("Breed")]
    public class BreedRecord : IRecord
    {
        [Container]
        private static readonly Dictionary<long, BreedRecord> Breeds = new Dictionary<long, BreedRecord>();

        long IRecord.Id => Id;

        [D2OField("id")]
        [Primary]
        public byte Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("shortNameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("maleLook")]
        public ServerEntityLook MaleLook
        {
            get;
            set;
        }
        [D2OField("femaleLook")]
        public ServerEntityLook FemaleLook
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForStrength")]
        public StatUpgradeCost[] StatsPointForStrength
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForIntelligence")]
        public StatUpgradeCost[] StatsPointForIntelligence
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForChance")]
        public StatUpgradeCost[] StatsPointForChance
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForAgility")]
        public StatUpgradeCost[] StatsPointForAgility
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForVitality")]
        public StatUpgradeCost[] StatsPointForVitality
        {
            get;
            set;
        }
        [Blob]
        [D2OField("statsPointsForWisdom")]
        public StatUpgradeCost[] StatsPointForWisdom
        {
            get;
            set;
        }
        [Blob]
        [D2OField("maleColors")]
        public int[] MaleColors
        {
            get;
            set;
        }
        [Blob]
        [D2OField("maleColors")]
        public int[] FemaleColors
        {
            get;
            set;
        }
        [Blob]
        [D2OField("breedSpellsId")]
        public short[] SpellIds
        {
            get;
            set;
        }
        [Update]
        public short GraveBonesId
        {
            get;
            set;
        }

        [Ignore]
        public SpellRecord[] Spells
        {
            get;
            private set;
        }

        [Ignore]
        public BreedEnum BreedEnum => (BreedEnum)Id;


        public int GetStatUpgradeCostIndex(int actualpoints, StatUpgradeCost[] upgradeCost)
        {
            int result;
            for (int i = 0; i < upgradeCost.Length - 1; i++)
            {
                if (upgradeCost[i].Until <= actualpoints && upgradeCost[i + 1].Until > actualpoints)
                {
                    result = i;
                    return result;
                }
            }
            result = upgradeCost.Length - 1;
            return result;
        }
        public StatUpgradeCost[] GetStatUpgradeCost(StatsBoostEnum statId)
        {
            switch (statId)
            {
                case StatsBoostEnum.STRENGTH:
                    return StatsPointForStrength;
                case StatsBoostEnum.VITALITY:
                    return StatsPointForVitality;
                case StatsBoostEnum.WISDOM:
                    return StatsPointForWisdom;
                case StatsBoostEnum.CHANCE:
                    return StatsPointForChance;
                case StatsBoostEnum.AGILITY:
                    return StatsPointForAgility;
                case StatsBoostEnum.INTELLIGENCE:
                    return StatsPointForIntelligence;
            }
            return null;
        }
        [StartupInvoke("Breed spells", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var breed in Breeds)
            {
                breed.Value.Spells = new SpellRecord[breed.Value.SpellIds.Length];

                for (int i = 0; i < breed.Value.Spells.Length; i++)
                {
                    breed.Value.Spells[i] = SpellRecord.GetSpellRecord(breed.Value.SpellIds[i]);
                }
            }
        }
      

        public static BreedRecord GetBreed(byte breedId)
        {
            return Breeds[breedId];
        }
        public static IEnumerable<BreedRecord> GetBreeds()
        {
            return Breeds.Values;
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}", Id, Name);
        }
    }
    [ProtoContract]
    public struct StatUpgradeCost
    {
        [ProtoMember(1)]
        public short Until;

        [ProtoMember(2)]
        public short Cost;

        public StatUpgradeCost(short until, short cost)
        {
            this.Until = until;
            this.Cost = cost;
        }
    }
}
