using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public abstract class SpellModifier
    {
        /// <summary>
        /// Value context
        /// </summary>
        public short Value
        {
            get;
            protected set;
        }

        public short SpellId
        {
            get;
            private set;
        }
        public abstract SpellModifierTypeEnum Type { get; }

        public abstract SpellModifierActionTypeEnum Update(short value);

        public abstract bool RequiresDeletion();

        public SpellModifier(short spellId)
        {
            this.SpellId = spellId;
        }


    }
}
