using Giny.Core.Collections;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.History
{
    public class SpellHistory
    {
        public const int HistoryEntriesLimit = 60;

        private readonly LimitedStack<SpellHistoryEntry> m_underlyingStack = new LimitedStack<SpellHistoryEntry>(HistoryEntriesLimit);

        public Fighter Owner
        {
            get;
            private set;
        }
        public int CurrentRound
        {
            get
            {
                return (int)this.Owner.Fight.RoundNumber;
            }
        }
        public SpellHistory(Fighter owner)
        {
            this.Owner = owner;
        }
        public void RegisterCastedSpell(SpellHistoryEntry entry)
        {
            this.m_underlyingStack.Push(entry);
        }
        public void RegisterCastedSpell(SpellLevelRecord spell, Fighter target)
        {
            this.RegisterCastedSpell(new SpellHistoryEntry(this, spell, this.Owner, target, this.CurrentRound));
        }
        public bool CanCastSpell(SpellLevelRecord spell, CellRecord targetedCell)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.Id == spell.Id);
            bool result;
            if (spellHistoryEntry == null && (long)this.CurrentRound < (long)((ulong)spell.InitialCooldown))
            {
                result = false;
            }
            else if (spellHistoryEntry == null)
            {
                result = true;
            }
            else if (spellHistoryEntry.IsGlobalCooldownActive())
            {
                result = false;
            }
            else
            {
                SpellHistoryEntry[] array = (
                    from entry in this.m_underlyingStack
                    where entry.Spell.Id == spell.Id && entry.CastRound == this.CurrentRound
                    select entry).ToArray();
                if (array.Length == 0)
                {
                    result = true;
                }
                else if (spell.MaxCastPerTurn > 0u && (long)array.Length >= (long)((ulong)spell.MaxCastPerTurn))
                {
                    result = false;
                }
                else
                {
                    Fighter target = this.Owner.Fight.GetFighter(targetedCell.Id);

                    if (target == null)
                    {
                        result = true;
                    }
                    else
                    {
                        int num = array.Count((SpellHistoryEntry entry) => entry.Target != null && entry.Target.Id == target.Id);
                        result = (spell.MaxCastPerTarget <= 0u || (long)num < (long)((ulong)spell.MaxCastPerTarget));
                    }
                }
            }
            return result;
        }
        public bool CanCastSpell(SpellLevelRecord spell)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.Id == spell.Id);
            bool result;
            if (spellHistoryEntry == null && (long)this.CurrentRound < (long)((ulong)spell.InitialCooldown))
            {
                result = false;
            }
            else if (spellHistoryEntry == null)
            {
                result = true;
            }
            else if (spellHistoryEntry.IsGlobalCooldownActive())
            {
                result = false;
            }
            else
            {
                SpellHistoryEntry[] array = (
                    from entry in this.m_underlyingStack
                    where entry.Spell.Id == spell.Id && entry.CastRound == this.CurrentRound
                    select entry).ToArray<SpellHistoryEntry>();
                result = (array.Length == 0 || spell.MaxCastPerTurn <= 0u || (long)array.Length < (long)((ulong)spell.MaxCastPerTurn));
            }
            return result;
        }

        public int ReduceSpellCooldown(short spellId, short delta)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.SpellId == spellId);

            if (spellHistoryEntry == null)
            {
                return 0;
            }
            else
            {
                spellHistoryEntry.CastRound -= delta;
                int newCooldown = spellHistoryEntry.GetCooldown();

                if (newCooldown >= 0)
                {
                    return newCooldown;
                }
                else
                {
                    return 0;
                }
            }
        }

        public GameFightSpellCooldown[] GetSpellCooldowns()
        {
            List<GameFightSpellCooldown> result = new List<GameFightSpellCooldown>();

            foreach (var entry in this.m_underlyingStack.Reverse())
            {
                byte cooldown = (byte)entry.GetCooldown();

                if (!result.Any(x => x.spellId == entry.Spell.SpellId) && cooldown > 0)
                {
                    result.Add(new GameFightSpellCooldown()
                    {
                        cooldown = cooldown,
                        spellId = entry.Spell.SpellId,
                    });
                }
            }

            return result.ToArray();
        }

        public void SetSpellCooldown(short spellId, short value)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.SpellId == spellId);

            if (spellHistoryEntry != null)
            {
                spellHistoryEntry.Cooldown = value;
            }
        }
    }
}
