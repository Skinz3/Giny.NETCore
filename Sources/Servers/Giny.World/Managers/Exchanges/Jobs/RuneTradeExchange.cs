using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges.Jobs
{
    public class RuneTradeExchange : Exchange
    {
        private JobItemCollection Items
        {
            get;
            set;
        }
        public RuneTradeExchange(Character character) : base(character)
        {
            this.Items = new JobItemCollection(character);
        }

        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.RUNES_TRADE;


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
            Character.Client.Send(new ExchangeStartOkRunesTradeMessage());
        }
        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }
        public override void Ready(bool ready, short step)
        {
            AsyncRandom random = new AsyncRandom();

            Dictionary<CharacterItemRecord, BasicItemCollection> results = new Dictionary<CharacterItemRecord, BasicItemCollection>();

            foreach (var item in Items.GetItems())
            {
                results.Add(item, new BasicItemCollection());

                for (int i = 0; i < item.Quantity; i++)
                {
                    results[item].AddItems(DecraftItem(random, item));
                }

            }

            OnResulted(results);

            foreach (var result in results.Values)
            {
                Character.Inventory.AddItems(result.GetItems());
            }

            foreach (var item in Items.GetItems())
            {
                Character.Inventory.RemoveItem(item.UId, item.Quantity);
            }

            Items.Clear();
        }



        private List<CharacterItemRecord> DecraftItem(AsyncRandom random, CharacterItemRecord item)
        {
            List<CharacterItemRecord> results = new List<CharacterItemRecord>();

            List<ItemRecord> runeItems = new List<ItemRecord>();

            foreach (EffectInteger effect in item.Effects)
            {
                var runes = ItemsManager.Instance.GetRunesItem(effect.EffectEnum);

                foreach (var rune in runes)
                {
                    var maxQuantity = (Math.Pow(item.Record.Level, 1.2d)) / 5;

                    if (maxQuantity > 0)
                    {
                        var quantity = random.Next(0, (int)maxQuantity);

                        CharacterItemRecord runeItem = ItemsManager.Instance.CreateCharacterItem(rune, Character.Id, quantity);
                        results.Add(runeItem);
                    }
                }
            }


            return results;
        }
        public override void MoveItem(int uid, int quantity)
        {
            Items.MoveItem(uid, quantity);
        }

        private void OnResulted(Dictionary<CharacterItemRecord, BasicItemCollection> results)
        {
            List<DecraftedItemStackInfo> decraftedItems = new List<DecraftedItemStackInfo>();

            foreach (var result in results)
            {
                decraftedItems.Add(new DecraftedItemStackInfo()
                {
                    objectUID = result.Key.UId,
                    bonusMax = 1,
                    bonusMin = 1,
                    runesId = result.Value.GetItems().Select(x => x.GId).ToArray(),
                    runesQty = result.Value.GetItems().Select(x => x.Quantity).ToArray(),
                });
            }


            Character.Client.Send(new DecraftResultMessage()
            {
                results = decraftedItems.ToArray(),
            });
        }

    }
}
