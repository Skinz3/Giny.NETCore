using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;

namespace Giny.World.Managers.Fights.Fighters
{
    public abstract class SummonedFighter : AIFighter
    {
        public Fighter Summoner
        {
            get;
            set;
        }
        public CharacterFighter Controller
        {
            get;
            set;
        }

        public bool IsControlled => Controller != null;

        private SpellEffectHandler SummoningEffect
        {
            get;
            set;
        }

        public override bool Sex => false;

        public override BreedEnum Breed => BreedEnum.SUMMONED;

        public SummonedFighter(Fighter owner, SpellEffectHandler summoningEffect, CellRecord cell) :
            base(owner.Team, null)
        {
            Cell = cell;
            SummoningEffect = summoningEffect;
            Summoner = owner;
        }

        public override void Initialize()
        {
            base.Initialize();
            FightStartCell = this.Cell;
            Direction = Summoner.Cell.Point.OrientationTo(Cell.Point);
        }
        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            throw new NotImplementedException();
        }
        public override SpellEffectHandler GetSummoningEffect()
        {
            return SummoningEffect;
        }
        public override void Kick(Fighter source)
        {
            throw new NotImplementedException();
        }
        public override void OnTurnBegin()
        {
            if (!IsControlled)
            {
                base.OnTurnBegin();
            }
            else
            {
                if (Controller.Disconnected)
                {
                    PassTurn();
                }
            }
        }
        [WIP("can controller be different of summoner?")]
        public void SetController(CharacterFighter controller)
        {
            this.Controller = controller;
        }
        public void SwitchContext()
        {
            var msg = new SlaveSwitchContextMessage()
            {
                masterId = Controller.Id,
                shortcuts = GetShortcuts().ToArray(),
                slaveId = Id,
                slaveSpells = GetSpellItems(),
                slaveStats = this.Stats.GetCharacterCharacteristicsInformations(Controller.Character),
            };
            this.Controller.Character.Client.Send(msg);
        }
        private Shortcut[] GetShortcuts()
        {
            SpellRecord[] spells = GetSpells().ToArray();
            Shortcut[] results = new Shortcut[spells.Length];

            for (byte i = 0; i < spells.Length; i++)
            {
                results[i] = new ShortcutSpell()
                {
                    slot = i,
                    spellId = spells[i].Id,
                };
            }

            return results;
        }
        private SpellItem[] GetSpellItems()
        {
            return GetSpells().Select(x => new SpellItem(x.Id, GetSpell(x.Id).Level.Grade)).ToArray();
        }
        public void RemoveController()
        {
            if (Controller != null)
            {
                this.Controller = null;
            }
        }
        public override bool IsSummoned()
        {
            return true;
        }
        public override Fighter GetSummoner()
        {
            return Summoner;
        }
        private void ChangeContextIfNeeded()
        {
            if (Controller != null)
            {
                var nextSummon = Controller.GetNextControlableSummon(1);

                if (nextSummon != null)
                {
                    nextSummon.SwitchContext();
                }
            }
        }
        public override void OnDie(Fighter killedBy)
        {
            base.OnDie(killedBy);
            ChangeContextIfNeeded();
        }
        public override void OnTurnEnded()
        {
            ChangeContextIfNeeded();
        }
        public virtual bool UseSummonSlot()
        {
            return true;
        }
        public override Fighter GetController()
        {
            return Controller;
        }

        public abstract void OnSummoned();

    }
}
