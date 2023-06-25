using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using Giny.World.Managers.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [Table("merchantitems")]
    public class MerchantItemRecord : AbstractItem, ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, MerchantItemRecord> MerchantItems = new ConcurrentDictionary<long, MerchantItemRecord>();

        [Ignore]
        public long Id => UId;

        public long CharacterId
        {
            get;
            set;
        }
        [Update]
        public long Price
        {
            get;
            set;
        }

        [Update]
        public int QuantitySold
        {
            get;
            set;
        }

        [Ignore]
        public bool Sold => Quantity == 0;

        public override AbstractItem CloneWithoutUID()
        {
            return new MerchantItemRecord()
            {
                CharacterId = CharacterId,
                AppearanceId = this.AppearanceId,
                Effects = this.Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = this.Position,
                Quantity = this.Quantity,
                UId = ItemsManager.Instance.PopItemUID(),
            };
        }

        public override AbstractItem CloneWithUID()
        {
            return new MerchantItemRecord()
            {
                CharacterId = CharacterId,
                AppearanceId = this.AppearanceId,
                Effects = this.Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = this.Position,
                Quantity = this.Quantity,
                UId = UId,
            };
        }
        public ObjectItemToSellInHumanVendorShop GetObjectItemToSellInHumanVendorShop()
        {
            return new ObjectItemToSellInHumanVendorShop()
            {
                effects = Effects.Select(x => x.GetObjectEffect()).ToArray(),
                objectGID = GId,
                objectPrice = Price,
                objectUID = UId,
                publicPrice = Price,
                quantity = Quantity,
            };
        }

        public ObjectItemToSell GetObjectItemToSell()
        {
            return new ObjectItemToSell()
            {
                effects = Effects.Select(x => x.GetObjectEffect()).ToArray(),
                objectGID = GId,
                objectPrice = Price,
                objectUID = UId,
                quantity = Quantity,
            };
        }
        public ObjectItemQuantityPriceDateEffects GetObjectItemQuantityPriceDateEffects()
        {
            return new ObjectItemQuantityPriceDateEffects()
            {
                date = 0,
                effects = new ObjectEffects(Effects.Select(x => x.GetObjectEffect()).ToArray()),
                objectGID = GId,
                price = Price * QuantitySold,
                quantity = QuantitySold,
            };
        }
        public static int GetLastItemUID()
        {
            return (int)MerchantItems.Keys.OrderByDescending(x => x).FirstOrDefault();
        }

        public static IEnumerable<MerchantItemRecord> GetMerchantItemsSolded(long characterId)
        {
            return GetMerchantItems(characterId).Where(x => x.QuantitySold > 0);
        }
        public static IEnumerable<MerchantItemRecord> GetMerchantItems(long characterId)
        {
            return MerchantItems.Values.Where(x => x.CharacterId == characterId);
        }
        public static IEnumerable<MerchantItemRecord> GetMerchantItems()
        {
            return MerchantItems.Values;
        }
        public override void Initialize()
        {

        }

        public static void RemoveMerchantItems(long id)
        {
            GetMerchantItems(id).RemoveInstantElements();
        }


    }
}
