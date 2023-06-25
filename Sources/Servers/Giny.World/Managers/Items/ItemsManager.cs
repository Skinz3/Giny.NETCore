using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.Core.Time;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Records.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Giny.Core.Extensions;
using System.Text;
using System.Threading.Tasks;
using Giny.World.Modules;

namespace Giny.World.Managers.Items
{
    public class ItemsManager : Singleton<ItemsManager>
    {
        private EffectsEnum[] IgnoredEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Exchangeable,
        };

        private ConcurrentDictionary<ItemRecord, double> RunesWeight
        {
            get;
            set;
        }

        private ConcurrentDictionary<EffectsEnum, double> EffectsWeigth = new ConcurrentDictionary<EffectsEnum, double>();

        private Dictionary<short, double> RunesWeightRaw = new Dictionary<short, double>()
        {
            {1524,1 }, // Age 
            {1549,3 },
            {1555,10 },

            {1525,1 }, // Cha 
            {1550,3 },
            {1556,10 },

            {1519,1 },// Fo 
            {1545,3 },
            {1551,10 },

            {1522,1 }, // Ine 
            {1547,3 },
            {1553,10 },

            {7443,2.5}, // Pod 
            {7444,7.5 },
            {7445,25 },

            {7448,1 }, // Ini 
            {7449,3 },
            {7450,10 },

            {1523,1 }, // Vi
            {1548,3 },
            {1554,10 },

            {7447,2 }, // Pi per
            {10615,6 },
            {10616,20 },

            {7451,3 }, // Prospe
            {10662,9 },

            {7436,2 }, // Pui
            {10618,6 },
            {10619,20 },

            {1521,3 }, // Sa
            {1546,8 },
            {1552,30 },

            {7453,2 }, // Ré air
            {19338,6 },

            {7454,2 }, // Ré eau
            {19339,6 },

            {7455,2 }, // Ré terre
            {19342,6 },

            {7452,2 }, // Ré feu
            {19340,6 },

            {7456,2 }, // Ré neutre
            {19341,6 },

            {11651,2 }, // Ré pou
            {11652,6 },

            {11655,2 }, // Ré cri
            {11656,6 },

            {11653,5 }, // do cri
            {11654,15 },

            {11649,5 }, // do pou
            {11650,15 },

            {11661,5 }, // do eau
            {11662,15 },

            {11663,5 }, // do air
            {11664,15 },

            {11659,5 }, // do feu
            {11660,15 },

            {11657,5 }, // do terre
            {11658,15 },

            {11665,5 }, // do terre
            {11666,15 },

            {7435,20 }, // do

            {7437,10 }, // do ren

            {11637,4 }, // fui
            {11638,12 },

            {11639,4 }, // tac
            {11640,12 },

            {11645,7 }, // ret PA
            {11646,21 },

            {11647,7 }, // ret PM
            {11648,21 },

            {7458,6 }, // re per air

            {7457,6 }, // re per feu

            {7560,6 }, // re per eau

            {7459,6 }, // re per terre

            {7460,6 }, // re per neutre

            {11641,7 }, // ré pa
            {11642,21 },

            {11643,7 }, // ré pm
            {11644,21 },

            {7446,5 }, // pi
            {10613,15 },

            {18721,15 }, // do per ar

            {18720,15 }, // do per di

            {18719,15 }, // do per me

            {18723,15 }, // re per me

            {18724,15 }, // re per dis

            {7434,10 }, // so
            {19337,30 },

            {7433,10 }, // cri

            {10057,5 }, // chasse

            {7442,30 }, // invo

            {7438,51 }, // PO

            {1558,90 }, // PME

            {1557,100 }, // PA
 
        };


        private UniqueIdProvider m_idprovider;

        private Dictionary<ItemUsageHandlerAttribute, MethodInfo> m_usageHandlers = new Dictionary<ItemUsageHandlerAttribute, MethodInfo>();

