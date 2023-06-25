using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Merchants;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges
{
    public class MerchantSellerExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.DISCONNECTED_VENDOR;

        private CharacterMerchant Merchant
        {
            get;
            set;
        }
        public MerchantSellerExchange(Character character, CharacterMerchant merchant) : base(character)
        {
            this.Merchant = merchant;
            this.Merchant.AddExchange(this);
        }


        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void MoveItem(int uid, int quantity)
        {
            throw new NotImplementedException();
        }

        public override void MoveItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void MoveKamas(long quantity)
        {
            throw new NotImplementedException();
        }

        public override void OnNpcGenericAction(NpcActionsEnum action)
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            Character.Client.Send(new ExchangeStartOkHumanVendorMessage()
            {
                sellerId = Merchant.Id,
                objectsInfos = Merchant.Items.GetItems().Where(x => !x.Sold).Select(x => x.GetObjectItemToSellInHumanVendorShop()).ToArray(),
            });

        }

        public void Buy(int uid, int quantity)
        {
            MerchantItemRecord item = Merchant.Items.GetItem(uid);

            if (item == null)
            {
                Character.OnExchangeError(ExchangeErrorEnum.BUY_ERROR);
                return;
            }

            if (item.Quantity < quantity)
            {
                Character.OnExchangeError(ExchangeErrorEnum.BUY_ERROR);
                return;
            }

            long cost = item.Price * quantity;

            if (!Character.RemoveKamas(cost))
            {
                Character.OnExchangeError(ExchangeErrorEnum.BUY_ERROR);
                return;
            }

            Character.OnKamasLost(cost);

            Character.Inventory.AddItem(item.ToCharacterItemRecord(Character.Id), quantity);

            Merchant.Items.RemoveItem(item, quantity);

            Character.NotifyItemGained(item.GId, quantity);

            Character.Client.Send(new ExchangeBuyOkMessage());

            if (Merchant.Items.GetItems().Where(x => !x.Sold).Count() == 0)
            {
                this.Close();
                Merchant.Remove();
            }
        }

        public override void Ready(bool ready, short step)
        {
            throw new NotImplementedException();
        }
        public override void Close()
        {
            Merchant.RemoveExchange(this);
            base.Close();
        }
    }
}
