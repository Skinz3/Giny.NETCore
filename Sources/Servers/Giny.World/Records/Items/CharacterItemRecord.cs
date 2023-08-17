using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.ORM;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [Table("character_items", false)]
    public class CharacterItemRecord : AbstractItem, IRecord
    {
        [Update]
        public long CharacterId
        {
            get;
            set;
        }

        long IRecord.Id => base.UId;

        public CharacterItemRecord(long characterId, int uid, int gid, byte position, int quantity, EffectCollection effects, short appearanceId, string look)
        {
            this.UId = uid;
            this.GId = gid;
            this.Position = position;
            this.CharacterId = characterId;
            this.Quantity = quantity;
            this.Effects = effects;
            this.AppearanceId = appearanceId;
            this.Look = look;
        }


        public CharacterItemRecord()
        {

        }


        public bool IsEquiped()
        {
            return PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
        }

        public ObjectItemNotInContainer GetObjectItemNotInContainer()
        {
            return new ObjectItemNotInContainer(GId, Effects.GetObjectEffects(), UId, Quantity);
        }

        public CharacterItemRecord ToMimicry(ItemRecord foodItem)
        {
            CharacterItemRecord newItem = (CharacterItemRecord)this.CloneWithoutUID();
            newItem.Effects.Add(new EffectInteger(EffectsEnum.Effect_Appearance, (ushort)foodItem.Id));
            newItem.Quantity = 1;
            newItem.AppearanceId = foodItem.AppearenceId;
            newItem.Look = foodItem.Look;
            return newItem;
        }
        public void EraseMimicry()
        {
            this.Effects.RemoveAll(EffectsEnum.Effect_Appearance);
            this.AppearanceId = Record.AppearenceId;
            this.Look = Record.Look;
        }

        public static List<CharacterItemRecord> GetCharacterItems(long characterId)
        {
            return DatabaseReader.Select<CharacterItemRecord>("CharacterId", characterId).ToList();
        }


        public override string ToString()
        {
            return "(" + UId + ") " + Record.Name;
        }
        public bool CanBeExchanged()
        {
            return Record.Exchangeable;
        }

        /// <summary>
        /// Return the maximum item UID
        /// Use this method wisely, direct SQL query
        /// </summary>
        public static int GetLastItemUID()
        {
            return (int)TableManager.Instance.GetLastIdFromQuery<CharacterItemRecord>();
        }
        public override AbstractItem CloneWithUID()
        {
            return new CharacterItemRecord(CharacterId, UId, GId, Position, Quantity, this.Effects.Clone(), AppearanceId, Look); /* shouldnt we clone each effects? */
        }
        public override AbstractItem CloneWithoutUID()
        {
            return new CharacterItemRecord(CharacterId, ItemsManager.Instance.PopItemUID(), GId, Position, Quantity, this.Effects.Clone(), AppearanceId, Look);
        }

        public static void RemoveCharacterItems(long characterId)
        {
            DatabaseWriter.Delete<CharacterItemRecord>("CharacterId", characterId);
        }

        public override void Initialize()
        {
            switch (Record.TypeEnum)
            {
                case ItemTypeEnum.LIVING_OBJECT:
                    LivingObjectManager.Instance.InitializeLivingObject(this);
                    break;
            }
        }
        public void Feed(Character character, ObjectItemQuantity[] meal)
        {
            if (this.Effects.Exists(EffectsEnum.Effect_LivingObjectId))
            {
                LivingObjectManager.Instance.FeedLivingObject(character, this, meal);
            }
        }


    }
}
