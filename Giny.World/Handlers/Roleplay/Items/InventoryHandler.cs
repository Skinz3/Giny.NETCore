using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Protocol.Messages;
using Giny.World.Network;
using Giny.Protocol.Enums;
using Giny.World.Records.Items;
using Giny.World.Managers.Items;

namespace Giny.World.Handlers.Items
{
    class InventoryHandler
    {
        [MessageHandler]
        public static void HandleObjectUseMessage(ObjectUseMessage message, WorldClient client)
        {
            if (!client.Character.Busy && !client.Character.Fighting)
                client.Character.UseItem(message.objectUID, true);
        }
        [MessageHandler]
        public static void HandleObjectSetPositionMessage(ObjectSetPositionMessage message, WorldClient client)
        {
            if (client.Character.Busy)
            {
                return;
            }

            if (client.Character.Fighting && client.Character.Fighter.Fight.Started)
            {
                return;
            }

            client.Character.Inventory.SetItemPosition(message.objectUID, (CharacterInventoryPositionEnum)message.position, message.quantity);

            if (client.Character.Fighting)
            {
                client.Character.Fighter.Restore();
            }
        }
        [MessageHandler]
        public static void HandleObjectDeleteMessage(ObjectDeleteMessage message, WorldClient client)
        {
            if (!client.Character.Busy && !client.Character.Fighting)
            {
                CharacterItemRecord item = client.Character.Inventory.GetItem(message.objectUID);

                if (item != null)
                {
                    client.Character.Inventory.RemoveItem(item, message.quantity);
                }
            }
        }
        [MessageHandler]
        public static void HandleLivingObjectDissociateMessage(LivingObjectDissociateMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                return;
            }
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.livingUID);

            if (item != null)
            {
                LivingObjectManager.Instance.DissociateLivingObject(client.Character, item);
                client.Character.RefreshActorOnMap();
                client.Character.Inventory.RefreshWeight();
            }
        }
        [MessageHandler]
        public static void HandleLivingObjectChangeSkinRequestMessage(LivingObjectChangeSkinRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                return;
            }

            CharacterItemRecord item = client.Character.Inventory.GetItem(message.livingUID);

            if (item != null)
            {
                LivingObjectManager.Instance.ChangeLivingObjectSkin(client.Character, item, message.skinId, (CharacterInventoryPositionEnum)message.livingPosition);
            }
        }
        [MessageHandler]
        public static void HandleObjectFeedMessage(ObjectFeedMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                return;
            }
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.objectUID);

            if (item != null)
            {
                item.Feed(client.Character, message.meal);
            }
        }
        [MessageHandler]
        public static void HandleWrapperObjectDissociateRequestMessage(WrapperObjectDissociateRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                return;
            }
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.hostUID);

            if (item != null)
            {
                client.Character.Inventory.Dissociate(item, (CharacterInventoryPositionEnum)message.hostPos);
            }
        }
    }
}
