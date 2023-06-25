using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Bidshops;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Network;
using Giny.World.Records.Bidshops;
using Giny.World.Records.Items;

namespace Giny.World.Managers.Exchanges
{
    public class BuyExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.BIDHOUSE_BUY;

        private BidShopRecord BidShop
        {
            get;
            set;
        }

        private IEnumerable<BidShopItemRecord> Items
        {
            get
            {
                return BidshopsManager.Instance.GetItems(BidShop.Id);
            }
        }


        private short GIdWatched
        {
            get;
            set;
        }
        private ItemTypeEnum TypeWatched
        {
            get;
            set;
        }

        public BuyExchange(Character character, BidShopRecord bidShop) : base(character)
        {
            this.BidShop = bidShop;
        }
        public void ShowTypes(short type)
        {
            TypeWatched = (ItemTypeEnum)type;
            GIdWatched = 0;

            var gids = GetGIDs(TypeWatched);
            Character.Client.Send(new ExchangeTypesExchangerDescriptionForUserMessage(type, gids));
        }
        public void ShowList(short gid)
        {
            GIdWatched = gid;

            var items = SortedItems(GetItemsByGId(gid));

            Character.Client.Send(new ExchangeTypesItemsExchangerDescriptionForUserMessage()
            {
                objectType = (int)TypeWatched,
                itemTypeDescriptions = items,
            });

        }
        [WIP("Some bug concerning UID Quantities ...?")]
        public void Buy(int uid, int quantity, long price)
        {
            bool bought = false;

            BidShopItemRecord item = BidshopsManager.Instance.GetItem(BidShop.Id, uid);

            if (item != null)
            {
                if (item.Price == price)
                {
                    if (Character.RemoveKamas(price))
                    {
                        bought = true;
                        Character.Inventory.AddItem(item.ToCharacterItemRecord(Character.Id));
                        this.OnItemBuy(item);

                        OnItemRemoved(uid);

                        if (Items.Count(x => x.GId == item.GId) == 0)
                        {
                            OnGIdRemoved(item.GId);
                        }

                    }
                    else
                    {
                        Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 82);
                    }
                }
            }
            else
            {
                Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 64);

                if (Items.Count(x => x.GId == GIdWatched) == 0)
                {
                    OnGIdRemoved(GIdWatched);
                }

                OnItemRemoved(uid);
            }

            OnBuy(uid, bought);

        }
        private void OnBuy(int uid, bool bought)
        {
            Character.Client.Send(new ExchangeBidHouseBuyResultMessage()
            {
                bought = bought,
                uid = uid
            });
        }
        private void OnGIdRemoved(short gid)
        {
            Character.Client.Send(new ExchangeBidHouseGenericItemRemovedMessage()
            {
                objGenericId = gid
            }); ;
        }
        private void OnItemRemoved(int uid)
        {
            Character.Client.Send(new ExchangeBidHouseInListRemovedMessage()
            {
                itemUID = uid,
                objectGID = GIdWatched,
                objectType = (short)TypeWatched,
            });
        }
        private IEnumerable<BidShopItemRecord> GetItemsByGId(short gid)
        {
            return Items.Where(x => x.GId == gid);
        }

        private BidExchangerObjectInfo[] SortedItems(IEnumerable<BidShopItemRecord> items)
        {
            List<BidExchangerObjectInfo> result = new List<BidExchangerObjectInfo>();

            foreach (var itemsData in ItemCollection<BidShopItemRecord>.SortByEffects(items).Keys)
            {
                for (int i = 0; i < BidShop.Quantities.Count; i++)
                {
                    BidShopItemRecord item = itemsData.Where(x => x.Quantity == BidShop.Quantities[i]).OrderBy(x => x.Price).ToList().FirstOrDefault();

                    if (item != null)
                    {
                        result.Add(item.GetBidExchangerObjectInfo(BuildPrices(item, i)));
                    }

                }
            }
            return result.ToArray();
        }
        private long[] BuildPrices(BidShopItemRecord item, int quantityIndex)
        {
            List<long> prices = new List<long>();

            for (int i = 0; i < quantityIndex; i++)
            {
                prices.Add(0);
            }

            prices.Add(item.Price);

            return prices.ToArray();
        }
        private int[] GetGIDs(ItemTypeEnum type)
        {
            var items = Items.Where(x => x.Record.TypeEnum == type);
            return items.Select(x => (int)x.GId).Distinct().ToArray();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Open()
        {
            Character.Client.Send(new ExchangeStartedBidBuyerMessage(BidShop.GetBuyerDescriptor()));
        }
        public void OnItemBuy(BidShopItemRecord item)
        {
            var client = WorldServer.Instance.GetOnlineClient(x => x.Account.Id == item.AccountId);

            if (client != null)
            {
                client.WorldAccount.BankKamas += item.Price;
                client.WorldAccount.UpdateElement();
                client.Character.NotifyItemSelled(item.GId, item.Quantity, item.Price);
                BidshopsManager.Instance.RemoveItem(BidShop.Id, item);
            }
            else
            {
                item.Sold = true;
                item.UpdateElement();
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
            if (action == NpcActionsEnum.BUY)
            {
                this.Close();
                this.Character.OpenBuyExchange(BidShop);
            }
            else if (action == NpcActionsEnum.SELL || action == NpcActionsEnum.SELL2)
            {
                this.Close();
                Character.OpenSellExchange(BidShop);
            }
            else
            {
                throw new NotImplementedException();
            }
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
