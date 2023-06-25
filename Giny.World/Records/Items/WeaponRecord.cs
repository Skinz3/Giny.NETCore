using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Effects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [D2OClass("Weapon")]
    [Table("weapons")]
    public class WeaponRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, WeaponRecord> Weapons = new ConcurrentDictionary<long, WeaponRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [Update]
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("typeId")]
        public short TypeId
        {
            get;
            set;
        }
        [Ignore]
        public ItemTypeEnum TypeEnum => (ItemTypeEnum)TypeId;

        [D2OField("level")]
        public short Level
        {
            get;
            set;
        }
        [D2OField("realWeight")]
        public int RealWeight
        {
            get;
            set;
        }
        [D2OField("cursed")]
        public bool Cursed
        {
            get;
            set;
        }
        [D2OField("usable")]
        public bool Usable
        {
            get;
            set;
        }
        [D2OField("exchangeable")]
        public bool Exchangeable
        {
            get;
            set;
        }
        [Update]
        [D2OField("price")]
        public double Price
        {
            get;
            set;
        }
        [D2OField("etheral")]
        public bool Etheral
        {
            get;
            set;
        }
        [D2OField("itemSetId")]
        public int ItemSetId
        {
            get;
            set;
        }
        [D2OField("criteria")]
        public string Criteria
        {
            get;
            set;
        }
        [Update]
        [D2OField("appearanceId")]
        public short AppearenceId
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("dropMonsterIds")]
        public short[] DropMonsterIds
        {
            get;
            set;
        }
        [D2OField("recipeSlots")]
        public int RecipeSlots
        {
            get;
            set;
        }
        [D2OField("recipeIds")]
        [ProtoSerialize]
        public uint[] RecipeIds
        {
            get;
            set;
        }
        [Update]
        [ProtoSerialize]
        [D2OField("possibleEffects")]
        public EffectCollection Effects
        {
            get;
            set;
        }
        [D2OField("craftXpRatio")]
        public int CraftXpRatio
        {
            get;
            set;
        }
        [D2OField("isSaleable")]
        public bool IsSaleable
        {
            get;
            set;
        }
        [D2OField("apCost")]
        public short ApCost
        {
            get;
            set;
        }

        [D2OField("minRange")]
        public short MinRange
        {
            get;
            set;
        }

        [D2OField("range")]
        public short MaxRange
        {
            get;
            set;
        }

        [D2OField("maxCastPerTurn")]
        public short MaxCastPerTurn
        {
            get;
            set;
        }

        [D2OField("castInLine")]
        public bool CastInLine
        {
            get;
            set;
        }

        [D2OField("castInDiagonal")]
        public bool CastInDiagonal
        {
            get;
            set;
        }

        [D2OField("castTestLos")]
        public bool CastTestLos
        {
            get;
            set;
        }

        [D2OField("criticalHitProbability")]
        public short CriticalHitProbability
        {
            get;
            set;
        }

        [D2OField("criticalHitBonus")]
        public short CriticalHitBonus
        {
            get;
            set;
        }

        public ItemRecord ToItemRecord()
        {
            return new ItemRecord()
            {
                AppearenceId = this.AppearenceId,
                CraftXpRatio = this.CraftXpRatio,
                Criteria = this.Criteria,
                Cursed = this.Cursed,
                DropMonsterIds = this.DropMonsterIds,
                Effects = this.Effects,
                Etheral = this.Etheral,
                Exchangeable = this.Exchangeable,
                Id = this.Id,
                IsSaleable = this.IsSaleable,
                ItemSetId = this.ItemSetId,
                Level = this.Level,
                Look = string.Empty,
                Name = this.Name,
                Price = this.Price,
                RealWeight = this.RealWeight,
                RecipeIds = this.RecipeIds,
                RecipeSlots = this.RecipeSlots,
                TypeId = this.TypeId,
                Usable = this.Usable,
            };
        }
        [StartupInvoke("Weapons Bindings", StartupInvokePriority.FourthPass)]
        public static void Initialize()
        {
            foreach (var weaponRecord in Weapons.Values)
            {
                ItemRecord.Add(weaponRecord.ToItemRecord());
            }
        }
        public static WeaponRecord GetWeapon(long id)
        {
            WeaponRecord result = null;
            Weapons.TryGetValue(id, out result);
            return result;
        }
    }
}
