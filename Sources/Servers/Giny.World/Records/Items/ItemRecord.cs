using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [D2OClass("Item")]
    [Table("items")]
    public class ItemRecord : ITable
    {
        private const int MinimumItemPrice = 1;

        [Container]
        private static readonly ConcurrentDictionary<long, ItemRecord> Items = new ConcurrentDictionary<long, ItemRecord>();

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
        [Ignore]
        public ItemSetRecord ItemSet
        {
            get;
            private set;
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
        [Update]
        public string Look
        {
            get;
            set;
        }

        [Ignore]
        public ItemTypeEnum TypeEnum => (ItemTypeEnum)TypeId;

        [Ignore]
        public bool HasSet => ItemSetId != -1;

        public bool IsCeremonialItem()
        {
            return TypeEnum == ItemTypeEnum.CEREMONIAL_CAPE ||
                TypeEnum == ItemTypeEnum.CEREMONIAL_HAT ||
                TypeEnum == ItemTypeEnum.CEREMONIAL_PET ||
                TypeEnum == ItemTypeEnum.CEREMONIAL_PETSMOUNT ||
                TypeEnum == ItemTypeEnum.CEREMONIAL_SHIELD ||
                TypeEnum == ItemTypeEnum.CEREMONIAL_WEAPON ||
                TypeEnum == ItemTypeEnum.MISCELLANEOUS_CEREMONIAL_ITEM;
        }

        public ObjectItemToSellInNpcShop GetObjectItemToSellInNpcShop()
        {
            return new ObjectItemToSellInNpcShop()
            {
                buyCriterion = string.Empty,
                effects = Effects.Select(x => x.GetObjectEffect()).ToArray(),
                objectGID = (short)Id,
                objectPrice = (long)Price,
            };
        }
        [StartupInvoke("Items Bindings", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var item in Items.Values)
            {
                if (item.HasSet)
                {
                    item.ItemSet = ItemSetRecord.GetItemSet(item.ItemSetId);
                }

                if (item.Price < MinimumItemPrice)
                {
                    item.Price = MinimumItemPrice;
                }
            }
        }
        public static IEnumerable<ItemRecord> GetItems()
        {
            return Items.Values;
        }
        public static void Add(ItemRecord item)
        {
            if (!Items.ContainsKey(item.Id))
            {
                Items[item.Id] = item;
            }
        }
        public static bool ItemExists(long gid)
        {
            return Items.ContainsKey(gid);
        }
        public static ItemRecord GetItem(long gid)
        {
            ItemRecord result = null;
            Items.TryGetValue(gid, out result);
            return result;
        }

        public override string ToString()
        {
            return "{" + Id + "} " + Name;
        }

    }
}
