using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Bidshops;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Bidshops;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges
{
    public class SellExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.BIDHOUSE_SELL;
            }
        }

        private BidShopRecord BidShop
        {
            get;
            set;
        }

        public SellExchange(Character character, BidShopRecord bidShop)
            : base(character)
        {
            this.BidShop = bidShop;

        }
        public override void MoveItemPriced(int uid, int quantity, long price)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null && item.Quantity >= quantity && item.CanBeExchanged())
            {
                BidShopItemRecord selledItem = item.ToBidShopItemRecord(BidShop.Id, Character.Client.Account.Id, price);
                selledItem.Quantity = quantity;
                Character.Inventory.RemoveItem(item.UId, quantity);
                AddSelledItem(selledItem);
            }
        }
        public override void ModifyItemPriced(int uid, int quantity, long price)
        {
            BidShopItemRecord item = BidshopsManager.Instance.GetItem(BidShop.Id, uid);

            if (item != null)
            {
                item.Price = price;
                item.Quantity = quantity;
                item.UpdateElement();
                Open();
            }
        }
        public void AddSelledItem(BidShopItemRecord item)
        {
            BidshopsManager.Instance.AddItem(BidShop.Id, item);

            Character.Client.Send(new ExchangeBidHouseItemAddOkMessage(item.GetObjectItemToSellInBid()));

        }

        public override void Open()
        {
            IEnumerable<BidShopItemRecord> itemSelled = BidshopsManager.Instance.GetSellerItems(BidShop.Id, Character.Client.Account.Id);

            Character.Client.Send(new ExchangeStartedBidSellerMessage(BidShop.GetBuyerDescriptor(),
                itemSelled.Select(x => x.GetObjectItemToSellInBid()).ToArray()));
        }

        public override void MoveItem(int uid, int quantity)
        {
            if (quantity < 0)
            {
                BidShopItemRecord item = BidshopsManager.Instance.GetItem(BidShop.Id, uid);

                if (item != null && item.Quantity >= Math.Abs(quantity))
                {
                    BidshopsManager.Instance.RemoveItem(BidShop.Id, item);
                    Character.Inventory.AddItem(item.ToCharacterItemRecord(Character.Id));
                    Character.Client.Send(new ExchangeBidHouseItemRemoveOkMessage((int)uid));
                }
            }
        }

        public override void Ready(bool ready, short step)
        {
            throw new NotImplementedException();
        }

        public override void MoveKamas(long quantity)
        {
            throw new NotImplementedException();
        }

        public override void OnNpcGenericAction(NpcActionsEnum action)
        {
            if (action == NpcActionsEnum.SELL)
            {
                this.Close();
                this.Character.OpenSellExchange(BidShop);
            }
            else if (action == NpcActionsEnum.BUY)
            {
                this.Close();
                Character.OpenBuyExchange(BidShop);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
