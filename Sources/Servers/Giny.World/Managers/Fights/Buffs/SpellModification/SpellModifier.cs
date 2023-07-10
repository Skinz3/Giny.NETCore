using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModifiers
{
    public class SpellModifier
    {
        public SpellModifierTypeEnum Type
        {
            get;
            private set;
        }
        public short Value
        {
            get;
            private set;
        }
        public short SpellId
        {
            get;
            private set;
        }
        public SpellModifier(SpellModifierTypeEnum type, short spellId, short value)
        {
            this.Type = type;
            this.Value = value;
            this.SpellId = spellId;
        }


        public SpellModifierActionTypeEnum GetModifierActionTypeEnum()
        {
            switch (Type)
            {
                case SpellModifierTypeEnum.INVALID_MODIFICATION:
                    break;
                case SpellModifierTypeEnum.RANGEABLE:
                    break;
                case SpellModifierTypeEnum.DAMAGE:
                    break;

                case SpellModifierTypeEnum.RANGE_MAX:
                case SpellModifierTypeEnum.AP_COST:
                case SpellModifierTypeEnum.BASE_DAMAGE:
                    return SpellModifierActionTypeEnum.ACTION_BOOST;


                case SpellModifierTypeEnum.HEAL_BONUS:
                    break;
                case SpellModifierTypeEnum.CAST_INTERVAL:
                    break;
                case SpellModifierTypeEnum.CRITICAL_HIT_BONUS:
                    break;
                case SpellModifierTypeEnum.CAST_LINE:
                    break;


                case SpellModifierTypeEnum.LOS:
                    return SpellModifierActionTypeEnum.ACTION_SET;


                case SpellModifierTypeEnum.MAX_CAST_PER_TURN:
                    break;
                case SpellModifierTypeEnum.MAX_CAST_PER_TARGET:
                    break;

                case SpellModifierTypeEnum.RANGE_MIN:
                    break;
                case SpellModifierTypeEnum.OCCUPIED_CELL:
                    break;
                case SpellModifierTypeEnum.FREE_CELL:
                    break;
                case SpellModifierTypeEnum.VISIBLE_TARGET:
                    break;
                case SpellModifierTypeEnum.PORTAL_FREE_CELL:
                    break;
                case SpellModifierTypeEnum.PORTAL_PROJECTION:
                    break;
                default:
                    break;
            }

            return SpellModifierActionTypeEnum.ACTION_INVALID;
        }
        public void UpdateValue(short value)
        {
            var actionType = GetModifierActionTypeEnum();

            switch (actionType)
            {
                case SpellModifierActionTypeEnum.ACTION_INVALID:
                    break;
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

        }

        public SpellModifierMessage GetSpellModifierMessage()
        {
            var type = GetModifierActionTypeEnum();
            return new SpellModifierMessage(SpellId, (byte)type, (byte)Type, Value, 0);
        }

    }
}
