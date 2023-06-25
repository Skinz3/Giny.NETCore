using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.Network.Messages;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Exchanges;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;

namespace Giny.World.Managers.Entities.Merchants
{
    public class CharacterMerchant : Entity
    {
        public const short BAG_SKIN = 237;

        private List<MerchantSellerExchange> Exchanges
        {
            get;
            set;
        }
        private MerchantRecord Record
        {
            get;
            set;
        }

        public MerchantSellerItemCollection Items
        {
            get;
            set;
        }

        public CharacterMerchant(MerchantRecord record, MapRecord map) : base(map)
        {
            this.Record = record;

            ServerEntityLook bagLook = new ServerEntityLook();
            bagLook.SetBones(BAG_SKIN);
            Look.RemoveAura();
            Look.SubEntities.Add(new ServerSubentityLook(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MERCHANT_BAG, 0, bagLook));

            IEnumerable<MerchantItemRecord> items = MerchantItemRecord.GetMerchantItems(this.Record.CharacterId);
            this.Items = new MerchantSellerItemCollection(this, items);
            this.Exchanges = new List<MerchantSellerExchange>();
        }

        public override long Id => Record.CharacterId;

        public override string Name => Record.Name;

        public override short CellId
        {
            get => Record.CellId;
            set => throw new NotImplementedException();
        }
        public override DirectionsEnum Direction
        {
            get => Record.Direction;
            set => throw new NotImplementedException();
        }
        public override ServerEntityLook Look
        {
            get => Record.Look;
            set => throw new NotImplementedException();
        }

        public void AddExchange(MerchantSellerExchange exchange)
        {
            lock (Exchanges)
            {
                Exchanges.Add(exchange);
            }
        }
        public void RemoveExchange(MerchantSellerExchange exchange)
        {
            lock (Exchanges)
            {
                Exchanges.Remove(exchange);
            }
        }
        public void SendExchangers(NetworkMessage message)
        {
            lock (Exchanges)
            {
                foreach (var exchange in Exchanges)
                {
                    exchange.Character.Client.Send(message);
                }
            }
        }

        public override GameRolePlayActorInformations GetActorInformations()
        {
            return new GameRolePlayMerchantInformations()
            {
                contextualId = Id,
                sellType = 0,
                disposition = new EntityDispositionInformations(CellId, (byte)Direction),
                look = Look.ToEntityLook(),
                name = Name,
                options = new HumanOption[0],
            };
        }

        public void Remove()
        {
            lock (this)
            {
                Map.Instance.RemoveEntity(this.Id);
                Record.RemoveElement();
            }
        }
    }
}
