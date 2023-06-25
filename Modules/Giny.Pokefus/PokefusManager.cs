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
        private const short RegularItemId = 15710;

        private const short MiniBossItemId = 15161;

        private const short BossItemId = 15393;

        private ItemRecord m_regularItemRecord;
        private ItemRecord m_miniBossItemRecord;
        private ItemRecord m_bossItemRecord;

        public const short MaxPokefusLevel = 200;

        private const string PokefusLevelRequirementMessage = "Vous devez être niveau {0} pour pouvoir équiper ce pokéfus.";

        private const string PokefusLevelUpMessage = "Felicitation ! Votre Pokefus {0} passe niveau {1}";

        private const string DropInfoMessage = "Chance de drop pour {0} : <b>{1}</b> %";

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
        /*  private static short[] ForbiddenMonsters = new short[]
          {
              232,233,494, 1003,216, 3578, 4275, 113, 4362, 4363, 4364, 499, 2819, 3138, 588, 4373,
              424,2954,3141,1181,3760,939,4619,116, 158, 216, 229,250,384,387, 424,499,557,558,559,
              3142,560, 654, 667,670,676,682,802, 831,872,890, 1027,1088,1094, 1140, 1145,1169,
              1181,3138,1194,1195,1231,1233,2384,2424,2493, 2495,2538,2666,2762,2794,2795,2971,2977,
              2978,2979,3101,2819,2822,2827,2906,2941,2951,2954,2955, 2967,3143,3144,3165,3166,3167,
              3173,3174,3217,3293,3294,3295,3296,3297,3298,3341,3362,3398,3413,3433,3457,3461,3519,
              3528,3539,3541,3553,3556,3560,3619,3632,3644,3648,3655,3657,3668,3669, 3677,3678,3680,
              3681,3690,3692,3711,3714, 3716,3726,3735,3749, 3751,3760,3769, 3770,3773,3781,3804,3826,
              3828,3851,3854,3882,3907,3918,3923, 3943,3991,3998,4016, 4017,4020,4024,4034, 4068,4071,
              4074, 4126,4164,4172,4216,4226,4228,4235,4245,4256,4270,4298,4299,4331,4332,4362,4363,
              4364,4442,4451,4456, 4487,4499, 4505,4512, 4523,4552,4559, 4597, 4619,4662,4677,4684,
              4685, 4695,4710,2793, 1044,4139,422,3543,1145,1169, 407,839,2636,1184,1185,1186,1188,1187,
              4460,1070,2570,666,3590,3592,3589,3591,3588,3234,1072,1085,1086,1087,4359,1050,3803,3561,
              783
          };
         */


        public void Initialize()
        {
            m_regularItemRecord = ItemRecord.GetItem(RegularItemId);
            m_miniBossItemRecord = ItemRecord.GetItem(MiniBossItemId);
            m_bossItemRecord = ItemRecord.GetItem(BossItemId);
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
            AsyncRandom random = new AsyncRandom();

            Dictionary<double, List<MonsterFighter>> droppers = new Dictionary<double, List<MonsterFighter>>();

            if (result.Fight.Winners == result.Fighter.Team)
            {
                var monsters = result.Fighter.EnemyTeam.GetFighters<MonsterFighter>(false);

                foreach (var monster in monsters)
                {

                    var chance = (random.Next(0, 100) + random.NextDouble());
                    var dropRate = GetDropRate(monster, result.Fighter);

                    if (droppers.ContainsKey(dropRate))
                    {
                        droppers[dropRate].Add(monster);
                    }
                    else
                    {
                        droppers.Add(dropRate, new List<MonsterFighter>() { monster });
                    }


                    if (!(dropRate >= chance))
                        continue;

                    CharacterItemRecord item = CreatePokefusItem(result.Character.Id, monster.Record, monster.Monster.Grade.GradeId);
                    result.Loot.AddItem(item.GId, item.Quantity);
                    result.Character.Inventory.AddItem(item);
                }

                if (result.ExperienceData != null)
                {
                    AddPokefusExperience(result.Character, result.ExperienceData.ExperienceFightDelta);
                }

                foreach (var dropData in droppers)
                {
                    string monsterNames = string.Join(",", dropData.Value.Select(x => x.Name));
                    result.Character.Reply(string.Format(DropInfoMessage, monsterNames, dropData.Key));
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
                    character.DisplayPopup(0, "Serveur", string.Format(PokefusLevelUpMessage, effect.MonsterName, effectLevel.Level));
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

            dropRate = dropRate + (dropRate * ((fighter.Stats[CharacteristicEnum.PROSPECTING].TotalInContext() / 500d) * ProspectingCoeff));

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
        public CharacterItemRecord CreatePokefusItem(long characterId, MonsterRecord monster, byte monsterGradeId)
        {
            ItemRecord itemRecord;

            if (monster.IsMiniBoss)
            {
                itemRecord = m_miniBossItemRecord;
            }
            else if (monster.IsBoss)
            {
                itemRecord = m_bossItemRecord;
            }
            else
            {
                itemRecord = m_regularItemRecord;
            }

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