        [StartupInvoke("Items Manager", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            int lastUID = CharacterItemRecord.GetLastItemUID();
            lastUID = Math.Max(lastUID, BankItemRecord.GetLastItemUID());
            lastUID = Math.Max(lastUID, BidShopItemRecord.GetLastItemUID());
            lastUID = Math.Max(lastUID, MerchantItemRecord.GetLastItemUID());


            m_idprovider = new UniqueIdProvider(lastUID);

            foreach (var type in AssemblyCore.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<ItemUsageHandlerAttribute>();

                    if (attribute != null)
                    {
                        m_usageHandlers.Add(attribute, method);
                    }
                }
            }

            foreach (var item in ItemRecord.GetItems())
            {
                item.Effects = new EffectCollection(item.Effects.Where(x => !IgnoredEffects.Contains(x.EffectEnum)));
            }

            this.RunesWeight = new ConcurrentDictionary<ItemRecord, double>();

            foreach (var rawWeight in RunesWeightRaw)
            {
                ItemRecord record = ItemRecord.GetItem(rawWeight.Key);
                RunesWeight.TryAdd(record, rawWeight.Value);
            }


            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAP_111, 100);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddMP_128, 90);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddRange, 51);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddSummonLimit, 30);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddDamageBonus, 20);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_WeaponDamageDonePercent, 15);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_RangedDamageDonePercent, 15);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_MeleeDamageDonePercent, 15);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_SpellDamageDonePercent, 15);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddRangedResistance, 15);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddMeleeResistance, 15);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddHealBonus, 10);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddCriticalHit, 10);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddMPAttack, 7);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAPAttack, 7);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddDodgeMPPercent, 7);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddDodgeAPPercent, 7);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAirResistPercent, 6);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddFireResistPercent, 6);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddEarthResistPercent, 6);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddNeutralResistPercent, 6);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddWaterResistPercent, 6);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddFireDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddWaterDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddEarthDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAirDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddNeutralDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddCriticalDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddPushDamageBonus, 5);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddTrapBonus, 5);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddLock, 4);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddEvade, 4);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddProspecting, 3);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddWisdom, 3);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_IncreaseDamage_138, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddTrapBonusPercent, 2);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddWaterElementReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddEarthElementReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddFireElementReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAirElementReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddNeutralElementReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddCriticalDamageReduction, 2);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddPushDamageReduction, 2);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddStrength, 1);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddChance, 1);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddAgility, 1);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddIntelligence, 1);

            EffectsWeigth.TryAdd(EffectsEnum.Effect_IncreaseWeight, 0.25d);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddVitality, 0.2d);
            EffectsWeigth.TryAdd(EffectsEnum.Effect_AddInitiative, 0.1d);

        }

        public int PopItemUID()
        {
            return m_idprovider.Pop();
        }
        public void Reload()
        {
            DatabaseManager.Instance.Reload<ItemRecord>();
            DatabaseManager.Instance.Reload<WeaponRecord>();

            WeaponRecord.Initialize();
            ItemRecord.Initialize();
        }
        public IEnumerable<ItemRecord> GetRunesItem(EffectsEnum effect)
        {
            foreach (var item in RunesWeight.Keys)
            {
                var runeEffect = (EffectInteger)item.Effects.FirstOrDefault();

                if (runeEffect.EffectEnum == effect)
                {
                    yield return item;
                }
            }
        }

       
        public double? GetRuneWeight(ItemRecord runeRecord)
        {
            if (RunesWeight.ContainsKey(runeRecord))
            {
                return RunesWeight[runeRecord];
            }
            else
            {
                return null;
            }
        }
        public double? GetEffectWeight(EffectsEnum effect)
        {
            double value = 0d;

            if (EffectsWeigth.TryGetValue(effect, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        
        public CharacterItemRecord CutItem(CharacterItemRecord item, int quantity, CharacterInventoryPositionEnum newItempos)
        {
            CharacterItemRecord newItem = (CharacterItemRecord)item.CloneWithoutUID();

            item.PositionEnum = newItempos;
            item.Quantity = quantity;
            newItem.Quantity -= quantity;

            item.UpdateElement();

            return newItem;
        }

        public CharacterItemRecord CreateCharacterItem(ItemRecord record, long characterId, int quantity, bool perfect = false)
        {
            return new CharacterItemRecord(characterId, 0, (short)record.Id, (byte)CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, quantity, record.Effects.Generate(perfect),
                record.AppearenceId, record.Look);
        }

        public bool UseItem(Character character, CharacterItemRecord item)
        {
            if (!CriteriaManager.Instance.EvaluateCriterias(character.Client, item.Record.Criteria))
            {
                character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 34);
                return false;
            }

            var function = m_usageHandlers.FirstOrDefault(x => x.Key.GId == item.GId);

            if (function.Value != null)
            {
                return (bool)function.Value.Invoke(null, new object[] { character, item });
            }
            else
            {
                function = m_usageHandlers.FirstOrDefault(x => x.Key.ItemType == item.Record.TypeEnum);
                if (function.Value != null)
                {
                    return (bool)function.Value.Invoke(null, new object[] { character, item });

                }
                foreach (var effect in item.Effects.OfType<Effect>())
                {
                    function = m_usageHandlers.FirstOrDefault(x => x.Key.Effect == effect.EffectEnum);
                    if (function.Value != null)
                    {
                        try
                        {
                            return (bool)function.Value.Invoke(null, new object[] { character, effect });
                        }
                        catch (Exception ex)
                        {
                            character.ReplyError(ex.ToString());
                            return false;
                        }


                    }
                    else
                    {
                        return false;

                    }
                }
                return false;

            }
        }
    }
    public class ItemUsageHandlerAttribute : Attribute
    {
        public EffectsEnum? Effect
        {
            get;
            set;
        }
        public short? GId
        {
            get;
            set;
        }
        public ItemTypeEnum? ItemType
        {
            get;
            set;
        }
        public ItemUsageHandlerAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }
        public ItemUsageHandlerAttribute(short gid)
        {
            this.GId = gid;
        }
        public ItemUsageHandlerAttribute(ItemTypeEnum type)
        {
            this.ItemType = type;
        }
    }
}
