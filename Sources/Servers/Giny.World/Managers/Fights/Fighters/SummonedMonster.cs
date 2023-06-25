using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Monsters;
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
    public class SummonedMonster : SummonedFighter, IMonster
    {
        public MonsterRecord Record
        {
            get;
            private set;
        }
        public MonsterGrade Grade
        {
            get;
            private set;
        }

        public override string Name => Record.Name;

        public override short Level => Grade.Level;

        public SummonedMonster(Fighter owner, MonsterRecord record, SpellEffectHandler summoningEffect, byte gradeId, CellRecord cell) : base(owner, summoningEffect, cell)
        {
            this.Record = record;
            this.Grade = Record.GetGrade(gradeId);
        }
        public override void Initialize()
        {
            double statsCoeff = 1 + (Summoner.Level / 100);
            this.Stats = new FighterStats(Grade, statsCoeff);
            this.Look = Record.Look.Clone();
            base.Initialize();
        }

        public override bool CanBePushed()
        {
            return base.CanBePushed() && Record.CanBePushed;
        }
        public override bool CanTackle()
        {
            return base.CanTackle() && Record.CanTackle;
        }
        public override bool CanSwitchPosition()
        {
            return base.CanSwitchPosition() && Record.CanSwitchPosition;
        }
        public override bool CanBeCarried()
        {
            return base.CanBeCarried() && Record.CanBeCarried;
        }
        public override bool CanUsePortal()
        {
            return base.CanUsePortal() && Record.CanUsePortal;
        }
        public override bool MustSkipTurn()
        {
            return base.MustSkipTurn() || !Record.CanPlay;
        }

        public override bool UseSummonSlot()
        {
            return Record.UseSummonSlot;
        }
        public override bool DisplayInTimeline()
        {
            return Record.CanPlay;
        }
        public override GameFightFighterInformations GetFightFighterInformations(CharacterFighter target)
        {
            return new GameFightMonsterInformations()
            {
                contextualId = Id,
                creatureGenericId = (short)Record.Id,
                creatureGrade = Grade.GradeId,
                creatureLevel = Grade.Level,
                disposition = GetEntityDispositionInformations(),
                look = Look.ToEntityLook(),
                previousPositions = new short[0],
                stats = Stats.GetGameFightCharacteristics(this, target),
                wave = 0,
                spawnInfo = new GameContextBasicSpawnInformation()
                {
                    teamId = (byte)Team.TeamId,
                    alive = Alive,
                    informations = new GameContextActorPositionInformations()
                    {
                        contextualId = Id,
                        disposition = GetEntityDispositionInformations(),
                    },
                }
            };
        }

        public override Spell GetSpell(short spellId)
        {
            var record = Record.SpellRecords[spellId];
            var level = record.GetLevel(Grade.GradeId);
            return new Spell(record, level);
        }

        public override bool HasSpell(short spellId)
        {
            return Record.Spells.Contains(spellId);
        }

        public override IEnumerable<SpellRecord> GetSpells()
        {
            return Record.SpellRecords.Values;
        }


        public override void OnSummoned()
        {
            CastSpell(Grade.StartingSpellLevelId);
        }
    }
}
