using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Items;
using Giny.World.Records.Npcs;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges.Trades
{
    public class TradeRule
    {
        public double Rate
        {
            get;
            set;
        }
        public Dictionary<ItemRecord, ItemRecord> Items
        {
            get;
            set;
        } = new Dictionary<ItemRecord, ItemRecord>();
    }
    public class NpcTradeExchange : TradeExchange
    {
        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.NPC_TRADE;

        private Npc Npc
        {
            get;
            set;
        }
        private NpcActionRecord NpcAction
        {
            get;
            set;
        }

        private NpcTradeItemCollection Items
        {
            get;
            set;
        }


        public NpcTradeExchange(Character character, Npc npc, NpcActionRecord action) : base(character)
        {
            Npc = npc;
            NpcAction = action;
            Items = new NpcTradeItemCollection(character, BuildTradeRules());
        }

        private TradeRule BuildTradeRules()
        {
            var rule = new TradeRule();

            rule.Rate = double.Parse(NpcAction.Param2);

            var split = this.NpcAction.Param1.Split(',');

            foreach (var pair in split)
            {
                var itemSplit = pair.Split(':');
                var gid1 = itemSplit[0];
                var gid2 = itemSplit[1];

                var item1 = ItemRecord.GetItem(long.Parse(gid1));
                var item2 = ItemRecord.GetItem(long.Parse(gid2));

                rule.Items.Add(item1, item2);
            }

            return rule;
        }

        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void MoveItem(int uid, int quantity)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null && Items.CanMoveItem(item, quantity))
            {
                if (quantity > 0)
                {
                    if (item.Quantity >= quantity)
                    {
                        Items.AddItem(item, quantity);
                    }
                }
                else
                {
                    Items.RemoveItem(item.UId, Math.Abs(quantity));
                }
            }
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
            Character.Client.Send(new ExchangeStartOkNpcTradeMessage(Npc.Id));
        }

        public override void Ready(bool ready, short step)
        {
            Character.Client.Send(new ExchangeIsReadyMessage(Character.Id, true));

            var tradeItems = Items.GetTradeItems();
            var characterItems = Items.GetCharacterItems();

            foreach (var characterItem in characterItems)
            {
                Character.Inventory.RemoveItem(characterItem.UId, characterItem.Quantity);
            }

            foreach (var tradeItem in tradeItems)
            {
                tradeItem.CharacterId = Character.Id;
                Character.Inventory.AddItem(tradeItem);
            }

            this.Succes = true;
            this.Close();
        }

        public override IEnumerable<CharacterItemRecord> GetItems()
        {
            return Items.GetItems();
        }
    }
}
