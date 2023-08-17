using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.ORM;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Bidshops;
using Giny.World.Records.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Bidshops
{
    public class BidshopsManager : Singleton<BidshopsManager> 
    {
        private ConcurrentDictionary<long, ConcurrentDictionary<long, BidShopItemRecord>> m_bidshopItems = new ConcurrentDictionary<long, ConcurrentDictionary<long, BidShopItemRecord>>();

        private ConcurrentDictionary<int, long> m_averagePrices = new ConcurrentDictionary<int, long>();

        [StartupInvoke("Bidshops", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            m_bidshopItems.Clear();

            foreach (var bidshop in BidShopRecord.GetBidShops())
            {
                m_bidshopItems.TryAdd(bidshop.Id, new ConcurrentDictionary<long, BidShopItemRecord>());
            }

            foreach (var item in BidShopItemRecord.GetItems())
            {
                m_bidshopItems[item.BidShopId].TryAdd(item.UId, item);
            }
        }
        public void AddItem(long bidshopId, BidShopItemRecord item)
        {
            m_bidshopItems[bidshopId].TryAdd(item.UId, item);
            item.AddElement();
        }
        public void RemoveItem(long bidshopId, BidShopItemRecord item)
        {
            m_bidshopItems[bidshopId].TryRemove(item.UId);
            item.RemoveElement();
        }
        public IEnumerable<BidShopItemRecord> GetItems(long bidshopId)
        {
            return m_bidshopItems[bidshopId].Values.Where(x => !x.Sold);
        }
        public BidShopItemRecord GetItem(long bidshopId, int uid)
        {
            BidShopItemRecord result = null;
            m_bidshopItems[bidshopId].TryGetValue(uid, out result);
            return result;
        }

        public long GetAveragePrice(int genId)
        {
            if (m_averagePrices.ContainsKey(genId))
            {
                return m_averagePrices[genId];
            }
            else
            {
                return 0;
            }
        }

        public ConcurrentDictionary<int, long> GetAveragePrices()
        {
            return m_averagePrices;
        }
        public void RefreshAveragePrices()
        {
            m_averagePrices.Clear();

            Dictionary<int, List<long>> prices = new Dictionary<int, List<long>>();

            foreach (var bidshop in m_bidshopItems)
            {
                foreach (var item in bidshop.Value)
                {
                    if (item.Value.Quantity == 1)
                    {
                        if (!prices.ContainsKey(item.Value.GId))
                        {
                            prices.Add(item.Value.GId, new List<long>());
                        }
                        prices[item.Value.GId].Add(item.Value.Price);
                    }
                }
            }

            foreach (var price in prices)
            {
                m_averagePrices.TryAdd(price.Key, price.Value.Sum(x => x) / price.Value.Count);
            }
        }

        public IEnumerable<BidShopItemRecord> GetSellerItems(long bidshopId, int accountId)
        {
            return m_bidshopItems[bidshopId].Values.Where(x => x.AccountId == accountId);
        }

        public IEnumerable<BidShopItemRecord> GetSoldItem(Character character)
        {
            return BidShopItemRecord.GetItems().Where(x => x.Sold && x.AccountId == character.Client.Account.Id);
        }
    }
}
