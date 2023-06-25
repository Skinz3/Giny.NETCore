using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones;
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
    public class Rune : Mark
    {
        public int Duration
        {
            get;
            set;
        }
        public Rune(int id, EffectDice effect, Zone zone, MarkTriggerType triggers, Color color, Fighter source, CellRecord centerCell, SpellRecord spellRecord, SpellLevelRecord spellLevel) : base(id, effect, zone, triggers, color, source, centerCell, spellRecord, spellLevel)
        {
            this.Duration = effect.Duration;
        }

        public override bool StopMovement => false;

        public override GameActionMarkTypeEnum Type => GameActionMarkTypeEnum.RUNE;

        public override bool IsVisibleFor(CharacterFighter fighter)
        {
            return true;
        }

        public override void OnAdded()
        {

        }

        public override void OnRemoved()
        {

        }

        public override void Trigger(Fighter target, MarkTriggerType triggerType)
        {
            Source.Fight.RemoveMark(this);
            ApplyEffects();
        }

        public bool DecrementDuration()
        {
            return this.Duration != -1 && (this.Duration -= 1) <= 0;
        }
    }
}
