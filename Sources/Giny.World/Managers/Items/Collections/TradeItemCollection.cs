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
    public class TradeItemCollection : ItemCollection<CharacterItemRecord>
    {
        private Character Character
        {
            get;
            set;
        }
        private Character Target
        {
            get;
            set;
        }
        public TradeItemCollection(Character character, Character target)
        {
            this.Character = character;
            this.Target = target;
        }

        public override void OnItemUnstacked(CharacterItemRecord item, int quantity)
        {
            OnObjectModified(item);
        }
        public override void OnItemStacked(CharacterItemRecord item, int quantity)
        {
            OnObjectModified(item);
        }
        public override void OnItemRemoved(CharacterItemRecord item)
        {
            Character.Client.Send(new ExchangeObjectRemovedMessage()
            {
                remote = false,
                objectUID = item.UId
            });
            Target.Client.Send(new ExchangeObjectRemovedMessage
            {
                remote = true,
                objectUID = item.UId
            });
        }
        public override void OnItemAdded(CharacterItemRecord item)
        {
            Character.Client.Send(new ExchangeObjectAddedMessage()
            {
                remote = false,
                @object = item.GetObjectItem()
            });
            Target.Client.Send(new ExchangeObjectAddedMessage()
            {
                remote = true,
                @object = item.GetObjectItem()
            });
        }
        private void OnObjectModified(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectModifiedMessage()
            {
                remote = false,
                @object = obj.GetObjectItem(),
            });
            Target.Client.Send(new ExchangeObjectModifiedMessage()
            {
                remote = true,
                @object = obj.GetObjectItem(),
            });
        }
    }
}
