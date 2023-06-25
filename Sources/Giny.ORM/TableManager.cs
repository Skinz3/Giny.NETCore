using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM.Attributes;
using Giny.ORM.Expressions;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Giny.ORM.Expressions.ExpressionsManager;

namespace Giny.ORM
{
    public class TableDefinitions
    {
        public FieldInfo Container;

        public IDictionary ContainerValue;

        public TableAttribute TableAttribute;

        public PropertyInfo[] Properties;

        public PropertyInfo PrimaryProperty;

        public bool Load
        {
            get
            {
                return TableAttribute.Load;
            }
        }
        public TableDefinitions(Type type)
        {
            var attribute = type.GetCustomAttribute<TableAttribute>();

            if (attribute == null)
            {
                throw new Exception("Unable to find table attribute for table " + type.Name);
            }

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.GetCustomAttribute<ContainerAttribute>() != null);

            if ((field == null || !field.FieldType.IsGenericType))
            {
                if (attribute.Load)
                {
                    Logger.Write("Unable to find container for table : " + type.Name, Channels.Critical);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            else
            {
                this.Container = field;
                this.ContainerValue = (IDictionary)Container.GetValue(null);
            }

            this.TableAttribute = attribute;
            this.Properties = type.GetProperties().Where(property =>
                property.GetCustomAttribute(typeof(IgnoreAttribute), false) == null).OrderBy(x => x.MetadataToken).ToArray();

            this.PrimaryProperty = GetPrimaryProperty();

        }

        private PropertyInfo GetPrimaryProperty()
        {
            var properties = Properties.Where(property => property.GetCustomAttribute(typeof(PrimaryAttribute), false) != null);

            if (properties.Count() != 1)
            {
                if (properties.Count() == 0)
                    throw new Exception(string.Format("The Table '{0}' hasn't got a primary property", TableAttribute.TableName));

                if (properties.Count() > 1)
                    throw new Exception(string.Format("The Table '{0}' has too much primary properties", TableAttribute.TableName));
            }
            return properties.First();
        }

    }
    public class TableManager : Singleton<TableManager>
    {
        private ConcurrentDictionary<Type, TableDefinitions> m_TableDefinitions = new ConcurrentDictionary<Type, TableDefinitions>();

        private ConcurrentDictionary<Type, DatabaseWriter> m_writers = new ConcurrentDictionary<Type, DatabaseWriter>();

        private ConcurrentDictionary<Type, MethodInfo> m_serializationMethods = new ConcurrentDictionary<Type, MethodInfo>();

        private ConcurrentDictionary<Type, MethodInfo> m_deserializationMethods = new ConcurrentDictionary<Type, MethodInfo>();

        public void Initialize(Type[] tableTypes)
        {
            foreach (var type in tableTypes)
            {
                m_TableDefinitions[type] = new TableDefinitions(type); // no duplicate key check
                m_writers[type] = new DatabaseWriter(type);
                ClearContainer(type);
            }

            this.m_serializationMethods = new ConcurrentDictionary<Type, MethodInfo>();
            this.m_deserializationMethods = new ConcurrentDictionary<Type, MethodInfo>();


            foreach (var tableType in tableTypes)
            {
                foreach (var property in tableType.GetProperties())
                {
                    foreach (var method in property.PropertyType.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
                    {
                        if (!m_serializationMethods.ContainsKey(property.PropertyType) && method.GetCustomAttribute<CustomSerializeAttribute>() != null)
                        {
                            var type = method.GetParameters()[0].ParameterType;
                            m_serializationMethods[type] = method;
                        }
                        if (!m_deserializationMethods.ContainsKey(property.PropertyType) && method.GetCustomAttribute<CustomDeserializeAttribute>() != null)
                        {
                            var type = method.ReturnType;
                            m_deserializationMethods[type] = method;
                        }
                    }
                }
            }
        }
        public void ClearContainer(Type tableType)
        {
            var container = m_TableDefinitions[tableType].ContainerValue;
            container?.Clear();
        }
        public void RemoveFromContainer(ITable element)
        {
            var tableDefinition = m_TableDefinitions[element.GetType()];
            if (tableDefinition.Load)
                tableDefinition.ContainerValue.Remove(element.Id);
        }

        public void AddToContainer(ITable element)
        {
            var tableDefinition = m_TableDefinitions[element.GetType()];

            if (tableDefinition.Load)
            {
                if (!tableDefinition.ContainerValue.Contains(element.Id))
                {
                    tableDefinition.ContainerValue.Add(element.Id, element);
                }
                else
                {
                    throw new Exception("Duplicate key during insersion of " + element.GetType().Name);
                }
            }
        }

        public DatabaseWriter GetWriter(Type type)
        {
            return m_writers[type];
        }
        public TableDefinitions GetDefinition(Type type)
        {
            return m_TableDefinitions[type];
        }

        public MethodInfo GetDeserializationMethods(Type type)
        {
            MethodInfo result = null;
            m_deserializationMethods.TryGetValue(type, out result);
            return result;
        }
        public MethodInfo GetSerializationMethods(Type type)
        {
            MethodInfo result = null;
            m_serializationMethods.TryGetValue(type, out result);
            return result;
        }
        /// <summary>
        /// Use Id provider for better performances.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [PerformanceIssue]
        public long PopId<T>()
        {
            var definition = m_TableDefinitions[typeof(T)];
            var ids = (IEnumerable<long>)definition.ContainerValue.Keys;

            if (ids.Count() == 0)
            {
                return 1;
            }
            return ids.OrderByDescending(x => x).First() + 1;
        }
    }
}
