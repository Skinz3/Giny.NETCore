using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Giny.ORM.Interfaces;
using Giny.ORM.Attributes;
using Giny;
using Giny.Core;
using System.Globalization;
using System.Collections;
using System.Linq.Expressions;
using Giny.ORM.Expressions;
using System.Diagnostics;
using Giny.Core.IO;

namespace Giny.ORM.IO
{
    public class DatabaseReader
    {
        private MySqlDataReader m_reader;

        public string TableName
        {
            get;
            private set;
        }
        public IDictionary Elements
        {
            get;
            set;
        }
        private PropertyInfo[] Properties
        {
            get;
            set;
        }
        private Type Type
        {
            get;
            set;
        }
        public DatabaseReader(Type type)
        {
            this.Type = type;
            var definition = TableManager.Instance.GetDefinition(type);
            this.Properties = definition.Properties;
            this.TableName = definition.TableAttribute.TableName;
            this.Elements = definition.TableAttribute.Load ? definition.ContainerValue : new Dictionary<long, ITable>();
        }
        public ITable ReadFirst(MySqlConnection connection, string where)
        {
            lock (DatabaseManager.SyncRoot)
            {
                using (var command = new MySqlCommand(string.Format(QueryConstants.SelectWhere, TableName, where), connection))
                {
                    try
                    {
                        this.m_reader = command.ExecuteReader();

                        if (!m_reader.HasRows)
                        {
                            this.m_reader.Close();
                            return null;
                        }
                        var obj = new object[this.Properties.Length];

                        if (m_reader.Read())
                        {
                            for (var i = 0; i < this.Properties.Length; i++)
                                obj[i] = ConvertObject(this.m_reader[i], Properties[i]);
                        }

                        var itable = (ITable)Activator.CreateInstance(Type); // expressions?

                        for (int i = 0; i < Properties.Length; i++)
                        {
                            Properties[i].SetValue(itable, obj[i]);
                        }

                        this.m_reader.Close();

                        return itable;
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("Unable to read table " + TableName + " " + ex, Channels.Warning);
                        this.m_reader?.Close();
                        return null;
                    }
                }
            }
        }
        private void ReadTable(MySqlConnection connection, string parameter)
        {
            lock (DatabaseManager.SyncRoot)
            {
                using (var command = new MySqlCommand(parameter, connection))
                {

                    try
                    {
                        this.m_reader = command.ExecuteReader();
                    }
                    catch 
                    {
                        this.m_reader?.Close();
                        Logger.Write("Unable to read table " + TableName, Channels.Warning);
                        AskForStructureRebuild(connection, parameter);
                        return;
                    }

                    double n = 0;

                    while (this.m_reader.Read())
                    {
                        var obj = new object[this.Properties.Length];
                        for (var i = 0; i < this.m_reader.FieldCount; i++)
                            obj[i] = ConvertObject(this.m_reader[i], Properties[i]);

                        var itable = (ITable)Activator.CreateInstance(Type); // expressions?

                        for (int i = 0; i < Properties.Length; i++)
                        {
                            Properties[i].SetValue(itable, obj[i]);
                        }

                        this.Elements.Add(itable.Id, itable);
                        n++;

                    }
                    this.m_reader.Close();
                }
            }
        }
        public void Read(MySqlConnection connection)
        {
            this.ReadTable(connection, string.Format(QueryConstants.Select, TableName));
        }
        public void Read(MySqlConnection connection, string condition)
        {
            this.ReadTable(connection, string.Format(QueryConstants.Select, TableName, condition));
        }
        public long Count(MySqlConnection connection)
        {
            lock (DatabaseManager.SyncRoot)
            {
                using (MySqlCommand cmd = new MySqlCommand(string.Format(QueryConstants.Count, TableName), connection))
                {
                    return (long)cmd.ExecuteScalar();
                }
            }
        }
        private void AskForStructureRebuild(MySqlConnection connection, string parameter)
        {
            Logger.Write("Do you want to recreate table structure? (y/n)", Channels.Warning);

            string result = Console.ReadLine();

            if (result.ToLower() == "y")
            {
                DatabaseManager.Instance.DropTableIfExists(Type);
                DatabaseManager.Instance.CreateTableIfNotExists(Type);
                ReadTable(connection, parameter);
            }
            else if (result.ToLower() == "n")
            {
                return;
            }
            else
            {
                AskForStructureRebuild(connection, parameter);
            }

        }
        /* Maybe store all properties info instead of calling .NET reflection methods? */
        private object ConvertObject(object obj, PropertyInfo property)
        {
            MethodInfo deserializationMethod = TableManager.Instance.GetDeserializationMethods(property.PropertyType);

            if (property.PropertyType.BaseType == typeof(Enum))
            {
                return Enum.Parse(property.PropertyType, obj.ToString());
            }
            else if (property.CustomAttributes.Count() > 0 && property.GetCustomAttribute<ProtoSerializeAttribute>() != null)
            {
                if (obj is DBNull)
                {
                    return null;
                }
                return Protobuf.Deserialize((byte[])obj, property.PropertyType);
            }
            else if (deserializationMethod != null)
            {
                return deserializationMethod.Invoke(null, new object[] { obj });
            }
            else if (property.PropertyType.IsGenericType)
            {
                if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var genericType = property.PropertyType.GetGenericArguments()[0];
                    var newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));

                    var split = obj.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var element in split)
                    {
                        newList.Add(Convert.ChangeType(element, genericType, CultureInfo.InvariantCulture));
                    }
                    return newList;
                }
            }
            else if (property.PropertyType == typeof(bool))
            {
                return Convert.ToBoolean(Convert.ToByte(obj));
            }
            try
            {
                return Convert.ChangeType(obj, property.PropertyType, CultureInfo.InvariantCulture);
            }
            catch
            {
                string exception = string.Format("Unknown constructor for '{0}', ({1}).", property.PropertyType.Name, property.Name);
                throw new Exception(exception);
            }
        }
        private static IDictionary Read(Type type, string condition)
        {
            DatabaseReader reader = new DatabaseReader(type);
            reader.ReadTable(DatabaseManager.Instance.UseProvider(), string.Format(QueryConstants.Select, reader.TableName, condition));
            return reader.Elements;
        }
        public static IEnumerable<T> Read<T>() where T : ITable
        {
            return Read(typeof(T), string.Empty).Values.Cast<T>();
        }
        public static T ReadFirst<T>(string fieldName, string fieldValue) where T : ITable
        {
            DatabaseReader reader = new DatabaseReader(typeof(T));
            return (T)reader.ReadFirst(DatabaseManager.Instance.UseProvider(), string.Format("{0}='{1}'", fieldName, fieldValue));
        }
    }
}

