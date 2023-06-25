using Giny.Pokefus.Effects;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Monsters;
using Giny.World.Managers.Stats;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus.Fight.Fighters
{
    public class PokefusFighter : SummonedMonster
    {
        public CharacterItemRecord PokefusItem
        {
            get;
            private set;
        }
        public PokefusFighter(Fighter owner, CharacterItemRecord pokefusItem, MonsterRecord record, SpellEffectHandler summoningEffect, byte gradeId, CellRecord cell) : base(owner, record, summoningEffect, gradeId, cell)
        {
            this.PokefusItem = pokefusItem;
        }

        public override short Level => PokefusItem.Effects.GetFirst<EffectPokefusLevel>().Level;

        public override bool CanDrop => true;

        public override bool Sex => false;

        public override void Initialize()
        {
            this.Look = Record.Look.Clone();

            this.SetController((CharacterFighter)Summoner);

            base.Initialize();

            var coeff = ComputeStatsCoeff();
            this.Stats = new FighterStats(Grade, coeff);

            int lifePoints = (int)(coeff * 1000);

            if (Level == 200)
            {
                lifePoints = 3000;
            }

            const double statsMax = 600;

            this.Stats.Wisdom = Characteristic.New((short)(statsMax * coeff));
            this.Stats.Chance = Characteristic.New((short)(statsMax * coeff));
            this.Stats.Agility = Characteristic.New((short)(statsMax * coeff));
            this.Stats.Strength = Characteristic.New((short)(statsMax * coeff));
            this.Stats.BaseMaxLife = lifePoints;
            this.Stats.LifePoints = lifePoints;
            this.Stats.MaxLifePoints = lifePoints;
        }

        private double ComputeStatsCoeff()
        {
            return (Level * 2d / 200d) + 0.1d;
        }
        public override GameFightFighterInformations GetFightFighterInformations(CharacterFighter target)
        {
            return new GameFightMonsterInformations()
            {
                contextualId = Id,
                creatureGenericId = (short)Record.Id,
                creatureGrade = Grade.GradeId,
                creatureLevel = Level,
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
                grade = Grade.GradeId,
                monsterId = (int)Record.Id
            };
        }

        public override bool UseSummonSlot()
        {
            return false;
        }
        public override void Kick(Fighter source)
        {
            throw new NotImplementedException();
        }

    }
}
