using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public class Spell
    {
        public SpellRecord Record
        {
            get;
            set;
        }
        public SpellLevelRecord Level
        {
            get;
            set;
        }

        public short SpellId
        {
            get
            {
                return Record.Id;
            }
        }

        public Spell(SpellRecord record, SpellLevelRecord level)
        {
            this.Record = record;
            this.Level = level;
        }
        public Spell(SpellRecord record, byte grade)
        {
            this.Record = record;
            this.Level = record.GetLevel(grade);
        }

        public override string ToString()
        {
            return Record.Name;
        }



    }
}
