using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Exchanges.Trades;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges
{
    public class PlayerTradeExchange : TradeExchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.PLAYER_TRADE;
            }
        }

        private PlayerTradeItemCollection Items
        {
            get;
            set;
        }

        private bool IsReady = false;

        private long MovedKamas = 0;


        public Character Target
        {
            get;
            set;
        }

        public PlayerTradeExchange(Character character, Character target)
            : base(character)
        {
            this.Items = new PlayerTradeItemCollection(character, target);
            this.Target = target;
        }

        public override void Open()
        {
            this.Send(new ExchangeStartedWithPodsMessage()
            {
                exchangeType = (byte)ExchangeType,
                firstCharacterId = Character.Id,
                firstCharacterCurrentWeight = Character.Inventory.CurrentWeight,
                firstCharacterMaxWeight = Character.Inventory.TotalWeight,
                secondCharacterId = Target.Id,
                secondCharacterCurrentWeight = Target.Inventory.CurrentWeight,
                secondCharacterMaxWeight = Target.Inventory.TotalWeight,
            });
        }

        public void Send(NetworkMessage message)
        {
            Character.Client.Send(message);
            Target.Client.Send(message);
        }
        public override void Close()
        {
            Target.Client.Send(new ExchangeLeaveMessage()
            {
                dialogType = (byte)DialogType,
                success = Succes
            });

            Target.Dialog = null;

            Character.Client.Send(new ExchangeLeaveMessage()
            {
                dialogType = (byte)DialogType,
                success = Succes
            });

            Character.Dialog = null;
        }

      

        public override void MoveItem(int uid, int quantity)
        {
            if (!IsReady)
            {
                CharacterItemRecord item = Character.Inventory.GetItem(uid);

                if (item != null && Items.CanMoveItem(item, quantity))
                {
                    if (Target.GetDialog<PlayerTradeExchange>().IsReady)
                    {
                        Target.GetDialog<PlayerTradeExchange>().Ready(false, 0);
                    }
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
        }

        public override void Ready(bool ready, short step)
        {
            this.IsReady = ready;

            Send(new ExchangeIsReadyMessage(Character.Id, this.IsReady));

            if (this.IsReady && Target.GetDialog<PlayerTradeExchange>().IsReady)
            {
                foreach (var item in Items.GetItems())
                {
                    item.CharacterId = Target.Id;
                    Target.Inventory.AddItem((CharacterItemRecord)item.CloneWithoutUID());
                    Character.Inventory.RemoveItem(item.UId, item.Quantity);
                }

                foreach (var item in Target.GetDialog<PlayerTradeExchange>().Items.GetItems())
                {
                    item.CharacterId = Character.Id;
                    Character.Inventory.AddItem((CharacterItemRecord)item.CloneWithoutUID());
                    Target.Inventory.RemoveItem(item.UId, item.Quantity);
                }

                Target.AddKamas(MovedKamas);
                Character.RemoveKamas(MovedKamas);

                Character.AddKamas(Target.GetDialog<PlayerTradeExchange>().MovedKamas);
                Target.RemoveKamas(Target.GetDialog<PlayerTradeExchange>().MovedKamas);

                this.Succes = true;
                this.Close();
            }
        }

        public override void MoveKamas(long quantity)
        {
            if (quantity <= Character.Record.Kamas)
            {
                if (IsReady)
                {
                    Ready(false, 0);
                }
                if (Target.GetDialog<PlayerTradeExchange>().IsReady)
                {
                    Target.GetDialog<PlayerTradeExchange>().Ready(false, 0);
                }

                Character.Client.Send(new ExchangeKamaModifiedMessage()
                {
                    remote = false,
                    quantity = quantity
                });
                Target.Client.Send(new ExchangeKamaModifiedMessage()
                {
                    remote = true,
                    quantity = quantity
                });

                MovedKamas = quantity;
            }


        }

        public override void OnNpcGenericAction(NpcActionsEnum action)
        {
            throw new NotImplementedException();
        }

        public override void MoveItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CharacterItemRecord> GetItems()
        {
            return Items.GetItems();
        }
    }
}