using Giny.ORM;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items.Collections
{
    public class BankItemCollection : ItemCollection<BankItemRecord>
    {
        private Character Character
        {
            get;
            set;
        }
        public BankItemCollection(Character character, IEnumerable<BankItemRecord> items) : base(items)
        {
            this.Character = character;
        }
        public override void OnItemUnstacked(BankItemRecord item, int quantity)
        {
            item.UpdateElement();
            Character.Client.Send(new StorageObjectUpdateMessage(item.GetObjectItem()));
        }
        public override void OnItemStacked(BankItemRecord item, int quantity)
        {
            item.UpdateElement();
            Character.Client.Send(new StorageObjectUpdateMessage(item.GetObjectItem()));
        }
        public override void OnItemRemoved(BankItemRecord item)
        {
            item.RemoveElement();
            Character.Client.Send(new StorageObjectRemoveMessage(item.UId));
        }
        public override void OnItemAdded(BankItemRecord item)
        {
            item.AddElement();
            Character.Client.Send(new StorageObjectUpdateMessage(item.GetObjectItem()));
        }
    }
}
