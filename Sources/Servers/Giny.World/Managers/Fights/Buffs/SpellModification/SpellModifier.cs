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



        public void UpdateValue(short value)
        {
            switch (Type)
            {
                case SpellModifierTypeEnum.INVALID_MODIFICATION:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.RANGEABLE:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.DAMAGE:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.BASE_DAMAGE:
                    Value += value;
                    break;
                case SpellModifierTypeEnum.HEAL_BONUS:
                    break;
                case SpellModifierTypeEnum.AP_COST:
                    Value += value;
                    break;
                case SpellModifierTypeEnum.CAST_INTERVAL:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.CRITICAL_HIT_BONUS:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.CAST_LINE:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.LOS:
                    Value = value;
                    break;
                case SpellModifierTypeEnum.MAX_CAST_PER_TURN:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.MAX_CAST_PER_TARGET:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.RANGE_MAX:
                    Value += value;
                    break;
                case SpellModifierTypeEnum.RANGE_MIN:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.OCCUPIED_CELL:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.FREE_CELL:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.VISIBLE_TARGET:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.PORTAL_FREE_CELL:
                    throw new NotImplementedException();
                case SpellModifierTypeEnum.PORTAL_PROJECTION:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }

        }

        public SpellModifierMessage GetSpellModifierMessage()
        {
            return new SpellModifierMessage(SpellId, (byte)SpellModifierActionTypeEnum.ACTION_BOOST, (byte)Type, Value, 0);
        }
    }
}
