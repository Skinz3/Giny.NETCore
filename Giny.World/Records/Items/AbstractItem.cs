using Giny.ORM.Attributes;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    public abstract class AbstractItem
    {
        [Ignore]
        private ItemRecord m_record;

        [Ignore]
        public ItemRecord Record
        {
            get
            {
                if (m_record == null)
                {
                    m_record = ItemRecord.GetItem(GId);
                    return m_record;
                }
                else
                {
                    return m_record;
                }
            }
        }

        [Primary]
        public int UId
        {
            get;
            set;
        }

        public short GId
        {
            get;
            set;
        }

        [Update]
        public byte Position
        {
            get;
            set;
        }

        [Ignore]
        public CharacterInventoryPositionEnum PositionEnum
        {
            get
            {
                return (CharacterInventoryPositionEnum)Position;
            }
            set
            {
                Position = (byte)value;
            }
        }

        [Update]
        public int Quantity
        {
            get;
            set;
        }

        [ProtoSerialize, Update]
        public EffectCollection Effects
        {
            get;
            set;
        }

        [Update]
        public short AppearanceId
        {
            get;
            set;
        }
        [Update]
        public string Look
        {
            get;
            set;
        }

        
        public ObjectItem GetObjectItem()
        {
            return new ObjectItem(Position, GId, Effects.GetObjectEffects(), UId, Quantity);
        }
        public ObjectItemQuantity GetObjectItemQuantity()
        {
            return new ObjectItemQuantity(UId, Quantity);
        }

        public abstract AbstractItem CloneWithUID();

        public abstract AbstractItem CloneWithoutUID();

        public abstract void Initialize();

        public MerchantItemRecord ToMerchantItemRecord(long characterId, long price, int quantity)
        {
            return new MerchantItemRecord()
            {
                CharacterId = characterId,
                UId = UId,
                AppearanceId = AppearanceId,
                Effects = Effects.Clone(), /* Clone each effects */
                GId = GId,
                Look = Look,
                Position = Position,
                Quantity = quantity,
                Price = price,
                QuantitySold = 0,
            };
        }
        public CharacterItemRecord ToCharacterItemRecord(long characterId)
        {
            return new CharacterItemRecord()
            {
                CharacterId = characterId,
                UId = UId,
                AppearanceId = AppearanceId,
                Effects = Effects.Clone(), /* Clone each effects */
                GId = GId,
                Look = Look,
                Position = Position,
                Quantity = Quantity,
            };
        }
        public BankItemRecord ToBankItemRecord(int accountId)
        {
            return new BankItemRecord()
            {
                AccountId = accountId,
                UId = UId,
                AppearanceId = AppearanceId,
                Effects = Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = Position,
                Quantity = Quantity,
            };
        }
        public BidShopItemRecord ToBidShopItemRecord(long bidshopId, int accountId, long price)
        {
            return new BidShopItemRecord()
            {
                AccountId = accountId,
                AppearanceId = AppearanceId,
                BidShopId = bidshopId,
                Effects = Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = Position,
                Price = price,
                Quantity = Quantity,
                UId = ItemsManager.Instance.PopItemUID(),
                Sold = false,
            };
        }

    }
}
