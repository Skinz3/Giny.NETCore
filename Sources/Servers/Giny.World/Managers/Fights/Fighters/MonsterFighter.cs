using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.Extensions;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;

namespace Giny.World.Managers.Fights.Fighters
{
    public class MonsterFighter : AIFighter, IMonster
    {
        readonly Dictionary<MonsterDrop, int> m_dropsCount = new Dictionary<MonsterDrop, int>();

        public Monster Monster
        {
            get;
            private set;
        }
        public MonsterFighter(FightTeam team, CellRecord roleplayCell, Monster monster) : base(team, roleplayCell)
        {
            this.Monster = monster;
        }

        public override bool CanDrop => true;

        public override short Level => Monster.Grade.Level;

        public override string Name => Monster.Record.Name;

        public override bool Sex => false;

        public MonsterGrade Grade => Monster.Grade;

        public MonsterRecord Record => Monster.Record;

        public override BreedEnum Breed => BreedEnum.MONSTER;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override ServerEntityLook CreateLook()
        {
            return Record.Look.Clone(); 
        }
        public override FighterStats CreateStats()
        {
            return new FighterStats(Grade, null);
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
        public override GameFightFighterInformations GetFightFighterInformations(CharacterFighter target)
        {
            return new GameFightMonsterInformations()
            {
                contextualId = Id,
                creatureGenericId = (short)Monster.Record.Id,
                creatureGrade = Monster.GradeId,
                creatureLevel = Monster.Grade.Level,
                disposition = GetEntityDispositionInformations(),
                look = Look.ToEntityLook(),
                previousPositions = GetPreviousPositions(),
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

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            return new FightTeamMemberMonsterInformations()
            {
                id = Id,
                grade = Monster.GradeId,
                monsterId = (int)Monster.Record.Id
            };
        }

        public override int GetDroppedKamas()
        {
            return this.Random.Next(Monster.Record.MinDroppedKamas, Monster.Record.MaxDroppedKamas + 1);
        }

        public override IEnumerable<DroppedItem> RollLoot(IFightResult looter, double bonusRatio)
        {
            // have to be dead before
            if (Alive)
                return new DroppedItem[0];

            var items = new List<DroppedItem>();

            var prospectingSum = EnemyTeam.GetFighters<CharacterFighter>(false).Sum(entry => entry.Stats[CharacteristicEnum.MAGIC_FIND].TotalInContext());;

            foreach (var droppableItem in Monster.Record.Drops.Where(droppableItem => !droppableItem.HasCriteria && prospectingSum >= droppableItem.ProspectingLock).Shuffle())
            {
                for (var i = 0; i < droppableItem.RollsCounter; i++)
                {
                    if (droppableItem.DropLimit > 0 && m_dropsCount.ContainsKey(droppableItem) && m_dropsCount[droppableItem] >= droppableItem.DropLimit)
                        break;

                    var chance = (Random.Next(0, 100) + Random.NextDouble());
                    var dropRate = FightFormulas.Instance.AdjustDropChance(looter, droppableItem, Monster, bonusRatio);

                    if (!(dropRate >= chance))
                        continue;

                    items.Add(new DroppedItem((short)droppableItem.ItemGId, 1));

                    if (!m_dropsCount.ContainsKey(droppableItem))
                        m_dropsCount.Add(droppableItem, 1);
                    else
                        m_dropsCount[droppableItem]++;
                }
            }


            return items;
        }

        public override Spell GetSpell(short spellId)
        {
            var record = Record.SpellRecords[spellId];
            var level = record.GetLevel(Grade.GradeId);
            return new Spell(record, level);
        }

        public override void Kick(Fighter source)
        {
            throw new NotImplementedException();
        }

        public override bool HasSpell(short spellId)
        {
            return Record.Spells.Contains(spellId);
        }
        public override IEnumerable<SpellRecord> GetSpells()
        {
            return Record.SpellRecords.Values;
        }

        
        public override void OnTurnEnded()
        {

        }
        public override void CastInitialSpells()
        {
            CastSpell(Grade.StartingSpellLevelId);
        }
    }
}
