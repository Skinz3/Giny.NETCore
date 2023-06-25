using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Bidshops
{
    [Table("Bidshops")]
    public class BidShopRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, BidShopRecord> Bidshops = new ConcurrentDictionary<long, BidShopRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }

        public List<int> Quantities
        {
            get;
            set;
        }

        public List<int> ItemTypes
        {
            get;
            set;
        }

        public int MaxItemPerAccount
        {
            get;
            set;
        }


        public static BidShopRecord GetBidShop(int id)
        {
            return Bidshops[id];
        }
        public SellerBuyerDescriptor GetBuyerDescriptor()
        {
            return new SellerBuyerDescriptor(Quantities.ToArray(), ItemTypes.ToArray(), 0, 0,
                200, MaxItemPerAccount, 0, 50);
        }
        public static IEnumerable<BidShopRecord> GetBidShops()
        {
            return Bidshops.Values;
        }
    }
}
