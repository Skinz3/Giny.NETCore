using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public class SpellCast
    {
        public Fighter Source
        {
            get;
            private set;
        }
        public short SpellId
        {
            get
            {
                return Spell.Record.Id;
            }
        }
        public Spell Spell
        {
            get;
            set;
        }

        public Fighter Target
        {
            get;
            set;
        }

        CellRecord m_targetCell;

        public CellRecord TargetCell
        {
            get
            {
                if (Target != null)
                {
                    return Target.Cell;
                }
                return m_targetCell;
            }
            set
            {
                m_targetCell = value;
            }
        }
        public bool Force
        {
            get;
            set;
        }
        public CellRecord CastCell
        {
            get;
            set;
        }
        public SpellCastResult[] BypassedConditions
        {
            get;
            set;
        }
        public FightSpellCastCriticalEnum Critical
        {
            get;
            set;
        }
        public bool IsCriticalHit => Critical == FightSpellCastCriticalEnum.CRITICAL_HIT;

        public bool ApFree => Force;

        public bool Silent
        {
            get;
            set;
        }
        public bool Weapon
        {
            get;
            set;
        }
        public ITriggerToken Token
        {
            get;
            set;
        }
        private SpellCast Parent
        {
            get;
            set;
        }
        private List<SpellCast> Childs
        {
            get;
            set;
        }
        public Mark MarkSource
        {
            get;
            set;
        }
        public SpellCast(Fighter source, Spell spell, CellRecord targetCell, SpellCast parent = null)
        {
            if (parent != null)
            {
                this.Parent = parent;
                this.Parent.AddChild(this);
            }

            this.Source = source;
            this.Spell = spell;
            this.CastCell = source.Cell;
            this.TargetCell = targetCell;
            this.Force = false;
            this.Silent = false;
            this.Weapon = false;
            this.Childs = new List<SpellCast>();
        }

        public bool IsConditionBypassed(SpellCastResult result) => BypassedConditions != null && (BypassedConditions.Contains(result) || BypassedConditions.Contains(SpellCastResult.OK));

        private void AddChild(SpellCast child)
        {
            this.Childs.Add(child);
        }

        public Fighter GetCaster()
        {
            Fighter source = this.Source;

            SpellCast current = this;

            while (current.Parent != null)
            {
                current = current.Parent;
                source = current.Source;
            }
            return source;
        }

        public int GetDeep()
        {
            int deep = 0;

            SpellCast current = this;

            while (current.Parent != null)
            {
                current = current.Parent;
                deep++;
            }

            return deep;
        }

        public IEnumerable<SpellCast> GetAllChilds()
        {
            List<SpellCast> childs = new List<SpellCast>();

            foreach (var child in this.Childs)
            {
                childs.Add(child);

                childs.AddRange(child.GetAllChilds());
            }

            return childs;
        }


        public SpellCast GetParent()
        {
            return Parent;
        }

        public override string ToString()
        {
            return Spell.Record.Name;
        }

    }
}
