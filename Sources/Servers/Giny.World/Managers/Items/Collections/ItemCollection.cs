using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public abstract class ItemCollection<T> where T : AbstractItem
    {
        private IDictionary<int, T> m_items;

        public int Count
        {
            get
            {
                return m_items.Count;
            }
        }

        public ItemCollection(IEnumerable<T> items)
        {
            this.m_items = CreateContainer();

            foreach (var item in items)
            {
                m_items.Add(item.UId, item);
            }

        }
        public ItemCollection()
        {
            this.m_items = CreateContainer();
        }
        public T[] GetItems()
        {
            return this.m_items.Values.ToArray();
        }
        public T[] GetItems(Func<T, bool> predicate)
        {
            return this.m_items.Values.Where(predicate).ToArray();
        }

        public virtual void OnItemAdded(T item) { }

        public virtual void OnItemStacked(T item, int quantity) { }

        public virtual void OnItemRemoved(T item) { }

        public virtual void OnItemUnstacked(T item, int quantity) { }

        public virtual void OnItemsAdded(IEnumerable<T> items) { }

        public virtual void OnItemsStackeds(IEnumerable<T> items) { }

        public virtual void OnItemsRemoved(IEnumerable<T> items) { }

        public virtual void OnItemsUnstackeds(IEnumerable<T> items) { }

        public virtual void OnItemQuantityChanged(T item, int quantity) { }

        public virtual void OnItemsQuantityChanged(IEnumerable<T> items) { }

        public virtual void AddItems(IEnumerable<T> items)
        {
            List<T> addedItems = new List<T>();
            List<T> stackedItems = new List<T>();

            foreach (var item in items)
            {
                item.OnCreated();

                T sameItem = GetSameItem(item.GId, item.Effects);

                if (sameItem != null)
                {
                    sameItem.Quantity += item.Quantity;

                    if (!stackedItems.Contains(sameItem))
                        stackedItems.Add(sameItem);

                }
                else
                {
                    addedItems.Add(item);
                }
            }

            OnItemsAdded(addedItems);

            foreach (var item in addedItems)
            {
                m_items.Add(item.UId, item);
            }

            OnItemsStackeds(stackedItems);

            OnItemsQuantityChanged(stackedItems);

        }


        public T? GetFirstItem(short gid, int minimumQuantity)
        {
            return GetItems().FirstOrDefault(x => x.GId == gid && x.Quantity >= minimumQuantity);
        }

        public virtual void RemoveItems(Dictionary<int, int> pairs)
        {
            List<T> removedItems = new List<T>();
            List<T> unstackedItems = new List<T>();

            foreach (var info in pairs)
            {
                T item = GetItem(info.Key);

                if (item != null)
                {
                    if (item.Quantity == info.Value)
                    {
                        m_items.Remove(item.UId);
                        removedItems.Add(item);
                    }
                    else
                    {
                        item.Quantity -= info.Value;
                        unstackedItems.Add(item);
                    }
                }
            }

            OnItemsRemoved(removedItems);

            OnItemsUnstackeds(unstackedItems);

            OnItemsQuantityChanged(unstackedItems);

        }
        public virtual void AddItem(T item)
        {
            item.OnCreated();

            T sameItem = GetSameItem(item.GId, item.Effects);

            if (sameItem != null)
            {
                sameItem.Quantity += item.Quantity;
                OnItemStacked(sameItem, item.Quantity);

                OnItemQuantityChanged(sameItem, item.Quantity);
            }
            else
            {
                OnItemAdded(item);
                m_items.Add(item.UId, item);
            }
        }

        public virtual T AddItem(T item, int quantity)
        {
            item.OnCreated();

            T sameItem = GetSameItem(item.GId, item.Effects);

            if (sameItem != null)
            {
                sameItem.Quantity += quantity;
                OnItemStacked(sameItem, quantity);
                OnItemQuantityChanged(sameItem, quantity);
                return sameItem;
            }
            else
            {
                item = (T)item.CloneWithUID();
                item.Quantity = quantity;
                m_items.Add(item.UId, item);
                OnItemAdded(item);
                return item;
            }
        }
        public virtual void RemoveItem(T item, int quantity)
        {
            if (item != null)
            {
                if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    return;

                if (item.Quantity >= quantity)
                {
                    if (item.Quantity == quantity)
                    {
                        m_items.Remove(item.UId);
                        OnItemRemoved(item);
                    }
                    else
                    {
                        item.Quantity -= quantity;
                        OnItemUnstacked(item, quantity);
                        OnItemQuantityChanged(item, quantity);
                    }
                }
            }

        }
        public void Clear()
        {
            IEnumerable<T> removedItems = m_items.Values;
            m_items.Clear();
            OnItemsRemoved(removedItems);
        }
        public bool RemoveItem(int uid)
        {
            T item = GetItem(uid);

            if (item != null)
            {
                RemoveItem(item, item.Quantity);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveItem(int uid, int quantity)
        {
            T item = GetItem(uid);

            if (item != null && item.Quantity >= quantity)
            {
                RemoveItem(item, quantity);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Contains(T item)
        {
            return m_items.Values.Contains(item);
        }
        protected virtual T GetSameItem(int gid, EffectCollection effects)
        {
            return GetItems().FirstOrDefault(x => x.GId == gid && x.Effects.SequenceEqual(effects));
        }
        public T GetItem(int uid)
        {
            m_items.TryGetValue(uid, out var item);
            return item;
        }
        public T GetItem(int gid, EffectCollection effects)
        {
            return GetSameItem(gid, effects);
        }
        public ObjectItem[] GetObjectsItems()
        {
            return Array.ConvertAll(this.GetItems(), x => x.GetObjectItem());
        }
        public bool Exist(short gid, int minimumQuantity)
        {
            return m_items.Values.FirstOrDefault(x => x.GId == gid && x.Quantity >= minimumQuantity) != null;
        }
        public bool Exist(short gId)
        {
            return m_items.Values.FirstOrDefault(x => x.GId == gId) != null;
        }

        protected virtual IDictionary<int, T> CreateContainer()
        {
            return new Dictionary<int, T>();
        }

        protected IDictionary<int, T> GetContainer()
        {
            return m_items;
        }

        public static Dictionary<List<T>, EffectCollection> SortByEffects(IEnumerable<T> items)
        {
            Dictionary<List<T>, EffectCollection> results = new Dictionary<List<T>, EffectCollection>();
            foreach (var item in items)
            {
                var same = results.FirstOrDefault(x => x.Value.SequenceEqual(item.Effects));

                if (same.Key == null)
                    results.Add(new List<T>() { item }, item.Effects);
                else
                    same.Key.Add(item);
            }
            return results;
        }
    }
}
