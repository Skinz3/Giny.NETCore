using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Exchanges.Trades;
using Giny.World.Records.Items;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class NpcTradeItemCollection : TradeItemCollection
    {
        private Character Character
        {
            get;
            set;
        }
        private TradeRule Rule
        {
            get;
            set;
        }
        public NpcTradeItemCollection(Character character, TradeRule tradeRule)
        {
            this.Character = character;
            this.Rule = tradeRule;
        }

        public override void OnItemUnstacked(CharacterItemRecord item, int quantity)
        {
            DecrementalRule(item, quantity);

            OnObjectModified(item);
        }
        public override void OnItemStacked(CharacterItemRecord item, int quantity)
        {
            IncrementalRule(item, quantity);

            OnObjectModified(item);
        }

        private void DecrementalRule(CharacterItemRecord item, int quantity)
        {
            if (Rule.Items.ContainsKey(item.Record))
            {
                var tradeItemRecord = Rule.Items[item.Record];
                var qtyDiff = (int)(Rule.Rate * quantity);

                var tradeItem = GetFirstItem((short)tradeItemRecord.Id, qtyDiff);
                RemoveItem(tradeItem.UId, qtyDiff);
            }
        }
        private void IncrementalRule(CharacterItemRecord item, int quantity)
        {
            if (Rule.Items.ContainsKey(item.Record))
            {
                var tradeItemRecord = Rule.Items[item.Record];
                var qty = (int)(quantity * Rule.Rate);

                var tradeItem = ItemsManager.Instance.CreateCharacterItem(tradeItemRecord, -1, qty);
                AddItem(tradeItem);
            }
        }
        public override void OnItemRemoved(CharacterItemRecord item)
        {
            if (item.CharacterId != -1)
            {
                Character.Client.Send(new ExchangeObjectRemovedMessage()
                {
                    remote = false,
                    objectUID = item.UId
                });

                DecrementalRule(item, item.Quantity);

            }
            else
            {
                Character.Client.Send(new ExchangeObjectRemovedMessage()
                {
                    remote = true,
                    objectUID = item.UId
                });
            }

        }
        public override void OnItemAdded(CharacterItemRecord item)
        {
            if (item.CharacterId != -1)
            {
                Character.Client.Send(new ExchangeObjectAddedMessage()
                {
                    remote = false,
                    @object = item.GetObjectItem()
                });


                IncrementalRule(item, item.Quantity);
            }
            else
            {
                Character.Client.Send(new ExchangeObjectAddedMessage()
                {
                    remote = true,
                    @object = item.GetObjectItem()
                });
            }
        }

        private void OnObjectModified(CharacterItemRecord obj)
        {
            if (obj.CharacterId != -1)
            {
                Character.Client.Send(new ExchangeObjectModifiedMessage()
                {
                    remote = false,
                    @object = obj.GetObjectItem(),
                });


            }
            else
            {
                Character.Client.Send(new ExchangeObjectModifiedMessage()
                {
                    remote = true,
                    @object = obj.GetObjectItem(),
                });
            }

        }

        public List<CharacterItemRecord> GetCharacterItems()
        {
            return GetItems().Where(x => x.CharacterId != -1).ToList();
        }

        public List<CharacterItemRecord> GetTradeItems()
        {
            return GetItems().Where(x => x.CharacterId == -1).ToList();
        }
    }
}
