using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.History
{
    public class SpellHistoryEntry
    {
        public SpellHistory History
        {
            get;
            private set;
        }
        public SpellLevelRecord Spell
        {
            get;
            private set;
        }
        public Fighter Caster
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public int CastRound
        {
            get;
            set;
        }
        public int Cooldown
        {
            get;
            set;
        }
        public SpellHistoryEntry(SpellHistory history, SpellLevelRecord spell, Fighter caster, Fighter target, int castRound)
        {
            this.History = history;
            this.Spell = spell;
            this.Caster = caster;
            this.Target = target;
            this.CastRound = castRound;
            this.Cooldown = this.Spell.MinCastInterval;
        }
        public int GetElapsedRounds()
        {
            return History.CurrentRound - this.CastRound;
        }
        public int GetCooldown()
        {
            return Cooldown - GetElapsedRounds();
        }
        public bool IsGlobalCooldownActive()
        {
            int elapsedRounds = this.GetElapsedRounds();
            return elapsedRounds < Cooldown;
        }
        public override string ToString()
        {
            return Spell.ToString();
        }
    }
}
