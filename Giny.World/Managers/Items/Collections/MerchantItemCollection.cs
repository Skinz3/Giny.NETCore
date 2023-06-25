using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Giny.ORM;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class MerchantItemCollection : ItemCollection<MerchantItemRecord>
    {
        private Character Character
        {
            get;
            set;
        }
        public MerchantItemCollection(Character character, IEnumerable<MerchantItemRecord> items) : base(items)
        {
            this.Character = character;
        }

        public void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            MerchantItemRecord item = Character.MerchantItems.GetItem(objectUID);

            if (item != null)
            {
                if (price > 0)
                {
                    if (price > Inventory.MaximumKamas)
                    {
                        return;
                    }

                    item.Price = price;
                }

                if (quantity > 0)
                {
                    if (quantity < item.Quantity)
                    {
                        CharacterItemRecord characterItem = item.ToCharacterItemRecord(Character.Id);
                        characterItem.Quantity = item.Quantity - quantity;
                        Character.Inventory.AddItem(characterItem);
                        item.Quantity = quantity;
                    }
                    else
                    {
                        CharacterItemRecord characterItem = Character.Inventory.GetItem(item.GId, item.Effects);

                        if (quantity <= (characterItem.Quantity + item.Quantity))
                        {
                            int newQuantity = (characterItem.Quantity + item.Quantity) - quantity;
                            Character.Inventory.RemoveItem(characterItem.UId, characterItem.Quantity - newQuantity);
                            item.Quantity = quantity;
                        }
                        else
                        {
                            Character.DisplaySystemMessage(true, 57);
                        }
                    }

                    OnItemQuantityChanged(item, quantity);
                }

                item.UpdateElement();
                Character.Client.Send(new ExchangeShopStockMovementUpdatedMessage(item.GetObjectItemToSell()));
            }
            else
            {
                Character.DisplaySystemMessage(true, 57);
            }
        }
        public int GetMerchantTax()
        {
            var resultTax = this.GetItems().Aggregate<MerchantItemRecord, double>(0, (current, item) => current + Math.Round((double)item.Price * item.Quantity));

            resultTax = (resultTax * 0.0001);

            if (resultTax > int.MaxValue)
                return int.MaxValue;

            int result = (int)resultTax;

            return result == 0 ? 1 : result;
        }
        public void Store(int objectUID, int quantity, long price)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(objectUID);

            if (item != null && item.Quantity >= quantity && price > 0 && item.CanBeExchanged())
            {
                if (price > Inventory.MaximumKamas)
                {
                    return;
                }
                MerchantItemRecord selledItem = item.ToMerchantItemRecord(Character.Id, price, quantity);
                selledItem.Quantity = quantity;
                Character.Inventory.RemoveItem(item.UId, quantity);
                Character.MerchantItems.AddItem(selledItem);
            }
        }
        public void TakeBack(int uid, int quantity)
        {
            MerchantItemRecord merchantItem = Character.MerchantItems.GetItem(uid);

            if (quantity < 0 && merchantItem != null && merchantItem.Quantity >= Math.Abs(quantity))
            {
                Character.Inventory.AddItem(merchantItem.ToCharacterItemRecord(Character.Id), Math.Abs(quantity));
                Character.MerchantItems.RemoveItem(merchantItem, -quantity);
            }
            else
            {
                Character.DisplaySystemMessage(true, 57);
            }

        }

        public override void OnItemAdded(MerchantItemRecord item)
        {
            item.AddElement();
            Character.Client.Send(new ExchangeShopStockMovementUpdatedMessage(item.GetObjectItemToSell()));
        }
        public override void OnItemQuantityChanged(MerchantItemRecord item, int quantity)
        {
            item.UpdateElement();
            Character.Client.Send(new ExchangeShopStockMovementUpdatedMessage(item.GetObjectItemToSell()));
        }
        public override void OnItemRemoved(MerchantItemRecord item)
        {
            item.RemoveElement();
            Character.Client.Send(new ExchangeShopStockMovementRemovedMessage(item.UId));
        }

    }
}
