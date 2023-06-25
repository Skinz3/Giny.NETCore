using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Fighters
{
    public class DoubleFighter : SummonedFighter
    {
        public DoubleFighter(Fighter owner, SpellEffectHandler summoningEffect, CellRecord cell) : base(owner, summoningEffect, cell)
        {

        }

        public override string Name => Summoner.Name;

        public override short Level => Summoner.Level;

        public override void OnTurnBegin()
        {
            base.OnTurnBegin();
        }
        public override void OnTurnEnded()
        {
            base.OnTurnEnded();
        }
        public override void Initialize()
        {
            this.Stats = new FighterStats(Summoner.Stats);
            this.Stats.MaxLifePoints = Summoner.Stats.BaseMaxLife;
            this.Stats.LifePoints = this.Stats.MaxLifePoints;
            this.Look = Summoner.Look.Clone();
            base.Initialize();
        }
        public override GameFightFighterInformations GetFightFighterInformations(CharacterFighter to)
        {
            GameFightFighterInformations fighterInformations = Summoner.GetFightFighterInformations(to);
            fighterInformations.contextualId = Id;
            fighterInformations.disposition = GetEntityDispositionInformations();
            fighterInformations.look = this.Look.ToEntityLook();
            fighterInformations.previousPositions = GetPreviousPositions();
            fighterInformations.spawnInfo =  new GameContextBasicSpawnInformation()
            {
                alive = Alive,
                informations = new GameContextActorPositionInformations(Id, GetEntityDispositionInformations()),
                teamId = (byte)Team.TeamId,
            };
            fighterInformations.stats = Stats.GetGameFightCharacteristics(this, to);
            return fighterInformations;
        }

        public override Spell GetSpell(short spellId)
        {
            return null;
        }

        public override IEnumerable<SpellRecord> GetSpells()
        {
            return new SpellRecord[0];
        }
        public override string ToString()
        {
            return base.ToString() + " (Double)";
        }

        public override void OnSummoned()
        {
            
        }
    }
}
