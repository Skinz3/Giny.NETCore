using Giny.Protocol.Enums;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class TradeItemCollection : ItemCollection<CharacterItemRecord>
    {
        public bool CanMoveItem(CharacterItemRecord item, int quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED || !item.CanBeExchanged())
                return false;

            CharacterItemRecord exchanged = null;

            exchanged = this.GetItem(item.GId, item.Effects);

            if (exchanged != null && exchanged.UId != item.UId)
                return false;

            exchanged = this.GetItem(item.UId);

            if (exchanged == null)
            {
                return true;
            }

            if (exchanged.Quantity + quantity > item.Quantity)
                return false;
            else
                return true;
        }
    }
}
