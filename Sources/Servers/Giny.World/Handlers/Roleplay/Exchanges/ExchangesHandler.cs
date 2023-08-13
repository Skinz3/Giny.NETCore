using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Exchanges;
using Giny.World.Managers.Exchanges.Jobs;
using Giny.World.Managers.Exchanges.Trades;
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
            if (client.Character.IsInDialog<TradeExchange>())
            {
                client.Character.GetDialog<TradeExchange>().TransferAllToInventory();
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectTransfertAllFromInv(ExchangeObjectTransfertAllFromInvMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog<TradeExchange>())
            {
                client.Character.GetDialog<TradeExchange>().TransferAllFromInventory();
            }

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
        public static void HandleExchangeBuyMessage(ExchangeBuyMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.NPC_SHOP))
            {
                client.Character.GetDialog<BuySellExchange>().Buy((short)message.objectToBuyId, message.quantity);
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
