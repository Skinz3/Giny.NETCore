using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Shortcuts
{
    [ProtoContract]
    public class CharacterItemShortcut : CharacterShortcut
    {
        public CharacterItemShortcut(byte slotId, int itemUId, short itemGId) : base(slotId)
        {
            this.ItemUId = itemUId;
            this.ItemGId = itemGId;
        }
        public CharacterItemShortcut()
        {

        }
        [ProtoMember(2)]
        public int ItemUId
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public short ItemGId
        {
            get;
            set;
        }
        public override Shortcut GetShortcut()
        {
            return new ShortcutObjectItem()
            {
                slot = SlotId,
                itemGID = ItemGId,
                itemUID = ItemUId,
            };
        }
    }
}
