using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class JobItemCollection : ItemCollection<CharacterItemRecord>
    {
        private Character Character
        {
            get;
            set;
        }

        public JobItemCollection(Character character)
        {
            this.Character = character;
        }
        public override void OnItemAdded(CharacterItemRecord item)
        {
            Character.Client.Send(new ExchangeObjectAddedMessage()
            {
                @object = item.GetObjectItem(),
                remote = false,
            });
        }
        public override void OnItemRemoved(CharacterItemRecord item)
        {
            Character.Client.Send(new ExchangeObjectRemovedMessage()
            {
                objectUID = item.UId,
                remote = false
            });
        }
        public override void OnItemQuantityChanged(CharacterItemRecord item, int quantity)
        {
            Character.Client.Send(new ExchangeObjectModifiedMessage()
            {
                @object = item.GetObjectItem(),
                remote = false,
            });
        }
        public override void OnItemsRemoved(IEnumerable<CharacterItemRecord> items)
        {
            Character.Client.Send(new ExchangeObjectsRemovedMessage()
            {
                objectUID = items.Select(x => x.UId).ToArray(),
                remote = false,
            });
        }


        private bool CanAddItem(CharacterItemRecord item, int quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                return false;

            CharacterItemRecord exchanged = GetItem(item.GId, item.Effects);

            if (exchanged != null && exchanged.UId != item.UId)
                return false;

            exchanged = GetItem(item.UId);

            if (exchanged == null)
            {
                return true;
            }

            if (exchanged.Quantity + quantity > item.Quantity)
                return false;
            else
                return true;
        }
        public void MoveItem(int uid, int quantity)
        {
            CharacterItemRecord item = null;

            if (quantity > 0)
            {
                item = Character.Inventory.GetItem(uid);

                if (item != null && CanAddItem(item, quantity))
                {
                    AddItem(item, quantity);
                }
            }
            else if (quantity < 0)
            {
                item = GetItem(uid);

                if (item != null)
                {
                    RemoveItem(item, Math.Abs(quantity));
                }
            }
            else
            {
                return;
            }
        }

    }
}
