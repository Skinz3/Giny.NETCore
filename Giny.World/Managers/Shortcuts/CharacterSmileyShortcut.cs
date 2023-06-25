using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Protocol.Types;
using ProtoBuf;

namespace Giny.World.Managers.Shortcuts
{
    [ProtoContract]
    public class CharacterSmileyShortcut : CharacterShortcut
    {
        [ProtoMember(5)]
        public short SmileyId
        {
            get;
            set;
        }
        public CharacterSmileyShortcut(short smileyId, byte slotId) : base(slotId)
        {
            this.SmileyId = smileyId;
        }
        public CharacterSmileyShortcut()
        {

        }

        public override Shortcut GetShortcut()
        {
            return new ShortcutSmiley()
            {
                slot = SlotId,
                smileyId = SmileyId
            };
        }
    }
}
