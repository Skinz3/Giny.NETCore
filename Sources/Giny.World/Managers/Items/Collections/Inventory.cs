using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Api;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Formulas;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class Inventory : ItemCollection<CharacterItemRecord>
    {
        public const long MaximumKamas = 2000000000;

        public const EffectsEnum ItemCastEffect = EffectsEnum.Effect_CastSpell_1175;

        public static CharacterInventoryPositionEnum[] DofusPositions = new CharacterInventoryPositionEnum[]
        {
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_1,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_2,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_3,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_4,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_5,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_6,
        };

        public static CharacterInventoryPositionEnum[] RingPositions = new CharacterInventoryPositionEnum[]
        {
            CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_LEFT,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_RIGHT,
        };

        private Character Character
        {
            get;
            set;
        }

        public int CurrentWeight
        {
            get
            {
                return 0;
                /*
                int currWeight = 0;
                foreach (CharacterItemRecord item in GetItems().ToList())
                {
                    currWeight += ItemRecord.GetItem(item.GId).RealWeight * item.Quantity;
                }
                return currWeight; */
            }
        }



        public bool HasWeaponEquiped
        {
            get
            {
                return GetWeapon() != null;
            }
        }
        public int TotalWeight
        {
            get
            {
                return 1000;
                //return StatsFormulas.Instance.TotalWeight(Character);
            }
        }

        public Inventory(Character character, IEnumerable<CharacterItemRecord> items)
            : base(items)
        {
            this.Character = character;
        }

        public IEnumerable<CharacterItemRecord> GetSpellCastItems()
        {
            foreach (var item in GetEquipedItems())
            {
                if (item.Effects.Exists(ItemCastEffect))
                {
                    yield return item;
                }
            }
        }

        public override void OnItemsQuantityChanged(IEnumerable<CharacterItemRecord> items)
        {
            foreach (var item in items)
            {
                item.UpdateElement();
            }

            Character.Client.Send(new ObjectsQuantityMessage(Array.ConvertAll(items.ToArray(), x => x.GetObjectItemQuantity())));
            RefreshWeight();
        }

        public override void OnItemQuantityChanged(CharacterItemRecord item, int quantity)
        {
            item.UpdateElement();
            UpdateItemQuantity(item);
            RefreshWeight();
        }

        public override void OnItemsRemoved(IEnumerable<CharacterItemRecord> items)
        {
            foreach (var item in items)
            {
                item.RemoveElement();
                Character.GeneralShortcutBar.OnItemRemoved(item);
            }
            Character.Client.Send(new ObjectsDeletedMessage(Array.ConvertAll(items.ToArray(), x => x.UId)));
            RefreshWeight();
            Character.RefreshShortcuts();
        }

        public override void OnItemsAdded(IEnumerable<CharacterItemRecord> items)
        {
            foreach (var item in items)
            {
                item.UId = ItemsManager.Instance.PopItemUID();
                item.AddElement();
                Character.OnItemAdded(item);
            }
            Character.Client.Send(new ObjectsAddedMessage(Array.ConvertAll(items.ToArray(), x => x.GetObjectItem())));
            RefreshWeight();
        }

        public override void OnItemRemoved(CharacterItemRecord item)
        {
            item.RemoveElement();
            Character.Client.Send(new ObjectDeletedMessage(item.UId));
            Character.GeneralShortcutBar.OnItemRemoved(item);
            RefreshWeight();
            Character.OnItemAdded(item);
        }

        [WIP]
        public override void OnItemAdded(CharacterItemRecord item)
        {
            item.UId = ItemsManager.Instance.PopItemUID();
            item.AddElement();
            Character.Client.Send(new ObjectAddedMessage(item.GetObjectItem(), 0)); // 0??????
            RefreshWeight();
        }


        public void UnequipAll()
        {
            foreach (var item in GetEquipedItems())
            {
                SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
            }
        }
        public bool Unequip(CharacterInventoryPositionEnum position)
        {
            var item = GetEquipedItem(position);

            if (item != null)
            {
                SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
                return true;
            }
            else
            {
                return false;
            }

        }
        public CharacterItemRecord AddItem(short gid, int quantity, bool perfect = false)
        {
            var template = ItemRecord.GetItem(gid);
            if (template != null)
            {
                var obj = ItemsManager.Instance.CreateCharacterItem(template, Character.Id, quantity, perfect);
                base.AddItem(obj);
                return obj;
            }
            else
                return null;

        }
        public CharacterItemRecord GetFirstItem(ItemTypeEnum type)
        {
            return GetItems().FirstOrDefault(x => x.Record.TypeEnum == type);
        }
        public CharacterItemRecord GetFirstItem(short gid, int minimumQuantity)
        {
            return GetItems().FirstOrDefault(x => x.GId == gid && x.Quantity >= minimumQuantity);
        }
        public void Refresh()
        {
            Character.Client.Send(new InventoryContentMessage(this.GetObjectsItems(), Character.Record.Kamas));
            RefreshWeight();
        }

        public void RefreshWeight()
        {
            Character.Client.Send(new InventoryWeightMessage(CurrentWeight, 0, TotalWeight));
        }
        public void RefreshKamas()
        {
            Character.Client.Send(new KamasUpdateMessage(Character.Record.Kamas));
        }
        protected override CharacterItemRecord GetSameItem(short gid, EffectCollection effects)
        {
            var items = GetItems();
            return items.FirstOrDefault(x => x.GId == gid && effects.SequenceEqual(x.Effects) && x.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
        }
        public CharacterItemRecord GetWeapon()
        {
            return GetEquipedItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);
        }
        private void OnItemMoved(CharacterItemRecord item, CharacterInventoryPositionEnum lastPosition)
        {
            bool flag = lastPosition != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
            bool flag2 = item.IsEquiped();


            if (flag2 && !flag)
                ItemEffectsManager.Instance.AddEffects(Character, item.Effects);

            if (!flag2 && flag)
                ItemEffectsManager.Instance.RemoveEffects(Character, item.Effects);

            UpdateLook(item, flag2);

            if (item.Record.HasSet)
            {
                int num = CountItemSetEquiped(item.Record.ItemSet);

                if (flag2)
                {
                    ApplyItemSetEffects(item.Record.ItemSet, num, flag2);
                }
                else
                {
                    ApplyItemSetEffects(item.Record.ItemSet, num, flag2);
                }
            }
        }
        private void UpdateLook(CharacterItemRecord item, bool equiped)
        {
            switch (item.Record.TypeEnum)
            {
                case ItemTypeEnum.PET:
                    if (item.Look != string.Empty)
                        UpdatePetLook(item, equiped);
                    break;
                case ItemTypeEnum.PETSMOUNT:
                    if (item.Look != string.Empty)
                        UpdatePetMountLook(item, equiped);
                    break;
                default:
                    if (item.AppearanceId != 0)
                    {
                        if (equiped)
                            Character.Look.AddSkin(item.AppearanceId);
                        else
                            Character.Look.RemoveSkin(item.AppearanceId);
                    }
                    break;

            }
        }
        private void UpdatePetLook(CharacterItemRecord item, bool equiped)
        {
            if (equiped)
            {
                Character.Look = EntityLookManager.Instance.CreatePetLook(Character, item.Look);
            }
            else
            {
                Character.Look.RemoveFirstSubentity(x => x.Category == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_PET);
            }
        }
        private void UpdatePetMountLook(CharacterItemRecord item, bool equiped)
        {
            if (equiped)
            {
                Character.Look = EntityLookManager.Instance.CreatePetMountLook(Character, item.Look);
            }
            else
            {
                if (Character.Look.IsRiding)
                {
                    Character.Look = Character.Look.ActorLook;
                    Character.Look.SetBones(1);
                }
                else
                {
                    Logger.Write("Unable to remove pet mount look of player " + Character.Name, Channels.Warning);
                }
            }
        }
        public void OnItemModified(CharacterItemRecord item)
        {
            Character.Client.Send(new ObjectModifiedMessage(item.GetObjectItem()));
        }
        private void ApplyItemSetEffects(ItemSetRecord itemSet, int count, bool equiped)
        {
            if (equiped)
            {
                if (count >= 2)
                {
                    if (count >= 3)
                    {
                        ItemEffectsManager.Instance.RemoveEffects(Character, itemSet.GetEffects(count - 1));
                    }

                    ItemEffectsManager.Instance.AddEffects(Character, itemSet.GetEffects(count));

                    OnSetUpdated(itemSet, count);
                }
            }
            else
            {
                if (count >= 1)
                {
                    ItemEffectsManager.Instance.RemoveEffects(Character, itemSet.GetEffects(count + 1));

                    if (count >= 2)
                    {
                        ItemEffectsManager.Instance.AddEffects(Character, itemSet.GetEffects(count));
                    }

                    OnSetUpdated(itemSet, count);
                }

            }
        }

        private void OnSetUpdated(ItemSetRecord set, int num)
        {
            Character.Client.Send(new SetUpdateMessage()
            {
                setEffects = set.GetEffects(num).GetObjectEffects(),
                setId = (short)set.Id,
                setObjects = set.Items.ToArray(),
            });
        }
        private void EquipItem(CharacterItemRecord item, CharacterInventoryPositionEnum position, int quantity)
        {
            CharacterItemRecord equiped = GetEquipedItem(position);

            CharacterInventoryPositionEnum lastPosition = item.PositionEnum;

            if (equiped != null)
            {
                UnequipItem(equiped, quantity);
                OnObjectMoved(equiped, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            }

            if (item.Quantity == 1)
            {
                item.PositionEnum = position;
            }
            else
            {
                CharacterItemRecord newItem = ItemsManager.Instance.CutItem(item, quantity, position);
                AddItem(newItem);
                UpdateItemQuantity(item);
            }

            item.UpdateElement();

            OnItemMoved(item, lastPosition);

            foreach (var item2 in GetEquipedItems())
            {
                if (item != item2 && !CriteriaManager.Instance.EvaluateCriterias(Character.Client, item2.Record.Criteria))
                    SetItemPosition(item2.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item2.Quantity);
            }

            InventoryEventApi.ItemEquipped(Character, item);

        }
        /// <summary>
        /// Permet de déséquiper un item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        private void UnequipItem(CharacterItemRecord item, int quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                CharacterItemRecord sameItem = GetSameItem(item.GId, item.Effects);
                CharacterInventoryPositionEnum lastPosition = item.PositionEnum;

                if (sameItem != null)
                {
                    if (item.UId != sameItem.UId)
                    {
                        item.PositionEnum = CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
                        sameItem.Quantity += quantity;
                        sameItem.UpdateElement();
                        this.UpdateItemQuantity(sameItem);
                        this.RemoveItem(item.UId, item.Quantity);
                    }
                    else
                    {
                        Character.ReplyError("Error while moving item");
                    }

                }
                else
                {
                    item.PositionEnum = CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
                    item.UpdateElement();
                }

                OnItemMoved(item, lastPosition);

                InventoryEventApi.ItemUnequipped(Character, item);
            }
        }
        bool CheckStacks(CharacterItemRecord item, CharacterInventoryPositionEnum position, CharacterInventoryPositionEnum[] checker)
        {
            foreach (CharacterInventoryPositionEnum pos in checker)
            {
                var current = GetEquipedItem(pos);
                if (current != null && current.GId == item.GId)
                    return true;
            }
            return false;
        }


        private int CountItemSetEquiped(ItemSetRecord itemSet)
        {
            return this.GetEquipedItems().Count((CharacterItemRecord entry) => entry.Record.HasSet && entry.Record.ItemSetId == itemSet.Id);
        }
        public int MaximumItemSetCount()
        {
            int max = 0;

            foreach (var item in GetEquipedItems())
            {
                ItemSetRecord itemSet = item.Record.ItemSet;

                if (itemSet != null)
                {
                    int current = CountItemSetEquiped(itemSet);

                    if (current > max)
                    {
                        max = current;
                    }
                }
            }
            return max - 1;
        }
        public CharacterItemRecord[] GetEquipedItems()
        {
            return GetItems().Where(x => x.IsEquiped()).ToArray();
        }
        private CharacterItemRecord GetEquipedItem(CharacterInventoryPositionEnum position)
        {
            return GetItems().FirstOrDefault(x => x.PositionEnum == position);
        }
        private void UpdateItemQuantity(CharacterItemRecord item)
        {
            Character.Client.Send(new ObjectQuantityMessage(item.UId, item.Quantity, 0)); // 0 ?
        }
        void OnError(ObjectErrorEnum error)
        {
            Character.Client.Send(new ObjectErrorMessage((byte)error));
        }
        void OnLivingObjectEquipedDirectly()
        {
            Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 161);
        }
        public void SetItemPosition(int uid, CharacterInventoryPositionEnum position, int quantity)
        {
            var item = GetItem(uid);
            SetItemPosition(item, position, quantity);
        }
        private void SetItemPosition(CharacterItemRecord item, CharacterInventoryPositionEnum position, int quantity)
        {
            if (item == null)
            {
                return;
            }

            if (item.Record.TypeEnum == ItemTypeEnum.LIVING_OBJECT || item.Record.IsCeremonialItem())
            {
                if (Character.Fighting)
                {
                    return;
                }
            }

            var oldPosition = item.PositionEnum;

            if (position != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                if (Character.Level < item.Record.Level)
                {
                    OnError(ObjectErrorEnum.LEVEL_TOO_LOW);
                    return;
                }
                if (!CriteriaManager.Instance.EvaluateCriterias(Character.Client, item.Record.Criteria))
                {
                    OnError(ObjectErrorEnum.CRITERIONS);
                    return;
                }
                if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED && DofusPositions.Contains((CharacterInventoryPositionEnum)item.Position) && DofusPositions.Contains((CharacterInventoryPositionEnum)position))
                    return;

                if (CheckStacks(item, position, RingPositions) && item.Record.HasSet)
                {
                    OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                    return;
                }
                if (CheckStacks(item, position, DofusPositions) && item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                    return;
                }

                if (!InventoryEventApi.OnItemEquipping(Character, item))
                {
                    return;
                }

                if (item.Record.TypeEnum == ItemTypeEnum.LIVING_OBJECT)
                {
                    ItemTypeEnum livingObjectCategory = (ItemTypeEnum)item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_LivingObjectCategory).Value;

                    var targeted = GetEquipedItem(position);

                    if (targeted == null)
                    {
                        OnLivingObjectEquipedDirectly();
                        return;
                    }
                    if (targeted.Record.TypeEnum != livingObjectCategory)
                    {
                        OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                        return;
                    }
                    if (targeted.Effects.IsAssociated)
                    {
                        OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                        return;
                    }
                    if (item.Quantity > 1)
                    {
                        CharacterItemRecord newItem = ItemsManager.Instance.CutItem(item, 1, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                        LivingObjectManager.Instance.AssociateLivingObject(Character, item, targeted);
                        AddItem(newItem);
                        item.UpdateElement();
                        UpdateItemQuantity(item);
                        return;
                    }
                    else
                    {
                        LivingObjectManager.Instance.AssociateLivingObject(Character, item, targeted);
                        return;
                    }
                }
                if (item.Record.IsCeremonialItem())
                {
                    var targeted = GetEquipedItem(position);

                    if (targeted == null)
                    {
                        OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                        return;
                    }
                    if (targeted.Record.TypeId != item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_Compatible).Value)
                    {
                        return;
                    }
                    if (targeted.Effects.IsAssociated)
                    {
                        OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                        return;
                    }
                    if (item.Quantity > 1)
                    {
                        CharacterItemRecord newItem = ItemsManager.Instance.CutItem(item, 1, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                        this.Associate(item, targeted);
                        AddItem(newItem);
                        UpdateItemQuantity(item);
                        item.UpdateElement();

                    }
                    else
                    {
                        this.Associate(item, targeted);
                    }

                    return;
                }

                EquipItem(item, position, quantity);
            }
            else
            {
                if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                    return;
                }
                else
                {
                    UnequipItem(item, quantity);
                }

            }

            item.UpdateElement();
            OnObjectMoved(item, position);
            RefreshWeight();
            Character.Record.UpdateElement();
            Character.RefreshActorOnMap();
            Character.RefreshStats();

        }
        private void Associate(CharacterItemRecord item, CharacterItemRecord targeted)
        {
            RemoveItem(item, item.Quantity);

            if (targeted.Record.TypeEnum == ItemTypeEnum.PETSMOUNT || targeted.Record.TypeEnum == ItemTypeEnum.PET)
            {
                UpdateItemLook(targeted, item.Look);
            }
            else
            {
                UpdateItemAppearence(targeted, item.AppearanceId);
            }
            targeted.Effects.Add(new EffectInteger(EffectsEnum.Effect_Apparence_Wrapper, (short)item.Record.Id));
            targeted.UpdateElement();
            OnItemModified(targeted);
            RefreshWeight();
            Character.RefreshActorOnMap();
            Character.RefreshStats();
            Character.Client.Send(new WrapperObjectAssociatedMessage()
            {
                hostUID = targeted.UId,
            });

        }
        public void Dissociate(CharacterItemRecord item, CharacterInventoryPositionEnum pos)
        {
            EffectInteger effect = item.Effects.GetFirst<EffectInteger>(EffectsEnum.Effect_Apparence_Wrapper);

            if (effect != null)
            {
                AddItem((short)effect.Value, item.Quantity);

                if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    if (item.Record.TypeEnum == ItemTypeEnum.PETSMOUNT || item.Record.TypeEnum == ItemTypeEnum.PET)
                    {
                        UpdateItemLook(item, item.Record.Look);
                    }
                    else
                    {
                        UpdateItemAppearence(item, item.Record.AppearenceId);
                    }
                }
                else
                {
                    if (item.Record.TypeEnum == ItemTypeEnum.PETSMOUNT || item.Record.TypeEnum == ItemTypeEnum.PET)
                    {
                        item.Look = item.Record.Look;
                    }
                    else
                    {
                        item.AppearanceId = item.Record.AppearenceId;
                    }
                }

                item.Effects.RemoveAll(EffectsEnum.Effect_Apparence_Wrapper);

                if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    if (!CheckPassiveStacking(item))
                        OnItemModified(item);
                }
                else
                {
                    OnItemModified(item);
                }
                item.UpdateElement();

                RefreshWeight();
                Character.RefreshActorOnMap();
                Character.RefreshStats();
            }

        }

        public void UpdateItemAppearence(CharacterItemRecord item, short newApparenceId)
        {
            UpdateLook(item, false);
            item.AppearanceId = newApparenceId;
            UpdateLook(item, true);
        }
        public void UpdateItemLook(CharacterItemRecord item, string newLook)
        {
            UpdateLook(item, false);
            item.Look = newLook;
            UpdateLook(item, true);
        }
        public bool CheckPassiveStacking(CharacterItemRecord item)
        {
            var item2 = GetItems().FirstOrDefault(x => x.UId != item.UId && x.GId == item.GId && item.Effects.SequenceEqual(x.Effects) && x.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

            if (item2 != null)
            {
                item2.Quantity += item.Quantity;
                item2.UpdateElement();
                OnItemModified(item2);
                RemoveItem(item, item.Quantity);
                return true;

            }
            return false;
        }

        private void OnObjectMoved(CharacterItemRecord item, CharacterInventoryPositionEnum newPosition)
        {
            Character.Client.Send(new ObjectMovementMessage(item.UId, (byte)newPosition));
        }

        /*  public void DropItem(uint uid, uint quantity)
          {

              CharacterItemRecord item = GetItem(uid);
              if (item != null && item.CanBeExchanged() && item.Quantity >= quantity && item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
              {
                  ushort targetCell = Character.Map.CloseCellForDropItem(Character.CellId);

                  if (targetCell != 0)
                  {
                      this.RemoveItem(item, quantity);
                      this.Character.Map.Instance.AddDroppedItem(item, (ushort)quantity, targetCell);
                  }
              }
          } */
    }
}