using Giny.Core;
using Giny.Core.Logging;
using Giny.Core.Misc;
using Giny.IO;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.ORM;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Look;
using Giny.World.Records;
using Giny.World.Records.Breeds;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Effect = Giny.World.Managers.Effects.Effect;

namespace Giny.DatabaseSynchronizer
{
    class D2OSynchronizer
    {
        public static List<D2OReader> d2oReaders = new List<D2OReader>();

        public static void Synchronize()
        {
            string d2oDirectory = Path.Combine(ClientConstants.ClientPath, ClientConstants.D2oDirectory);

            foreach (var file in Directory.GetFiles(d2oDirectory))
            {
                if (Path.GetExtension(file) == ".d2o")
                    d2oReaders.Add(new D2OReader(file));
            }

            if (!Program.SYNC_D2O)
            {
                return;
            }

            Logger.Write("Building D2O", Channels.Info);

            foreach (var tableType in DatabaseManager.Instance.TableTypes.OrderBy(x=>x.Name))
            {
                var attribute = tableType.GetCustomAttribute<D2OClassAttribute>();

                if (attribute != null)
                {
                    var reader = d2oReaders.FirstOrDefault(x => x.Classes.Values.Any(j => j.Name == attribute.Name));
                    Logger.Write("Building " + tableType.Name + "...");

                    var objects = reader.EnumerateObjects().Where(x => x.GetType().Name == attribute.Name).ToArray();

                    BuildFromObjects(objects, tableType);
                }
            }

        }

        private static void LogClasseFields(string classname)
        {
            var reader = d2oReaders.FirstOrDefault(x => x.Classes.Values.Any(j => j.Name == classname));
            var d2oClassDefinition = reader.Classes.FirstOrDefault(x => x.Value.Name == classname);

            StringBuilder sb = new StringBuilder();

            sb.Append(classname);

            foreach (var field in d2oClassDefinition.Value.Fields)
            {
                sb.Append(Environment.NewLine + "-" + field.Key + ":" + field.Value.TypeId);
            }

            Notepad.Open(sb.ToString());
        }
        private static void BuildFromObjects(object[] objects, Type tableType)
        {
            var objectType = objects.First().GetType();

            int current = 0;

            ProgressLogger logger = new ProgressLogger();

            foreach (var obj in objects)
            {
                current++;
                ITable table = (ITable)Convert.ChangeType(Activator.CreateInstance(tableType), tableType);

                foreach (var property in tableType.GetProperties())
                {
                    var d2oFieldAttribute = property.GetCustomAttribute<D2OFieldAttribute>();

                    if (d2oFieldAttribute != null)
                    {
                        var d2oField = objectType.GetField(d2oFieldAttribute.FieldName);

                        if (d2oField == null)
                        {
                            Logger.Write("Unknown D2O field : " + d2oFieldAttribute.FieldName + " in " + tableType.Name, Channels.Critical);
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        var i18nField = property.GetCustomAttribute<I18NFieldAttribute>();

                        if (i18nField != null)
                        {
                            int key = int.Parse(d2oField.GetValue(obj).ToString()); // uint / int cast
                            property.SetValue(table, Program.D2IFileFR.GetText(key));
                        }
                        else
                        {
                            var value = d2oField.GetValue(obj);

                            if (value.GetType() == property.PropertyType)
                            {
                                property.SetValue(table, Convert.ChangeType(value, property.PropertyType));
                                continue;
                            }
                            if (property.PropertyType == typeof(StatUpgradeCost[]))
                            {
                                value = ConvertToStatUpgradeCost(value);
                            }
                            else if (property.PropertyType == typeof(ObjectMapPosition[]))
                            {
                                value = ConvertToObjectMapPosition(value);
                            }
                            else if (property.PropertyType == typeof(EffectCollection))
                            {
                                value = ConvertToServerEffects(((IEnumerable)value).Cast<EffectInstance>());
                            }

                            else if (property.PropertyType == typeof(ServerEntityLook))
                            {
                                value = EntityLookManager.Instance.Parse(value.ToString());
                            }
                            else if (property.PropertyType == typeof(MonsterRacesEnum))
                            {
                                value = Enum.ToObject(property.PropertyType, value);
                            }
                            else if (property.PropertyType == typeof(List<World.Records.Monsters.MonsterGrade>))
                            {
                                value = ConvertToMonsterGrades((List<IO.D2OClasses.MonsterGrade>)value);
                            }
                            else if (property.PropertyType == typeof(Dictionary<long, MonsterRoom>))
                            {
                                value = ConvertMonsterRooms((List<double>)value);
                            }
                            else if (property.PropertyType == typeof(List<World.Records.Monsters.MonsterDrop>))
                            {
                                value = ConvertToMonsterDrop((List<IO.D2OClasses.MonsterDrop>)value);
                            }
                            else if (property.PropertyType == typeof(List<EffectCollection>))
                            {
                                value = ConvertItemSetEffects((List<List<EffectInstance>>)value);
                            }
                            else if (value.GetType().IsGenericType)
                            {
                                var pType = property.PropertyType.GetElementType();

                                if (pType == null)
                                {
                                    pType = property.PropertyType.GetGenericArguments()[0];

                                    var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(pType));

                                    foreach (var element in (IList)value)
                                    {
                                        list.Add(Convert.ChangeType(element, pType));
                                    }
                                    value = list;

                                }
                                else
                                {
                                    var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(pType));

                                    foreach (var element in (IList)value)
                                    {
                                        list.Add(Convert.ChangeType(element, pType));
                                    }
                                    value = list.GetType().GetMethod("ToArray").Invoke(list, new object[0]);
                                }
                            }
                            try
                            {
                                property.SetValue(table, Convert.ChangeType(value, property.PropertyType));
                            }
                            catch (Exception ex)
                            {
                                Logger.Write($"Unable to set property {property.Name} to value ({value}) : {ex}", Channels.Warning);
                            }
                        }
                    }
                }

                logger.WriteProgressBar(current, objects.Length);
                TableManager.Instance.GetWriter(tableType).Use(new ITable[] { table }, DatabaseAction.Add);
            }
            logger.Flush();
        }

