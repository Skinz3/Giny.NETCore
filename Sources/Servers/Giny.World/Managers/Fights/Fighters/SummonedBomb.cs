using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Movements;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Fighters
{
    public class SummonedBomb : SummonedMonster
    {
        private SpellBombRecord? SpellBomb
        {
            get;
            set;
        }

        private bool Triggered
        {
            get;
            set;
        }
        public SummonedBomb(Fighter owner, MonsterRecord record, SpellEffectHandler summoningEffect, byte gradeId, CellRecord cell) : base(owner, record, summoningEffect, gradeId, cell)
        {
            this.SpellBomb = SpellBombRecord.GetSpellBomb(record.Id);
        }
        public override void OnSummoned()
        {
            base.OnSummoned();
            WallManager.Instance.UpdateWalls(this.Fight);
        }

        public override void TriggerMovementBuffs(Movement movement)
        {
            base.TriggerMovementBuffs(movement);
            // WallManager.Instance.UpdateWalls(this.Fight);
        }

        private IEnumerable<Wall> GetWalls()
        {
            return Fight.GetMarks<Wall>().Where(x => x.IsWallMember(this));
        }


        public void Trigger(Fighter source)
        {
            if (!AliveSafe)
            {
                return;
            }

            if (this.Triggered)
            {
                return;
            }

            this.Triggered = true;

            var spellGrade = GetSummoningEffect().CastHandler.Cast.Spell.Level.Grade;

            var walls = GetWalls();

            this.ExecuteSpell(SpellBomb.ExplodSpellId, spellGrade, this.Cell);

            foreach (var wall in walls)
            {
                var otherBomb = wall.GetPair(this);

                otherBomb.ExecuteSpell(SpellBomb.ExplodSpellId, spellGrade, otherBomb.Cell);
            }

            this.ExecuteSpell(SpellBomb.ChainReactionSpellId, spellGrade, this.Cell);


        }
        public override void OnDie(Fighter killedBy)
        {
            base.OnDie(killedBy);
            WallManager.Instance.UpdateWalls(this.Fight);
        }
        public override bool CanPlay()
        {
            return false;
        }
        public override bool DisplayInTimeline()
        {
            return false;
        }

        public int GetTotalComboBonus()
        {
            var result = this.Stats[CharacteristicEnum.BOMB_COMBO_BONUS].TotalInContext();

            foreach (var wall in GetWalls())
            {
                var pair = wall.GetPair(this);

                if (pair != null)
                {
                    result += pair.Stats[CharacteristicEnum.BOMB_COMBO_BONUS].TotalInContext();
                }
            }

            return result;
        }
    }
}
