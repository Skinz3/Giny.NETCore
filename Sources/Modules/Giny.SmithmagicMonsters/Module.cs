using Giny.Core.Extensions;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Api;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Items;
using Giny.World.Modules;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;

namespace Giny.SmithmagicMonsters
{
    [Module("Smithmagic monsters")]
    public class Module : IModule
    {
        private ItemTypeEnum[] SmithmagicItemTypes = new ItemTypeEnum[]
        {
            ItemTypeEnum.AXE,
            ItemTypeEnum.BOW,
            ItemTypeEnum.DAGGER,
            ItemTypeEnum.SHOVEL,

            ItemTypeEnum.RING,
            ItemTypeEnum.HAT,
            ItemTypeEnum.AMULET,
            ItemTypeEnum.BOOTS,
            ItemTypeEnum.CLOAK,

        };

        private EffectsEnum[] ExoEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_AddAP_111,
            EffectsEnum.Effect_AddMP_128,
            EffectsEnum.Effect_AddRange,
        };

        private const int ExoRate = 15;

        public void CreateHooks()
        {
            FightEventApi.OnPlayerResultApplied += OnResultApplied;
        }

        private void OnResultApplied(FightPlayerResult result)
        {
            if (result.Fight.Winners != result.Fighter.Team)
            {
                return;
            }
            if (!(result.Fight is FightPvM))
            {
                return;
            }

            if (!result.Fight.Map.IsDungeonMap)
            {
                return;
            }

            Random random = new Random();

            MonsterFighter[] bosses = result.Fighter.EnemyTeam.GetFighters<MonsterFighter>(false).Where(x => x.Record.IsBoss).ToArray();

            if (bosses.Length == 0)
            {
                return;
            }

            CharacterItemRecord item = result.Character.Inventory.GetEquipedItems().Where(x => CanBeUpgraded(x)).Random(random);

            if (item == null)
            {
                return;
            }


            var val = random.NextDouble();

            if (val < 1.0)
            {
                AddToEffect(result.Character, result.Fight.Map.Dungeon, item, random);
            }
        }

        private void AddToEffect(Character character, DungeonRecord dungeon, CharacterItemRecord item, Random random)
        {
            var position = item.PositionEnum;
            var quantity = item.Quantity;

            character.Inventory.Unequip(item.PositionEnum);

            var valExo = random.Next(0, 101);

            if (valExo <= ExoRate && dungeon.OptimalPlayerLevel == 200 && !ItemHasExo(item))
            {
                item.Effects.Add(new EffectInteger(ExoEffects.Random(random), 1));

                character.Reply($"Votre objet <b>[{item.Record.Name}]</b> à obtenu une ligne exotique ! ");
            }
            else
            {
                var itemEffects = item.Effects.OfType<EffectInteger>();

                EffectInteger boostedEffect = null;


                boostedEffect = itemEffects.Where(x => EffectCanBeUpgraded(item, x)).Random(random);

                var recordEffect = item.Record.Effects.GetFirst<EffectDice>(boostedEffect.EffectEnum);

                var max = ComputeOverrideMax(item, recordEffect);

                var difference = max - boostedEffect.Value;


                if (dungeon.OptimalPlayerLevel >= 200)
                {
                    difference = Math.Min(20, difference);
                }
                else if (dungeon.OptimalPlayerLevel >= 150)
                {
                    difference = Math.Min(15, difference);
                }
                else if (dungeon.OptimalPlayerLevel >= 100)
                {
                    difference = Math.Min(10, difference);
                }
                else if (dungeon.OptimalPlayerLevel >= 50)
                {
                    difference = Math.Min(5, difference);
                }
                else if (dungeon.OptimalPlayerLevel >= 30)
                {
                    difference = Math.Min(3, difference);
                }
                else if (dungeon.OptimalPlayerLevel >= 10)
                {
                    difference = Math.Min(1, difference);
                }


                boostedEffect.Value += difference;

                var index = item.Effects.ToList().IndexOf(boostedEffect) + 1;

                character.Reply($"La statistique <b>{index}</b> de  <b>[{item.Record.Name}]</b> a <b>augmentée de {difference}</b> !");

            }


            character.Inventory.OnItemModified(item);
            character.Inventory.SetItemPosition(item.UId, position, quantity);

        }

        private bool CanBeUpgraded(CharacterItemRecord item)
        {
            if (!SmithmagicItemTypes.Contains(item.Record.TypeEnum))
            {
                return false;
            }


            Dictionary<Effect, bool> map = new Dictionary<Effect, bool>();


            foreach (var effect in item.Effects.OfType<EffectInteger>())
            {
                map.Add(effect, EffectCanBeUpgraded(item, effect));
            }


            bool valid = map.Any(x => x.Value);

            return valid;
        }

       
        private bool ItemHasExo(CharacterItemRecord item)
        {
            return item.Effects.Any(x => ExoEffects.Contains(x.EffectEnum) && !item.Record.Effects.Any(y => y.EffectEnum == x.EffectEnum));
        }

        private bool EffectCanBeUpgraded(CharacterItemRecord item, EffectInteger effect)
        {
            var recordEffect = item.Record.Effects.GetFirst<EffectDice>(effect.EffectEnum);

            if (recordEffect == null)
            {
                return false;
            }

            return effect.Value < ComputeOverrideMax(item, recordEffect);
        }

        private int ComputeOverrideMax(CharacterItemRecord item, EffectDice effect)
        {
            var max = effect.Max;

            switch (effect.EffectEnum)
            {
                case EffectsEnum.Effect_AddWisdom:
                case EffectsEnum.Effect_AddChance:
                case EffectsEnum.Effect_AddIntelligence:
                case EffectsEnum.Effect_AddAgility:
                case EffectsEnum.Effect_AddStrength:
                    max += 15;
                    break;

                case EffectsEnum.Effect_AddVitality:
                    max += item.Record.Level;
                    break;

                case EffectsEnum.Effect_AddDamageBonus:
                    max += 10;
                    break;
            }

            return max;
        }

        public void Initialize()
        {

        }
    }
}