        private static Dictionary<long, MonsterRoom> ConvertMonsterRooms(List<double> mapIds)
        {
            var results = new Dictionary<long, MonsterRoom>();

            foreach (var map in mapIds)
            {
                results.Add((long)map, new MonsterRoom(10f, new short[0]));
            }
            return results;
        }
        private static List<EffectCollection> ConvertItemSetEffects(List<List<EffectInstance>> value)
        {
            List<EffectCollection> results = new List<EffectCollection>();

            foreach (var list in value)
            {
                EffectCollection effects = ConvertToServerEffects(list);
                results.Add(effects);
            }

            return results;
        }

        private static List<World.Records.Monsters.MonsterDrop> ConvertToMonsterDrop(List<IO.D2OClasses.MonsterDrop> value)
        {
            List<World.Records.Monsters.MonsterDrop> drops = new List<World.Records.Monsters.MonsterDrop>();

            foreach (var val in value)
            {
                drops.Add(new World.Records.Monsters.MonsterDrop()
                {
                    DropLimit = val.count,
                    criteria = val.criteria,
                    ItemGId = val.objectId,
                    HasCriteria = val.hasCriteria,
                    PercentDropForGrade1 = val.PercentDropForGrade1,
                    PercentDropForGrade2 = val.PercentDropForGrade2,
                    PercentDropForGrade3 = val.percentDropForGrade3,
                    PercentDropForGrade4 = val.percentDropForGrade4,
                    PercentDropForGrade5 = val.PercentDropForGrade5
                });
                ; ;
            }
            return drops;
        }

        private static List<World.Records.Monsters.MonsterGrade> ConvertToMonsterGrades(List<IO.D2OClasses.MonsterGrade> value)
        {
            List<World.Records.Monsters.MonsterGrade> grades = new List<World.Records.Monsters.MonsterGrade>();

            foreach (var val in value)
            {
                grades.Add(new World.Records.Monsters.MonsterGrade()
                {
                    Level = (short)val.level,
                    GradeId = (byte)val.grade,
                    ActionPoints = (short)val.ActionPoints,
                    Agility = (short)val.agility,
                    AirResistance = (short)val.airResistance,
                    StartingSpellLevelId = val.startingSpellId,
                    ApDodge = (short)val.paDodge,
                    Chance = (short)val.chance,
                    DamageReflect = (short)val.damageReflect,
                    EarthResistance = (short)val.earthResistance,
                    FireResistance = (short)val.fireResistance,
                    GradeXp = val.gradeXp,
                    HiddenLevel = (short)val.hiddenLevel,
                    Intelligence = (short)val.intelligence,
                    LifePoints = val.lifePoints,
                    MovementPoints = (short)val.movementPoints,
                    MpDodge = (short)val.pmDodge,
                    NeutralResistance = (short)val.neutralResistance,
                    Strength = (short)val.Strength,
                    Vitality = (short)val.vitality,
                    WaterResistance = (short)val.waterResistance,
                    Wisdom = (short)val.wisdom,
                    BonusCharacteristics = ConvertToMonsterBonusCharacteristics(val.BonusCharacteristics),


                }); ; ;
            }
            return grades;
        }

