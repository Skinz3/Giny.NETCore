using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;

namespace Giny.World.Managers.Exchanges
{
    public class MerchantVendorExchange : Exchange
    {
        public MerchantVendorExchange(Character character) : base(character)
        {

        }

        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.DISCONNECTED_VENDOR;

        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            Character.MerchantItems.ModifyItemPriced(objectUID, quantity, price);
        }

        public override void MoveItem(int uid, int quantity)
        {
            Character.MerchantItems.TakeBack(uid, quantity);
        }

        public override void MoveItemPriced(int objectUID, int quantity, long price)
        {
            Character.MerchantItems.Store(objectUID, quantity, price);
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
            Character.Client.Send(new ExchangeShopStockStartedMessage(Character.MerchantItems.GetItems().Select(x => x.GetObjectItemToSell()).ToArray()));
        }

        public override void Ready(bool ready, short step)
        {
            throw new NotImplementedException();
        }
    }
}
