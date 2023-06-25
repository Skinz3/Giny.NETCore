using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Merchants;
using Giny.World.Managers.Exchanges;
using Giny.World.Managers.Exchanges.Jobs;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Exchanges
{
    class ExchangesHandler
    {
        [MessageHandler]
        public static void HandleExchangeObjectTransfertAllToInv(ExchangeObjectTransfertAllToInvMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<PlayerTradeExchange>())
            {
                client.Character.GetDialog<PlayerTradeExchange>().TransferAllToInventory();
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectTransfertAllFromInv(ExchangeObjectTransfertAllFromInvMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<PlayerTradeExchange>())
            {
                client.Character.GetDialog<PlayerTradeExchange>().TransferAllFromInventory();
            }
        }
        [MessageHandler]
        public static void HandleExchangeOnHumanVendorRequest(ExchangeOnHumanVendorRequestMessage message, WorldClient client)
        {
            CharacterMerchant merchant = client.Character.Map.Instance.GetEntity<CharacterMerchant>(message.humanVendorId);

            if (merchant != null && merchant.CellId == message.humanVendorCell)
            {
                client.Character.OpenMerchantAsSellerExchange(merchant);
            }
            else
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
            }
        }
        [MessageHandler]
        public static void HandleExchangeStartAsVendor(ExchangeStartAsVendorMessage message, WorldClient client)
        {
            client.Character.EnterMerchantMode();
        }
        [MessageHandler]
        public static void HandleExchangeShowVendorTax(ExchangeShowVendorTaxMessage message, WorldClient client)
        {
            client.Send(new ExchangeReplyTaxVendorMessage(0, client.Character.MerchantItems.GetMerchantTax()));
        }
        [MessageHandler]
        public static void HandleExchangeCraftCountRequest(ExchangeCraftCountRequestMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<JobExchange>())
            {
                client.Character.GetDialog<JobExchange>().SetCount(message.count);
            }
        }
        [MessageHandler]
        public static void HandleExchangeSetCraftRecipeMessage(ExchangeSetCraftRecipeMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<CraftExchange>())
            {
                client.Character.GetDialog<CraftExchange>().SetRecipe(message.objectGID);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectMovePriced(ExchangeObjectMovePricedMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<Exchange>())
            {
                client.Character.GetDialog<Exchange>().MoveItemPriced(message.objectUID, message.quantity, message.price);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectModifyPriced(ExchangeObjectModifyPricedMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<Exchange>())
            {
                client.Character.GetDialog<Exchange>().ModifyItemPriced(message.objectUID, message.quantity, message.price);
            }
        }
        [MessageHandler]
        public static void HandleExchangeRequestOnShopStock(ExchangeRequestOnShopStockMessage message, WorldClient client)
        {
            client.Character.OpenMerchantAsVendorExchange();
        }
        [MessageHandler]
        public static void HandleExchangeBuyMessage(ExchangeBuyMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.NPC_SHOP))
            {
                client.Character.GetDialog<BuySellExchange>().Buy((short)message.objectToBuyId, message.quantity);
            }
            else if (client.Character.IsInExchange(ExchangeTypeEnum.DISCONNECTED_VENDOR))
            {
                client.Character.GetDialog<MerchantSellerExchange>().Buy(message.objectToBuyId, message.quantity);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectMoveMessage(ExchangeObjectMoveMessage message, WorldClient client)
        {
            client.Character.GetDialog<Exchange>().MoveItem(message.objectUID, message.quantity);
        }
        [MessageHandler]
        public static void HandleExchangeReadyMessage(ExchangeReadyMessage message, WorldClient client)
        {
            if (client.Character.GetDialog<Exchange>() != null)
                client.Character.GetDialog<Exchange>().Ready(message.ready, message.step);
        }
        [MessageHandler]
        public static void HandleFocusedExchangeReadyMessage(FocusedExchangeReadyMessage message, WorldClient client)
        {
            if (client.Character.GetDialog<Exchange>() != null)
                client.Character.GetDialog<Exchange>().Ready(message.ready, message.step);
        }
        [MessageHandler]
        public static void HandleExchangeObjectMoveKamasMessage(ExchangeObjectMoveKamaMessage message, WorldClient client)
        {
            if (client.Character.Record.Kamas >= message.quantity && client.Character.GetDialog<Exchange>() != null)
            {
                client.Character.GetDialog<Exchange>().MoveKamas(message.quantity);
            }
        }
        [MessageHandler]
        public static void HandleExchangePlayerRequestMessage(ExchangePlayerRequestMessage message, WorldClient client)
        {
            Character target = client.Character.Map.Instance.GetEntity<Character>((long)message.target);

            if (target == null)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }
            if (target.Busy)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_CHARACTER_OCCUPIED);
                return;
            }
            if (client.Character.Busy)
            {
                return;
            }
            if (target.Map == null || target.Record.MapId != client.Character.Record.MapId)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }
            if (!target.Map.Position.AllowExchangesBetweenPlayers)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }

            switch ((ExchangeTypeEnum)message.exchangeType)
            {
                case ExchangeTypeEnum.PLAYER_TRADE:
                    target.OpenRequestBox(new PlayerTradeRequestBox(client.Character, target));
                    break;
                default:
                    client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                    break;

            }
        }
        [MessageHandler]
        public static void HandleExchangeAcceptMessage(ExchangeAcceptMessage message, WorldClient client)
        {
            if (client.Character.RequestBox is PlayerTradeRequestBox)
                client.Character.RequestBox.Accept();
        }
    }
}
