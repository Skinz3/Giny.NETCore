using Giny.Core.Network.Messages;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Network;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Items
{
    class MimicrysHandler
    {
        public static void SendMimicryObjectErrorMessage(WorldClient client, MimicryErrorEnum error)
        {
            client.Send(new MimicryObjectErrorMessage(true, (byte)ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR, (byte)error));
        }

        [MessageHandler]
        public static void HandleMimicryObjectEraseRequest(MimicryObjectEraseRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                return;

            CharacterItemRecord item = client.Character.Inventory.GetItem(message.hostUID);

            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                client.Character.Inventory.SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, 1);
            }

            item.EraseMimicry();
            item.UpdateElement();
            client.Character.Inventory.Refresh();
            client.Character.RefreshActorOnMap();
            client.Character.RefreshStats();
        }
        [MessageHandler]
        public static void HandleMimicryObjectFeedAndAssociateRequest(MimicryObjectFeedAndAssociateRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                return;

            CharacterItemRecord symbioteItem = client.Character.Inventory.GetItem(message.symbioteUID);
            CharacterItemRecord hostItem = client.Character.Inventory.GetItem(message.hostUID);
            CharacterItemRecord foodItem = client.Character.Inventory.GetItem(message.foodUID);

            if (symbioteItem == null)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_MIMICRY);
                return;
            }
            else if (hostItem == null || foodItem == null)
            {
                SendMimicryObjectErrorMessage(client, hostItem == null ? MimicryErrorEnum.NO_VALID_HOST : MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }
            else if (hostItem.Effects.IsAssociated || hostItem.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_HOST);
                return;
            }
            else if (foodItem.Effects.IsAssociated || foodItem.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.NO_VALID_FOOD);
                return;
            }
            else if (hostItem.GId == foodItem.GId)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.SAME_SKIN);
                return;
            }
            else if (foodItem.Record.Level > hostItem.Record.Level)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_LEVEL);
                return;
            }
            else if (foodItem.Record.TypeId != hostItem.Record.TypeId)
            {
                SendMimicryObjectErrorMessage(client, MimicryErrorEnum.FOOD_TYPE);
                return;
            }

            CharacterItemRecord result = hostItem.ToMimicry(foodItem.Record);

            if (message.preview)
            {
                client.Send(new MimicryObjectPreviewMessage(result.GetObjectItem()));
            }
            else
            {
                client.Character.Inventory.RemoveItem(message.hostUID, 1);
                client.Character.Inventory.RemoveItem(message.foodUID, 1);
                client.Character.Inventory.RemoveItem(message.symbioteUID, 1);
                client.Character.Inventory.AddItem(result);

                client.Send(new MimicryObjectAssociatedMessage(result.UId));
            }
        }
    }
}