        private static World.Records.Monsters.MonsterBonusCharacteristics ConvertToMonsterBonusCharacteristics(IO.D2OClasses.MonsterBonusCharacteristics input)
        {
            var result = new World.Records.Monsters.MonsterBonusCharacteristics()
            {
                Agility = input.Agility,
                AirResistance = input.AirResistance,
                APRemoval = input.APRemoval,
                BonusAirDamage = input.BonusAirDamage,
                BonusEarthDamage = input.BonusEarthDamage,
                BonusFireDamage = input.BonusFireDamage,
                BonusWaterDamage = input.BonusWaterDamage,
                Chance = input.Chance,
                EarthResistance = input.EarthResistance,
                FireResistance = input.FireResistance,
                Intelligence = input.Intelligence,
                LifePoints = input.LifePoints,
                NeutralResistance = input.NeutralResistance,
                Strength = input.Strength,
                TackleBlock = input.TackleBlock,
                TackleEvade = input.TackleEvade,
                WaterResistance = input.WaterResistance,
                Wisdom = input.Wisdom,
            };

            return result;

        }
        private static EffectCollection ConvertToServerEffects(IEnumerable<EffectInstance> effectInstances)
        {
            EffectCollection results = new EffectCollection();

            foreach (var effectInstance in effectInstances)
            {
                if (effectInstance == null)
                {
                    continue;
                }
                else
                {
                    if (!effectInstance.ForClientOnly)
                        results.Add(BuildEffect(effectInstance));

                }
            }
            return results;
        }
        private static Effect BuildEffect(EffectInstance effectInstance)
        {
            var effectDice = effectInstance as EffectInstanceDice;

            if (effectDice != null)
            {
                return new EffectDice((short)effectDice.EffectId, (int)effectDice.diceNum, (int)effectDice.DiceSide, effectDice.value)
                {

                    Delay = effectDice.delay,
                    Dispellable = effectDice.dispellable,
                    Duration = effectDice.duration,
                    Group = effectDice.group,
                    Modificator = effectDice.modificator,
                    Order = effectDice.order,
                    Trigger = effectDice.trigger,
                    RawTriggers = effectDice.triggers,
                    RawZone = effectDice.rawZone,
                    TargetMask = effectDice.TargetMask,
                    Random = effectDice.random,
                    TargetId = effectDice.targetId,
                };
            }


            throw new Exception();
        }
        private static ObjectMapPosition[] ConvertToObjectMapPosition(object value)
        {
            var l1 = ((IEnumerable)value).Cast<object>().ToList();

            ObjectMapPosition[] result = new ObjectMapPosition[l1.Count];

            for (int i = 0; i < l1.Count; i++)
            {
                var l2 = ((IEnumerable)l1[i]).Cast<object>().ToList();
                result[i] = new ObjectMapPosition((int)Convert.ChangeType(l2[0], typeof(int)), (int)Convert.ChangeType(l2[1], typeof(int)));
            }

            return result;
        }

        private static StatUpgradeCost[] ConvertToStatUpgradeCost(object value)
        {
            var l1 = ((IEnumerable)value).Cast<object>().ToList();

            StatUpgradeCost[] upgradeCost = new StatUpgradeCost[l1.Count];

            for (int i = 0; i < l1.Count; i++)
            {
                var l2 = ((IEnumerable)l1[i]).Cast<object>().ToList();

                upgradeCost[i] = new StatUpgradeCost((short)Convert.ChangeType(l2[0], typeof(short)), (short)Convert.ChangeType(l2[1], typeof(short)));
            }


            return upgradeCost;
        }
    }
}
