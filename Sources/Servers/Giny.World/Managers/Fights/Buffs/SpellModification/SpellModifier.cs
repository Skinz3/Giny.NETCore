using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public enum SpellModifierUpdateResult
    {
        Ok,
        RequiresDeletion,
    }
    public class SpellModifier
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


        public SpellModifierTypeEnum Type
        {
            get;
            set;
        }

        public SpellModifierActionTypeEnum Action
        {
            get;
            private set;
        }

     

        public SpellModifier(short spellId, SpellModifierTypeEnum type, SpellModifierActionTypeEnum action)
        {
            this.SpellId = spellId;
            this.Type = type;
            this.Action = action;
        }
        /// <summary>
        /// Update the modifier
        /// </summary>
        /// <param name="value">delta</param>
        /// <returns>still valid</returns>

        public SpellModifierUpdateResult Update(short value)
        {
            var oldValue = this.Value;

            

            switch (Action)
            {
                case SpellModifierActionTypeEnum.ACTION_INVALID:
                    throw new InvalidOperationException("Invalid spell modifier action.");
                case SpellModifierActionTypeEnum.ACTION_BOOST:
                    Value += value;
                    break;
                case SpellModifierActionTypeEnum.ACTION_DEBOOST:
                    Value -= value;
                    break;
                case SpellModifierActionTypeEnum.ACTION_SET:
                    Value = value;
                    break;
                default:
                    break;
            }

            

            if (Action == SpellModifierActionTypeEnum.ACTION_SET && value == -oldValue)
            {
                return SpellModifierUpdateResult.RequiresDeletion;
            }


            if (Type == SpellModifierTypeEnum.LOS && Value == 1)
            {
                return SpellModifierUpdateResult.RequiresDeletion;
            }

            if ((Action == SpellModifierActionTypeEnum.ACTION_BOOST || Action == SpellModifierActionTypeEnum.ACTION_DEBOOST) && Value == 0)
            {
                return SpellModifierUpdateResult.RequiresDeletion;
            }

            return SpellModifierUpdateResult.Ok;
        }
        public SpellModifierMessage GetSpellModifierMessage()
        {
            return new SpellModifierMessage(SpellId, (byte)Action, (byte)Type, Value, 0);
        }


    }
}
