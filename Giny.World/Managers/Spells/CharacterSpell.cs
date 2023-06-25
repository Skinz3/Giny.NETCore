using Giny.Core.DesignPattern;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Spells;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Spells
{
    [ProtoContract]
    public class CharacterSpell
    {
        private SpellRecord m_record;

        private SpellRecord BaseRecord
        {
            get
            {
                if (m_record == null)
                {
                    m_record = SpellRecord.GetSpellRecord(SpellId);
                    return m_record;
                }
                else
                {
                    return m_record;
                }
            }
        }
        public SpellRecord VariantSpellRecord
        {
            get
            {
                return Variant ? BaseRecord : BaseRecord.VariantRecord;
            }
        }
        public SpellRecord ActiveSpellRecord
        {
            get
            {
                return Variant ? BaseRecord.VariantRecord : BaseRecord;
            }
        }
        [ProtoMember(1)]
        public short SpellId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public bool Variant
        {
            get;
            set;
        }
        public CharacterSpell(short spellId)
        {
            this.SpellId = spellId;
            this.Variant = Variant;
        }
        public CharacterSpell()
        {

        }
        public SpellItem GetSpellItem(Character character)
        {
            return new SpellItem(Variant ? BaseRecord.VariantRecord.Id : BaseRecord.Id, GetGrade(character));
        }
        public bool Learned(Character character)
        {
            return BaseRecord.MinimumLevel <= character.Level;
        }
        public byte GetGrade(Character character)
        {
           

            byte index = 0;

            for (byte i = 0; i < ActiveSpellRecord.Levels.Count; i++)
            {
                if (character.Level >= ActiveSpellRecord.Levels[i].MinPlayerLevel)
                {
                    index = i;
                }
                else
                {
                    break;
                }
            }
            return (byte)(index + 1);
        }


    }
}
