using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Sequences;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Spells;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    public abstract class Mark
    {
        public abstract bool InterceptMovement
        {
            get;
        }
        public abstract GameActionMarkTypeEnum Type
        {
            get;
        }
        private bool Active
        {
            get;
            set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public int Id
        {
            get;
            private set;
        }
        public CellRecord CenterCell
        {
            get;
            private set;
        }
        protected CellRecord? TargetCell
        {
            get;
            set;
        } = null;

        public Spell MarkSpell
        {
            get;
            private set;
        }
        protected Spell TriggerSpell
        {
            get;
            private set;
        }
        protected Effect Effect
        {
            get;
            private set;
        }
        protected Color Color
        {
            get;
            set;
        }
        private List<MarkShape> Shapes
        {
            get;
            set;
        }
        public CellRecord[] Cells
        {
            get;
            private set;
        }
        public MarkTriggerType Triggers
        {
            get;
            private set;
        }

        public Mark(int id, EffectDice effect, Zone zone, MarkTriggerType triggers, Color color, Fighter source, CellRecord centerCell, Spell markSpell, Spell triggerSpell, CellRecord? targetCell = null)

        {
            this.Id = id;
            this.Color = color;
            this.Triggers = triggers;
            this.Source = source;
            this.CenterCell = centerCell;
            this.Effect = effect;
            this.MarkSpell = markSpell;
            this.TriggerSpell = triggerSpell;
            this.Active = true;
            this.TargetCell = targetCell;
            this.Build(zone);
        }

        /// <summary>
        /// Return true = remove mark
        /// </summary>
        public abstract bool OnTurnBegin();

        private void Build(Zone zone)
        {
            this.Cells = zone.GetCells(CenterCell, TargetCell == null ? CenterCell : TargetCell, Source.Fight.Map);
            BuildShapes();

        }
        private void BuildShapes()
        {
            Shapes = new List<MarkShape>();

            for (int i = 0; i < Cells.Length; i++)
            {
                Shapes.Add(new MarkShape(Source.Fight, Cells[i], Color));
            }
        }

        public void UpdateCells(CellRecord centerCell, IEnumerable<CellRecord> cells)
        {
            this.CenterCell = centerCell;
            this.Cells = cells.ToArray();
            this.BuildShapes();
            Source.Fight.UpdateMark(this);
        }
        protected virtual Fighter GetTriggerCastSource()
        {
            return Source;
        }
        public GameActionMark GetGameActionMark()
        {
            return new GameActionMark()
            {
                active = Active,
                markAuthorId = Source.Id,
                cells = Shapes.Select(x => x.GetGameActionMarkedCell()).ToArray(),
                markId = (short)Id,
                markimpactCell = CenterCell.Id,
                markSpellId = MarkSpell.Record.Id,
                markSpellLevel = MarkSpell.Level.Grade,
                markTeamId = (byte)Source.Team.TeamId,
                markType = (byte)Type,
            };
        }
        public GameActionMark GetHiddenGameActionMark()
        {
            return new GameActionMark()
            {
                active = Active,
                markAuthorId = Source.Id,
                cells = new GameActionMarkedCell[0],
                markId = (short)Id,
                markimpactCell = -1,
                markSpellId = MarkSpell.Record.Id,
                markSpellLevel = MarkSpell.Level.Grade,
                markTeamId = (byte)Source.Team.TeamId,
                markType = (byte)Type,
            };
        }

        public abstract bool IsVisibleFor(CharacterFighter fighter);

        public abstract void OnAdded();

        public abstract void OnRemoved();

        public abstract void OnUpdated();

        public virtual bool ShouldTriggerOnMove(Fighter target, short oldCellId, short cellId)
        {
            return ContainsCell(cellId);
        }

        protected SpellCast CreateSpellCast(CellRecord castCell)
        {
            SpellCast cast = new SpellCast(GetTriggerCastSource(), TriggerSpell, CenterCell);
            cast.CastCell = castCell;
            cast.MarkSource = this;
            return cast;
        }
        protected SpellCast CreateSpellCast()
        {
            return CreateSpellCast(CenterCell);
        }

        public void ApplyEffects()
        {
            ApplyEffects(CenterCell);
        }
        public void ApplyEffects(CellRecord castCell)
        {
            SpellCast cast = CreateSpellCast();
            cast.CastCell = castCell;
            cast.Force = true;
            GetTriggerCastSource().CastSpell(cast);
        }
        protected void ApplyEffects(Fighter fighter)
        {
            SpellCastHandler castHandler = SpellManager.Instance.CreateSpellCastHandler(CreateSpellCast());

            if (!castHandler.Initialize())
            {
                return;
            }

            foreach (var effectHandler in castHandler.GetEffectHandlers())
            {
                IEnumerable<Fighter> targets = effectHandler.IsValidTarget(fighter) ? new Fighter[] { fighter } : new Fighter[0];
                effectHandler.Execute(targets);
            }
        }
        protected void RemoveEffects(Fighter fighter)
        {
            using (fighter.Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP))
            {
                var buffs = fighter.GetBuffs(false).Where(x => x.Cast.Spell.Level.Id == this.TriggerSpell.Level.Id);

                foreach (var buff in buffs.ToArray())
                {
                    fighter.RemoveAndDispellBuff(Source, buff);
                }
            }

        }

        public bool ContainsCell(short cellId)
        {
            return Cells.Any(x => x.Id == cellId);
        }

        public abstract void Trigger(Fighter target, MarkTriggerType triggerType, ITriggerToken? token);

    }
}
