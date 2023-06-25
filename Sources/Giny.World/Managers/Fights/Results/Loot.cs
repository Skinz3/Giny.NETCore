using Giny.Protocol.Types;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public class DroppedItem
    {
        public short ItemGId
        {
            get;
            set;
        }
        public int Amount
        {
            get;
            set;
        }
        public DroppedItem(short itemGId, int amount)
        {
            this.ItemGId = itemGId;
            this.Amount = amount;
        }
    }
    public class Loot
    {
        private readonly Dictionary<int, DroppedItem> m_items = new Dictionary<int, DroppedItem>();
        public IReadOnlyDictionary<int, DroppedItem> Items
        {
            get
            {
                return new ReadOnlyDictionary<int, DroppedItem>(this.m_items);
            }
        }
        public int Kamas
        {
            get;
            set;
        }
        public void AddItem(short itemId)
        {
            this.AddItem(itemId, 1);
        }
        public void AddItem(short itemId, int amount)
        {
            if (this.m_items.ContainsKey(itemId))
            {
                this.m_items[itemId].Amount += 1;
            }
            else
            {
                this.m_items.Add(itemId, new DroppedItem(itemId, amount));
            }
        }
        public void AddItem(DroppedItem item)
        {
            if (this.m_items.ContainsKey(item.ItemGId))
            {
                this.m_items[item.ItemGId].Amount += item.Amount;
            }
            else
            {
                this.m_items.Add(item.ItemGId, new DroppedItem(item.ItemGId, item.Amount));
            }
        }
        public FightLoot GetFightLoot()
        {
            return new FightLoot(this.m_items.Values.SelectMany((DroppedItem entry
                ) => new int[]
            {
                entry.ItemGId,
                entry.Amount
            }).ToArray(), this.Kamas);
        }
    }
}
