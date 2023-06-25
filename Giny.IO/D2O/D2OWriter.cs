using Giny.Core.IO;
using Giny.Core.IO.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OWriter : IDisposable
    {
        private const int NullIdentifier = unchecked((int)0xAAAAAAAA);
        private readonly object m_writingSync = new object();
        private Dictionary<Type, int> m_allocatedClassId = new Dictionary<Type, int>();

        private Dictionary<int, D2OClassDefinition> m_classes;
        private Dictionary<int, int> m_indexTable;
        private bool m_needToBeSync;
        private Dictionary<int, object> m_objects = new Dictionary<int, object>();
        private BigEndianWriter m_writer;
        private bool m_writing;
        private int m_nextIndex;

        /// <summary>
        /// Create a new instance of D2oWriter
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="readExistingFile"> </param>
        public D2OWriter(string filename, bool readDefinitionsOnly = false)
        {
            Filename = filename;

            if (!File.Exists(filename))
                CreateWrite(filename);
            else
                OpenWrite(readDefinitionsOnly);
        }

        public string BakFilename
        {
            get;
            set;
        }

        public string Filename
        {
            get;
            set;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (m_writing)
                EndWriting();
        }

        #endregion

        /// <summary>
        /// Create and flush and empty d2o file
        /// </summary>
        /// <param name="path"></param>
        public static void CreateEmptyFile(string path)
        {
            if (File.Exists(path))
                throw new Exception("File already exists, delete before overwrite");

            var writer = new BinaryWriter(File.OpenWrite(path));

            writer.Write("D2O");
            writer.Write((int)writer.BaseStream.Position + 4); // index table offset

            writer.Write(0); // index table len
            writer.Write(0); // class count

            writer.Flush();
            writer.Close();
        }

        private void CreateWrite(string filename)
        {
            //m_writer = new BigEndianWriter(File.Create(filename));

            m_indexTable = new Dictionary<int, int>();
            m_classes = new Dictionary<int, D2OClassDefinition>();
            m_objects = new Dictionary<int, object>();
            m_allocatedClassId = new Dictionary<Type, int>();
        }

        private void OpenWrite(bool readDefinitionsOnly = false)
        {
            ResetMembersByReading(readDefinitionsOnly);
        }

        private void ResetMembersByReading(bool readDefinitionsOnly = false)
        {
            var reader = new D2OReader(File.OpenRead(Filename));

            m_indexTable = readDefinitionsOnly ? new Dictionary<int, int>() : reader.Indexes;
            m_classes = reader.Classes;
            m_allocatedClassId = m_classes.ToDictionary(entry => entry.Value.ClassType, entry => entry.Key);
            m_objects = readDefinitionsOnly ? new Dictionary<int, object>() : reader.ReadObjects();

            reader.Close();
        }

        /// <summary>
        /// Start editing of the d2o file
        /// </summary>
        /// <param name="backupFile"></param>
        public void StartWriting(bool backupFile = true)
        {
            if (File.Exists(Filename))
            {
                if (backupFile)
                {
                    BakFilename = Filename + ".bak";
                    File.Copy(Filename, BakFilename, true);
                }
                File.Delete(Filename);
            }

            m_writer = new BigEndianWriter(File.Create(Filename));
            m_writing = true;
            lock (m_writingSync)
            {
                if (m_needToBeSync)
                {
                    ResetMembersByReading();
                }
            }
        }

        /// <summary>
        /// Stop editing the d2o file, flush the file and dispose ressources
        /// </summary>
        public void EndWriting()
        {
            lock (m_writingSync)
            {
                m_writer.Seek(0, SeekOrigin.Begin);

                m_writing = false;
                m_needToBeSync = false;

                WriteHeader();

                foreach (var obj in m_objects)
                {
                    if (!m_indexTable.ContainsKey(obj.Key))
                        m_indexTable.Add(obj.Key, (int)m_writer.BaseStream.Position);
                    else
                    {
                        m_indexTable[obj.Key] = (int)m_writer.BaseStream.Position;
                    }


                    WriteObject(m_writer, obj.Value, obj.Value.GetType());
                }

                WriteIndexTable();
                WriteClassesDefinition();
                WriteSearchTable();

                m_writer.Dispose();
            }
        }


        private void WriteHeader()
        {
            m_writer.WriteUTFBytes("D2O");
            m_writer.WriteInt(0); // allocate space to write the correct index table offset
        }

        private void WriteIndexTable()
        {
            var offset = (int)m_writer.BaseStream.Position;

            m_writer.Seek(3, SeekOrigin.Begin);
            m_writer.WriteInt(offset);

            m_writer.Seek(offset, SeekOrigin.Begin);
            m_writer.WriteInt(m_indexTable.Count * 8);

            foreach (var index in m_indexTable)
            {
                m_writer.WriteInt(index.Key);
                m_writer.WriteInt(index.Value);
            }
        }

        private void WriteClassesDefinition()
        {
            m_writer.WriteInt(m_classes.Count);

            foreach (var classDefinition in m_classes.Values)
            {
                classDefinition.Offset = (int)m_writer.BaseStream.Position;
                m_writer.WriteInt(classDefinition.Id);

                m_writer.WriteUTF(classDefinition.Name);
                m_writer.WriteUTF(classDefinition.PackageName);

                m_writer.WriteInt(classDefinition.Fields.Count);

                foreach (var field in classDefinition.Fields.Values)
                {
                    field.Offset = (int)m_writer.BaseStream.Position;
                    m_writer.WriteUTF(field.Name);
                    m_writer.WriteInt((int)field.TypeId);

                    foreach (var vectorType in field.VectorTypes)
                    {
                        m_writer.WriteUTF(ConvertNETTypeToAS3(vectorType.Item2));
                        m_writer.WriteInt((int)vectorType.Item1);
                    }
                }
            }
        }

        private void WriteSearchTable()
        {
            var positionBefore = (int)m_writer.BaseStream.Position;
            m_writer.WriteInt(0);

            foreach (var classDefinition in m_classes.Values)
            {
                foreach (var field in classDefinition.Fields)
                {
                    m_writer.WriteUTF(field.Value.Name);
                    m_writer.WriteInt((int)field.Value.Offset);
                    m_writer.WriteInt((int)field.Value.TypeId);
                    m_writer.WriteInt(classDefinition.Fields.Count);
                }
            }

            var positionAfter = (int)m_writer.BaseStream.Position;
            m_writer.Seek(positionBefore, SeekOrigin.Begin);
            m_writer.WriteInt(positionAfter - positionBefore - 4);
        }

        public void Write(object obj, Type type, int index)
        {
            if (!m_writing)
                StartWriting();

            lock (m_writingSync)
            {
                m_needToBeSync = true;

                if (!IsClassDeclared(type))
                    // if the class is not allocated then the class is not defined
                    DefineClassDefinition(type);

                if (m_objects.ContainsKey(index))
                    m_objects[index] = obj;
                else
                    m_objects.Add(index, obj);
            }
        }

        public void Write(object obj, Type type)
        {
            Write(obj, type, m_objects.Count > 0 ? m_objects.Keys.Max() + 1 : 1);
        }

        public void Write(object obj, int index)
        {
            Write(obj, obj.GetType(), index);
        }

        public void Write(object obj)
        {
            Write(obj, obj.GetType());
        }

        public void Write<T>(T obj)
        {
            Write(obj, typeof(T));
        }

        public void Write<T>(T obj, int index)
        {
            Write(obj, typeof(T), index);
        }

        public void Delete(int index)
        {
            lock (m_writingSync)
            {
                if (m_objects.ContainsKey(index))
                    m_objects.Remove(index);
            }
        }

        private bool IsClassDeclared(Type classType)
        {
            return m_allocatedClassId.ContainsKey(classType);
        }

        private int AllocateClassId(Type classType)
        {
            int id = m_allocatedClassId.Count > 0 ? m_allocatedClassId.Values.Max() + 1 : 1;
            AllocateClassId(classType, id);

            return id;
        }

        private void AllocateClassId(Type classType, int classId)
        {
            m_allocatedClassId.Add(classType, classId);
        }

        private void DefineClassDefinition(Type classType)
        {
            if (m_classes.Count(entry => entry.Value.ClassType == (classType)) > 0) // already define
                return;

            if (!m_allocatedClassId.ContainsKey(classType))
                AllocateClassId(classType);


            object[] attributes = classType.GetCustomAttributes(typeof(D2OClassAttribute), false);

            if (attributes.Length != 1)
                throw new Exception("The given class has no D2OClassAttribute attribute and cannot be wrote");

            string package = ((D2OClassAttribute)attributes[0]).PackageName;
            string name = !string.IsNullOrEmpty(((D2OClassAttribute)attributes[0]).Name)
                              ? ((D2OClassAttribute)attributes[0]).Name
                              : classType.Name;

            // add fields
            var fields = new List<D2OFieldDefinition>();
            foreach (var field in classType.GetFields())
            {
                if (field.GetCustomAttributes(typeof(D2OIgnore), false).Any() ||
                                                          field.IsStatic || field.IsPrivate || field.IsInitOnly)
                    continue;

                var attr = (D2OFieldAttribute)field.GetCustomAttributes(typeof(D2OFieldAttribute), false).SingleOrDefault();
                var fieldType = GetIdByType(field);
                var vectorTypes = GetVectorTypes(field.FieldType);
                var fieldName = attr != null ? attr.FieldName : field.Name;

                fields.Add(new D2OFieldDefinition(fieldName, fieldType, field, -1, vectorTypes));
            }

            foreach (var property in classType.GetProperties())
            {
                if (property.GetCustomAttributes(typeof(D2OIgnore), false).Any() || !property.CanWrite)
                    continue;

                var attr = (D2OFieldAttribute)property.GetCustomAttributes(typeof(D2OFieldAttribute), false).SingleOrDefault();
                var fieldType = GetIdByType(property);
                var vectorTypes = GetVectorTypes(property.PropertyType);
                var fieldName = attr != null ? attr.FieldName : property.Name;

                if (fields.Any(x => x.Name == fieldName))
                    continue;

                fields.Add(new D2OFieldDefinition(fieldName, fieldType, property, -1, vectorTypes));
            }

            m_classes.Add(m_allocatedClassId[classType],
                          new D2OClassDefinition(m_allocatedClassId[classType], name, package, classType, fields, -1));

            DefineAllocatedTypes(); // build class definition of allocated types that aren't define
        }

        private void DefineAllocatedTypes()
        {
            foreach (var allocatedClass in m_allocatedClassId.Where(entry => !m_classes.ContainsKey(entry.Value)).ToArray())
            {
                DefineClassDefinition(allocatedClass.Key);
            }
        }

        private D2OFieldType GetIdByType(FieldInfo field)
        {
            var fieldType = field.FieldType;

            if (field.GetCustomAttribute<I18NFieldAttribute>() != null)
                return D2OFieldType.I18N;

            return GetIdByType(fieldType);
        }
        private D2OFieldType GetIdByType(PropertyInfo property)
        {
            var fieldType = property.PropertyType;

            if (property.GetCustomAttribute<I18NFieldAttribute>() != null)
                return D2OFieldType.I18N;

            return GetIdByType(fieldType);
        }

        private D2OFieldType GetIdByType(Type fieldType)
        {
            if (fieldType == typeof(int))
                return D2OFieldType.Int;
            if (fieldType == typeof(bool))
                return D2OFieldType.Bool;
            if (fieldType == typeof(string))
                return D2OFieldType.String;
            if (fieldType == typeof(double) || fieldType == (typeof(float)))
                return D2OFieldType.Double;
            if (fieldType == typeof(uint))
                return D2OFieldType.UInt;
            if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                return D2OFieldType.List;

            int classId;
            if (!m_allocatedClassId.ContainsKey(fieldType))
            {
                classId = AllocateClassId(fieldType);
            }

            classId = m_allocatedClassId[fieldType];

            return (D2OFieldType)classId;
        }

        private Tuple<D2OFieldType, Type>[] GetVectorTypes(Type vectorType)
        {
            var ids = new List<Tuple<D2OFieldType, Type>>();

            if (vectorType.IsGenericType)
            {
                Type currentGenericType = vectorType;
                Type[] genericArguments = currentGenericType.GetGenericArguments();

                while (genericArguments.Length > 0)
                {
                    ids.Add(Tuple.Create(GetIdByType(genericArguments[0]), currentGenericType));

                    currentGenericType = genericArguments[0];
                    genericArguments = currentGenericType.GetGenericArguments();
                }
            }

            return ids.ToArray();
        }

        private string ConvertNETTypeToAS3(Type type)
        {
            switch (type.Name)
            {
                case "Int32":
                case "Int16":
                case "UInt16":
                    return "int";
                case "UInt32":
                    return "uint";
                case "Int64":
                case "UInt64":
                case "Single":
                case "Double":
                    return "Number";
                case "String":
                    return "String";
                default:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        return "Vector.<" + ConvertNETTypeToAS3(type.GetGenericArguments()[0]) + ">";
                    }

                    var @class = m_classes.Values.FirstOrDefault(x => x.ClassType == type);

                    if (@class == null)
                        throw new Exception(string.Format("Cannot found AS3 type associated to {0}", type));

                    return @class.PackageName + "::" + @class.Name;
            }
        }

        private void WriteObject(IDataWriter writer, object obj, Type type)
        {
            if (!m_allocatedClassId.ContainsKey(obj.GetType()))
            {
                DefineClassDefinition(obj.GetType());
            }

            D2OClassDefinition @class = m_classes[m_allocatedClassId[type]];

            writer.WriteInt(@class.Id);

            foreach (var field in @class.Fields)
            {
                object fieldValue = field.Value.GetValue(obj);

                WriteField(writer, field.Value.TypeId, field.Value, fieldValue);
            }
        }

        private void WriteField(IDataWriter writer, D2OFieldType fieldType, D2OFieldDefinition field, dynamic obj, int vectorDimension = 0)
        {
            switch (fieldType)
            {
                case D2OFieldType.Int:
                    WriteFieldInt(writer, Convert.ChangeType(obj,typeof(int)));
                    break;
                case D2OFieldType.Bool:
                    WriteFieldBool(writer, obj);
                    break;
                case D2OFieldType.String:
                    WriteFieldUTF(writer, obj);
                    break;
                case D2OFieldType.Double:
                    WriteFieldDouble(writer, obj);
                    break;
                case D2OFieldType.I18N:
                    WriteFieldI18n(writer, (int)obj);
                    break;
                case D2OFieldType.UInt:
                    WriteFieldUInt(writer, (uint)obj);
                    break;
                case D2OFieldType.List:
                    WriteFieldVector(writer, field, obj, vectorDimension);
                    break;
                default:
                    WriteFieldObject(writer, obj);
                    break;
            }
        }


        private void WriteFieldVector(IDataWriter writer, D2OFieldDefinition field, IList list, int vectorDimension)
        {
            if (list == null)
            {
                writer.WriteInt(0);
                return;
            }

            writer.WriteInt(list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                WriteField(writer, field.VectorTypes[vectorDimension].Item1, field, list[i], vectorDimension + 1);
            }
        }

        private void WriteFieldObject(IDataWriter writer, object obj)
        {
            if (obj == null)
                writer.WriteInt(NullIdentifier);
            else
            {
                if (!m_allocatedClassId.ContainsKey(obj.GetType()))
                {
                    DefineClassDefinition(obj.GetType());
                }

                WriteObject(writer, obj, obj.GetType());
            }
        }

        private static void WriteFieldInt(IDataWriter writer, int value)
        {
            writer.WriteInt(value);
        }

        private static void WriteFieldUInt(IDataWriter writer, uint value)
        {
            writer.WriteUInt(value);
        }

        private static void WriteFieldBool(IDataWriter writer, bool value)
        {
            writer.WriteBoolean(value);
        }

        private static void WriteFieldUTF(IDataWriter writer, string value)
        {
            if (value == null)
                value = string.Empty;

            writer.WriteUTF(value);
        }

        private static void WriteFieldDouble(IDataWriter writer, double value)
        {
            writer.WriteDouble(value);
        }

        private static void WriteFieldI18n(IDataWriter writer, int value)
        {
            writer.WriteInt(value);
        }
    }
}
