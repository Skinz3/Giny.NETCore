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
    public class CharacterEmoteShortcut : CharacterShortcut
    {
        [ProtoMember(4)]
        public short EmoteId
        {
            get;
            set;
        }
        public CharacterEmoteShortcut(byte slotId, short emoteId) : base(slotId)
        {
            this.EmoteId = emoteId;
        }
        public CharacterEmoteShortcut()
        {

        }
        public override Shortcut GetShortcut()
        {
            return new ShortcutEmote()
            {
                slot = SlotId,
                emoteId = EmoteId,
            };
        }
    }
}
