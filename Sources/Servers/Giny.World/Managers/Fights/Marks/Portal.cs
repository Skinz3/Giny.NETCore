using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
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
    public class Portal : Mark
    {
        public override bool InterceptMovement => Active;

        public override GameActionMarkTypeEnum Type => GameActionMarkTypeEnum.PORTAL;

        public Portal(int id, short bonusDamagePercent, EffectDice effect, Zone zone, MarkTriggerType triggers, Color color, Fighter source, CellRecord centerCell, Spell markSpell, Spell triggerSpell) : base(id, effect, zone, triggers, color, source, centerCell, markSpell, triggerSpell)
        {
            this.BonusDamagePercent = bonusDamagePercent;
        }
        public bool Active
        {
            get;
            private set;
        }
        public short BonusDamagePercent
        {
            get;
            set;
        }
        public override bool IsVisibleFor(CharacterFighter fighter)
        {
            return true;
        }

        public override void OnAdded()
        {
            Enable();
        }

        public void Enable()
        {
            this.Active = true;
            OnStateChanged();
        }
        public void Disable()
        {
            this.Active = false;
            OnStateChanged();
        }

        public void OnStateChanged()
        {
            Source.Fight.Send(new GameActionFightActivateGlyphTrapMessage()
            {
                actionId = (short)ActionsEnum.ACTION_FIGHT_DISABLE_PORTAL,
                active = Active,
                markId = (short)Id,
                sourceId = Source.Id,
            });
        }
        public override void OnRemoved()
        {

        }

        public override void Trigger(Fighter target, MarkTriggerType triggerType)
        {
            if (Active)
            {
                SpellRecord spellRecord = SpellRecord.GetSpellRecord(PortalManager.PortalTeleportSpellId);
                SpellLevelRecord spellLevel = spellRecord.Levels.Last();

                Spell spell = new Spell(spellRecord, spellLevel);

                SpellCast cast = new SpellCast(Source, spell, target.Cell);
                cast.Force = true;
                Source.CastSpell(cast);

                if (Active)
                {
                    Disable();
                }

            }

        }

        public override bool OnTurnBegin()
        {
            if (Source.Fight.IsCellFree(CenterCell))
            {
                Enable();
            }

            return false;
        }

        public override void OnUpdated()
        {
          
        }
    }
}
