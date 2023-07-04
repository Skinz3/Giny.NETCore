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
        public CharacterSpellModificationTypeEnum Type
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
        public SpellModifier(CharacterSpellModificationTypeEnum type, short spellId, short value)
        {
            this.Type = type;
            this.Value = value;
            this.SpellId = spellId;
        }

        public CharacterSpellModification GetCharacterSpellModification()
        {
            return new CharacterSpellModification((byte)Type, this.SpellId, new CharacterCharacteristicDetailed(0, 0, 0, 0, Value, 0));
        }

        public void UpdateValue(short value)
        {
            switch (Type)
            {
                case CharacterSpellModificationTypeEnum.RANGEABLE:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.DAMAGE:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.BASE_DAMAGE:
                    Value += value;
                    break;
                case CharacterSpellModificationTypeEnum.HEAL_BONUS:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.AP_COST:
                    Value += value;
                    break;
                case CharacterSpellModificationTypeEnum.CAST_INTERVAL:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.CAST_INTERVAL_SET:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.CRITICAL_HIT_BONUS:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.CAST_LINE:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.LOS:
                    Value = value;
                    break;
                case CharacterSpellModificationTypeEnum.MAX_CAST_PER_TURN:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.MAX_CAST_PER_TARGET:
                    throw new NotImplementedException();
                case CharacterSpellModificationTypeEnum.RANGE_MAX:
                    Value += value;
                    break;
                case CharacterSpellModificationTypeEnum.RANGE_MIN:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
