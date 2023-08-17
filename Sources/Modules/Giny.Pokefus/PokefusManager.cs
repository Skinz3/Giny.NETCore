using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.ORM;
using Giny.Pokefus.Effects;
using Giny.Pokefus.Fight.Fighters;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Items;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    public class PokefusManager : Singleton<PokefusManager>
    {


        public const short MaxPokefusLevel = 200;

        private const string PokefusLevelRequirementMessage = "Vous devez être niveau {0} pour pouvoir équiper ce pokéfus.";

        private const string PokefusLevelUpMessage = "Felicitation ! Votre Pokefus <b>{0}</b> passe niveau <b>{1}</b>.";

        private const string CannotCastMessage = "Le sort : {0} ne peut pas être lancé par un pokéfus.";

        private static EffectsEnum[] ForbiddenPokefusSpellEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Kill,
            EffectsEnum.Effect_KillAndSummon,
            EffectsEnum.Effect_KillAndSummonSlave,
        };

        private static short[] ForbiddenPokefusSpells = new short[]
        {
            411, // Tuerie
            228, // Etourderie Mortelle
            1037, // Briss Deuniss
            14757, // Sort tot
            914, // Invocation Poutch
        };

        public void Initialize()
        {

        }

        public void OnHumanOptionsCreated(Character character)
        {
            foreach (var item in GetPokefusItems(character.Inventory))
            {
                EffectPokefus pokefusEffect = item.Effects.GetFirst<EffectPokefus>();
                MonsterRecord monsterRecord = MonsterRecord.GetMonsterRecord(pokefusEffect.MonsterId);
                character.AddFollower(monsterRecord.Look);
            }
        }
        public void OnPlayerResultApplied(FightPlayerResult result)
        {
            if (result.Fight.Winners == result.Fighter.Team)
            {
                if (result.ExperienceData != null)
                {
                    AddPokefusExperience(result.Character, result.ExperienceData.ExperienceFightDelta);
                }
            }
        }
        public bool AddPokefusExperience(Character character, long experienceDelta)
        {
            var items = character.Inventory.GetEquipedItems().Where(x => x.Effects.Exists<EffectPokefus>());

            if (items.Count() == 0)
            {
                return false;
            }
            var delta = experienceDelta / items.Count();

            foreach (var item in items)
            {
                EffectPokefus effect = item.Effects.GetFirst<EffectPokefus>();

                EffectPokefusLevel effectLevel = item.Effects.GetFirst<EffectPokefusLevel>();

                var level = effectLevel.Level;

                effectLevel.AddExperience(delta);

                if (level != effectLevel.Level)
                {
                    character.Reply(string.Format(PokefusLevelUpMessage, effect.MonsterName, effectLevel.Level));
                }

                character.Inventory.OnItemModified(item);

                item.UpdateElement();
            }

            return true;

        }
        private double GetDropRate(MonsterFighter monster, CharacterFighter fighter)
        {
            const double ProspectingCoeff = 2d;

            double dropRate = 0.010d;

            if (monster.Level >= 50)
            {
                dropRate = 0.008;
            }
            else if (monster.Level >= 100)
            {
                dropRate = 0.007;
            }
            else if (monster.Level >= 150)
            {
                dropRate = 0.006;
            }
            else if (monster.Level >= 200)
            {
                dropRate = 0.005;
            }

            if (monster.Record.IsBoss)
            {
                dropRate = 0.001;
            }

            dropRate = dropRate + (dropRate * ((fighter.Stats[CharacteristicEnum.MAGIC_FIND].TotalInContext() / 500d) * ProspectingCoeff));

            var percentage = Math.Round(dropRate * 100d, 2);

            return percentage;
        }
        public void OnFighterJoined(Fighter fighter)
        {
            if (!(fighter is CharacterFighter))
                return;

            CharacterFighter characterFighter = (CharacterFighter)fighter;

            foreach (var pokefusItem in GetPokefusItems(characterFighter.Character.Inventory))
            {
                AddFighter(characterFighter, pokefusItem);
            }
        }

        private void AddFighter(CharacterFighter owner, CharacterItemRecord pokefusItem)
        {
            EffectPokefus effect = pokefusItem.Effects.GetFirst<EffectPokefus>();
            MonsterRecord monsterRecord = MonsterRecord.GetMonsterRecord(effect.MonsterId);

            CellRecord cell = owner.Team.GetPlacementCell();

            PokefusFighter pokefusFighter = new PokefusFighter(owner, pokefusItem, monsterRecord, null, effect.GradeId, cell);

            owner.Team.AddFighter(pokefusFighter);
        }
        private void RemoveFighter(CharacterFighter owner, CharacterItemRecord pokefusItem)
        {
            var pokefusFighter = owner.Team.GetFighters<PokefusFighter>().FirstOrDefault(x => x.PokefusItem == pokefusItem);

            if (pokefusFighter != null)
            {
                owner.Team.RemoveFighter(pokefusFighter);
            }
            else
            {
                owner.Fight.Warn("Impossible de retirer le pokéfus du combat. Le combattant est introuvable.");
            }
        }

        private IEnumerable<CharacterItemRecord> GetPokefusItems(Inventory inventory)
        {
            return inventory.GetEquipedItems().Where(x => IsPokefusItem(x));
        }
        private bool IsPokefusItem(CharacterItemRecord item)
        {
            return item.Effects.Exists<EffectPokefus>();
        }
        public CharacterItemRecord CreatePokefusItem(long characterId, ItemRecord itemRecord, MonsterRecord monster, byte monsterGradeId)
        {
            CharacterItemRecord item = ItemsManager.Instance.CreateCharacterItem(itemRecord, characterId, 1);

            item.Effects.Clear();

            item.Effects.Add(new EffectPokefus((short)monster.Id, monster.Name, monsterGradeId));
            item.Effects.Add(new EffectPokefusLevel(0));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Followed, (int)monster.Id));

            return item;
        }

        public bool OnSpellCasting(SpellCast arg)
        {

            if (!(arg.Source is PokefusFighter))
            {
                return true;
            }


            bool result = true;

            if (arg.Spell.Level.Effects.Any(x => ForbiddenPokefusSpellEffects.Contains(x.EffectEnum)))
            {
                result = false;
            }

            foreach (var effect in arg.Spell.Level.Effects)
            {
                var dice = ((EffectDice)effect);

                if (effect.EffectEnum == EffectsEnum.Effect_AddState)
                {
                    SpellStateRecord state = SpellStateRecord.GetSpellStateRecord(dice.Value);

                    if (state.Id == 218 || state.Invulnerable)
                    {
                        result = false;
                    }
                }

                if (effect.EffectEnum == EffectsEnum.Effect_SubAP || effect.EffectEnum == EffectsEnum.Effect_RemoveAP || effect.EffectEnum == EffectsEnum.Effect_SubAP_Roll)
                {
                    if (dice.Max > 4 || dice.Max > 4)
                    {
                        result = false;
                    }
                }
                if (effect.EffectEnum == EffectsEnum.Effect_SkipTurn || effect.EffectEnum == EffectsEnum.Effect_SkipTurn_1031)
                {
                    result = false;
                }

                if (effect.EffectEnum == EffectsEnum.Effect_SubResistances || effect.EffectEnum == EffectsEnum.Effect_SubEarthResistPercent ||
                    effect.EffectEnum == EffectsEnum.Effect_SubWaterResistPercent || effect.EffectEnum == EffectsEnum.Effect_SubAirResistPercent
                    || effect.EffectEnum == EffectsEnum.Effect_SubFireResistPercent)
                {
                    if (dice.Max > 50 || dice.Min > 50)
                    {
                        result = false;
                    }
                }
                if (effect.EffectEnum == EffectsEnum.Effect_DamageEarth || effect.EffectEnum == EffectsEnum.Effect_DamageAir ||
                    effect.EffectEnum == EffectsEnum.Effect_DamageFire || effect.EffectEnum == EffectsEnum.Effect_DamageWater ||
                    effect.EffectEnum == EffectsEnum.Effect_DamageNeutral)
                {
                    if (dice.Min > 150 || dice.Max > 150)
                    {
                        result = false;
                    }
                }
                if (effect.EffectEnum == EffectsEnum.Effect_Summon && dice.Min == 2941)
                {
                    result = false;
                }
            }

            if (ForbiddenPokefusSpells.Contains(arg.SpellId))
            {
                result = false;
            }

            if (!result)
            {
                arg.Source.Fight.Warn(string.Format(CannotCastMessage, arg.Spell.Record.Name));
            }

            return result;
        }

        public bool CanEquipItem(Character character, CharacterItemRecord item)
        {
            EffectPokefus effect = item.Effects.GetFirst<EffectPokefus>();

            if (effect != null)
            {
                var monsterGrade = MonsterRecord.GetMonsterRecord(effect.MonsterId).GetGrade(effect.GradeId);

                short required = monsterGrade.Level;

                if (required > 200)
                {
                    required = 200;
                }

                if (required > character.Level)
                {
                    character.Reply(string.Format(PokefusLevelRequirementMessage, required));
                    return false;
                }

                return true;
            }
            return true;
        }
        public void OnItemEquipped(Character owner, CharacterItemRecord item)
        {
            if (owner.Fighting && !owner.Fighter.Fight.Started && IsPokefusItem(item))
            {
                AddFighter(owner.Fighter, item);
            }
        }
        public void OnItemUnequipped(Character owner, CharacterItemRecord item)
        {
            if (owner.Fighting && !owner.Fighter.Fight.Started && IsPokefusItem(item))
            {
                RemoveFighter(owner.Fighter, item);
            }
        }
    }

}
