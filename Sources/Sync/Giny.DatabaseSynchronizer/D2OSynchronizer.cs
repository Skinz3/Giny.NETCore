using Giny.Core;
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
using Giny.World.Records.Idols;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
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
            string d2oDirectory = Path.Combine(Program.ClientPath, ClientConstants.D2oDirectory);

            foreach (var file in Directory.GetFiles(d2oDirectory))
            {
                if (Path.GetExtension(file) == ".d2o")
                    d2oReaders.Add(new D2OReader(file));
            }

            if (!Program.SYNC_D2O)
            {
                return;
            }
            foreach (var tableType in DatabaseManager.Instance.TableTypes)
            {
                var attribute = tableType.GetCustomAttribute<D2OClassAttribute>();

                if (attribute != null)
                {
                    var reader = d2oReaders.FirstOrDefault(x => x.Classes.Values.Any(j => j.Name == attribute.Name));
                    Logger.Write("Building " + tableType.Name + "...");
                    BuildFromObjects(reader.EnumerateObjects().Where(x => x.GetType().Name == attribute.Name), tableType);
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
        private static void BuildFromObjects(IEnumerable<object> objects, Type tableType)
        {
            var objectType = objects.First().GetType();

            foreach (var obj in objects)
            {
                ITable table = (ITable)Convert.ChangeType(Activator.CreateInstance(tableType), tableType);

                foreach (var property in tableType.GetProperties())
                {
                    var d2oFieldAttribute = property.GetCustomAttribute<D2OFieldAttribute>();

                    if (d2oFieldAttribute != null)
                    {
                        var d2oField = objectType.GetField(d2oFieldAttribute.FieldName);

                        if (d2oField == null)
                        {
                            Logger.Write("Unknown D2O field : " + d2oFieldAttribute.FieldName + " in " + objectType.Name, Channels.Critical);
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
                            else if (property.PropertyType == typeof(List<Giny.World.Managers.Monsters.MonsterGrade>))
                            {
                                value = ConvertToMonsterGrades((List<MonsterGrade>)value);
                            }
                            else if (property.PropertyType == typeof(Dictionary<long, MonsterRoom>))
                            {
                                value = ConvertMonsterRooms((List<long>)value);
                            }
                            else if (property.PropertyType == typeof(List<Giny.World.Managers.Entities.Monsters.MonsterDrop>))
                            {
                                value = ConvertToMonsterDrop((List<MonsterDrop>)value);
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
                                Logger.Write("Unable to set property " + property.Name + " :" + ex, Channels.Warning);
                            }
                        }
                    }
                }

                TableManager.Instance.GetWriter(tableType).Use(new ITable[] { table }, DatabaseAction.Add);
            }
        }

        private static Dictionary<long, MonsterRoom> ConvertMonsterRooms(List<long> mapIds)
        {
            var results = new Dictionary<long, MonsterRoom>();

            foreach (var map in mapIds)
            {
                results.Add(map, new MonsterRoom(10f, new short[0]));
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

        private static List<World.Managers.Entities.Monsters.MonsterDrop> ConvertToMonsterDrop(List<MonsterDrop> value)
        {
            List<World.Managers.Entities.Monsters.MonsterDrop> drops = new List<World.Managers.Entities.Monsters.MonsterDrop>();

            foreach (var val in value)
            {
                drops.Add(new World.Managers.Entities.Monsters.MonsterDrop()
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

        private static List<Giny.World.Managers.Monsters.MonsterGrade> ConvertToMonsterGrades(List<MonsterGrade> value)
        {
            List<Giny.World.Managers.Monsters.MonsterGrade> grades = new List<Giny.World.Managers.Monsters.MonsterGrade>();

            foreach (var val in value)
            {
                grades.Add(new World.Managers.Monsters.MonsterGrade()
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

                }); ;
            }
            return grades;
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
                return new EffectDice((short)effectDice.EffectId, (short)effectDice.diceNum, (short)effectDice.DiceSide, (short)effectDice.value)
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
