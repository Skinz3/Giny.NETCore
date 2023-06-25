using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges
{
    public class BuySellExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.NPC_SHOP;
            }
        }

        public Npc Npc
        {
            get;
            set;
        }

        public ItemRecord[] ItemToSell
        {
            get;
            set;
        }

        public short TokenId
        {
            get;
            set;
        }

        public BuySellExchange(Character character, Npc npc, ItemRecord[] itemToSell, short tokenId)
            : base(character)
        {
            this.Npc = npc;
            this.ItemToSell = itemToSell;
            this.TokenId = tokenId;
        }

        public override void Open()
        {
            ObjectItemToSellInNpcShop[] items = Array.ConvertAll<ItemRecord, ObjectItemToSellInNpcShop>(ItemToSell, x => x.GetObjectItemToSellInNpcShop());
            Character.Client.Send(new ExchangeStartOkNpcShopMessage(Npc.Id, TokenId, items));
        }

        public void Buy(short gid, int quantity)
        {
            ItemRecord template = ItemToSell.FirstOrDefault(x => x.Id == gid);

            if (template != null)
            {
                if (this.TokenId == 0)
                {
                    int cost = (int)(template.Price * quantity);

                    if (!Character.RemoveKamas(cost))
                        return;
                }
                else
                {
                    CharacterItemRecord tokenItem = Character.Client.Character.Inventory.GetFirstItem(TokenId, (int)(template.Price * quantity));

                    if (tokenItem == null)
                    {
                        Character.Client.Character.ReplyError("Vous ne possedez pas asser de token.");
                        return;
                    }
                    else
                    {
                        Character.Inventory.RemoveItem(tokenItem.UId, (int)(quantity * template.Price));
                    }
                }

                Character.Inventory.AddItem(gid, quantity, TokenId != 0);
                Character.Client.Send(new ExchangeBuyOkMessage());
            }

        }
        public void Sell(int uid, int quantity)
        {
            
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null && item.CanBeExchanged() && item.Quantity >= quantity)
            {
                int gained = (int)((item.Record.Price / 10) * quantity);

                if (gained >= item.Record.Price * quantity)
                {
                    return;
                }

                gained = gained == 0 ? 1 : gained;

                Character.Inventory.RemoveItem(uid, quantity);

                if (TokenId == 0)
                {
                    Character.AddKamas(gained);
                }
                else
                {
                    Character.Inventory.AddItem(TokenId, gained);
                }
                Character.Client.Send(new ExchangeSellOkMessage());
            }
        }
        public override void MoveItem(int uid, int quantity)
        {
            throw new NotImplementedException();
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
            
        }

        public override void MoveItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }
    }
}
