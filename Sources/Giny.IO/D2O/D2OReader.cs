using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Core.IO.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OReader
    {
        private const int NullIdentifier = unchecked((int)0xAAAAAAAA);

        /// <summary>
        /// Contains all assembly where the reader search d2o class
        /// </summary>
        public static List<Assembly> ClassesContainers = new List<Assembly>
            {
                typeof (D2OReader).Assembly
            };

        private static readonly Dictionary<Type, Func<object[], object>> objectCreators =
            new Dictionary<Type, Func<object[], object>>();

        private readonly string m_filePath;


        private int m_classcount;
        private Dictionary<int, D2OClassDefinition> m_classes;
        private int m_headeroffset;
        private Dictionary<int, int> m_indextable = new Dictionary<int, int>();
        private Dictionary<string, D2OSearchEntry> m_searchEntries = new Dictionary<string, D2OSearchEntry>();
        private int m_indextablelen;
        private IDataReader m_reader;
        private int m_contentOffset = 0;

        /// <summary>
        ///   Create and initialise a new D2o file
        /// </summary>
        /// <param name = "filePath">Path of the file</param>
        public D2OReader(string filePath)
            : this(new FastBigEndianReader(File.ReadAllBytes(filePath)))
        {
            m_filePath = filePath;
        }

        public D2OReader(Stream stream)
        {
            m_reader = new BigEndianReader(stream);
            Initialize();
        }

        public D2OReader(IDataReader reader)
        {
            m_reader = reader;
            Initialize();
        }

        public string FilePath
        {
            get
            {
                return m_filePath;
            }
        }

        public string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public int IndexCount
        {
            get
            {
                return m_indextable.Count;
            }
        }

        public int IndexTableOffset
        {
            get { return m_headeroffset; }
        }

        public Dictionary<int, D2OClassDefinition> Classes
        {
            get
            {
                return m_classes;
            }
        }

        public Dictionary<int, int> Indexes
        {
            get
            {
                return m_indextable;
            }
        }

        private void Initialize()
        {
            lock (m_reader)
            {
                ReadHeader();
                ReadIndexTable();
                ReadClassesTable();
                ReadSearchTable();
            }
        }

        private void ReadHeader()
        {
            var header = m_reader.ReadUTFBytes(3);

            if (header != "D2O")
            {
                m_reader.Seek(0, SeekOrigin.Begin);
                try
                {
                    header = m_reader.ReadUTF();
                }
                catch
                {
                    throw new Exception("Header doesn't equal the string \'D2O\' OR \'AKSF\' : Corrupted file");
                }
                if (header == "AKSF")
                {
                    var formatVersion = m_reader.ReadShort();
                    var len = m_reader.ReadInt();

                    m_reader.Seek(len, SeekOrigin.Current);
                    m_contentOffset = (int)m_reader.Position;

                    header = m_reader.ReadUTFBytes(3);
                    if (header != "D2O")
                        throw new Exception("Header doesn't equal the string \'D2O\' : Corrupted file (signed file)");
                }
                else
                    throw new Exception("Header doesn't equal the string \'D2O\' OR \'AKSF\' : Corrupted file");
            }
        }

        private void ReadIndexTable(bool isD2OS = false)
        {
            m_headeroffset = m_reader.ReadInt();
            m_reader.Seek(m_contentOffset + m_headeroffset, SeekOrigin.Begin); // place the reader at the beginning of the indextable
            m_indextablelen = m_reader.ReadInt();

            // init table index
            m_indextable = new Dictionary<int, int>(m_indextablelen / 8);
            for (var i = 0; i < m_indextablelen; i += 8)
            {
                m_indextable.Add(m_reader.ReadInt(), m_reader.ReadInt());
            }
        }

        private void ReadClassesTable()
        {
            var tempVectorTypes = new Dictionary<D2OFieldDefinition, List<Tuple<D2OFieldType, string>>>();
            m_classcount = m_reader.ReadInt();
            m_classes = new Dictionary<int, D2OClassDefinition>(m_classcount);
            for (int i = 0; i < m_classcount; i++)
            {
                int classId = m_reader.ReadInt();

                string classMembername = m_reader.ReadUTF();
                string classPackagename = m_reader.ReadUTF();

                Type classType = FindType(classMembername);

                int fieldscount = m_reader.ReadInt();
                var fields = new List<D2OFieldDefinition>(fieldscount);
                for (int l = 0; l < fieldscount; l++)
                {
                    string fieldname = m_reader.ReadUTF();
                    var fieldtype = (D2OFieldType)m_reader.ReadInt();
                    var field = classType.GetField(fieldname);

                    if (field == null)
                        throw new Exception(string.Format("Missing field '{0}' ({1}) in class '{2}'", fieldname, fieldtype, classType.Name));


                    var fieldDefinition = new D2OFieldDefinition(fieldname, fieldtype, field, m_reader.Position, new Tuple<D2OFieldType, Type>[0]);

                    var vectorTypes = new List<Tuple<D2OFieldType, object>>();
                    if (fieldtype == D2OFieldType.List)
                    {
                    addVectorType:

                        string name = m_reader.ReadUTF();
                        var id = (D2OFieldType)m_reader.ReadInt();

                        if (!tempVectorTypes.ContainsKey(fieldDefinition))
                            tempVectorTypes.Add(fieldDefinition, new List<Tuple<D2OFieldType, string>>());

                        tempVectorTypes[fieldDefinition].Add(Tuple.Create(id, name));

                        if (id == D2OFieldType.List)
                            goto addVectorType;
                    }

                    fields.Add(fieldDefinition);
                }

                m_classes.Add(classId,
                              new D2OClassDefinition(classId, classMembername, classPackagename, classType, fields,
                                                     m_reader.Position));
            }

            // find vector types
            foreach (var keyPair in tempVectorTypes)
            {
                keyPair.Key.VectorTypes = keyPair.Value.Select(tuple => Tuple.Create(tuple.Item1, FindNETType(tuple.Item2))).ToArray();
            }
        }

        private void ReadSearchTable()
        {
            var tableLen = m_reader.ReadInt();
            var contentOffset = (int)(m_reader.Position + 4 + tableLen);
            while (tableLen > 0)
            {
                var bytesAvailable = m_reader.BytesAvailable;

                var searchEntry = new D2OSearchEntry(m_reader.ReadUTF(), m_reader.ReadInt() + contentOffset, (D2OFieldType)m_reader.ReadInt(),
                    m_reader.ReadInt());

                if (!m_searchEntries.ContainsKey(searchEntry.FieldName))
                    m_searchEntries.Add(searchEntry.FieldName, searchEntry);

                tableLen -= (int)(bytesAvailable - m_reader.BytesAvailable);
            }
        }

        private Dictionary<int, object> BuildSortIndex(string field)
        {
            D2OSearchEntry searchEntry;
            if (!m_searchEntries.TryGetValue(field, out searchEntry))
                throw new Exception(string.Format("{0} is not a search field", field));

            var result = new Dictionary<int, object>();
            m_reader.Seek(searchEntry.FieldIndex, SeekOrigin.Begin);
            for (int i = 0; i < searchEntry.FieldCount; i++)
            {
                var obj = ReadField(m_reader, searchEntry.FieldType);
                var count = m_reader.ReadInt() / 4;
                for (int j = 0; j < count; j++)
                {
                    result.Add(m_reader.ReadInt(), obj);
                }

            }

            return result;
        }

        private Type FindNETType(string typeName)
        {
            switch (typeName)
            {
                case "int":
                    return typeof(int);
                case "uint":
                    return typeof(uint);
                case "Number":
                    return typeof(double);
                case "String":
                    return typeof(string);
                default:
                    if (typeName.StartsWith("Vector.<"))
                    {
                        return
                            typeof(List<>).MakeGenericType(
                                FindNETType(typeName.Remove(typeName.Length - 1, 1).Remove(0, "Vector.<".Length)));
                    }

                    var @class = m_classes.Values.FirstOrDefault(x => x.PackageName + "::" + x.Name == typeName);

                    if (@class == null)
                        throw new Exception(string.Format("Cannot found .NET type associated to {0}", typeName));

                    return @class.ClassType;
            }
        }

        private static Type FindType(string className)
        {
            IEnumerable<Type> correspondantTypes = from asm in ClassesContainers
                                                   let types = asm.GetTypes()
                                                   from type in types
                                                   where
                                                       type.Name.Equals(className, StringComparison.InvariantCulture) &&
                                                       type.HasInterface(typeof(IDataObject))
                                                   select type;

            return correspondantTypes.FirstOrDefault();
        }

        private bool IsTypeDefined(Type type)
        {
            return m_classes.Count(entry => entry.Value.ClassType == type) > 0;
        }

        /// <summary>
        ///   Get all objects that corresponding to T associated to his index
        /// </summary>
        /// <typeparam name = "T">Corresponding class</typeparam>
        /// <param name = "allownulled">True to adding null instead of throwing an exception</param>
        /// <returns></returns>
        public Dictionary<int, T> ReadObjects<T>(bool allownulled = false)
            where T : class
        {
            if (!IsTypeDefined(typeof(T)))
                throw new Exception("The file doesn't contain this class");

            var result = new Dictionary<int, T>(m_indextable.Count);

            using (var reader = CloneReader())
            {
                foreach (var index in m_indextable)
                {
                    reader.Seek(index.Value, SeekOrigin.Begin);

                    int classid = reader.ReadInt();

                    if (m_classes[classid].ClassType == typeof(T) ||
                        m_classes[classid].ClassType.IsSubclassOf(typeof(T)))
                    {
                        try
                        {
                            result.Add(index.Key, BuildObject(m_classes[classid], reader) as T);
                        }
                        catch
                        {
                            if (allownulled)
                                result.Add(index.Key, default(T));
                            else
                                throw;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///   Get all objects associated to his index
        /// </summary>
        /// <param name = "allownulled">True to adding null instead of throwing an exception</param>
        /// <returns></returns>
        public Dictionary<int, object> ReadObjects(bool allownulled = false, bool cloneReader = false)
        {
            var result = new Dictionary<int, object>(m_indextable.Count);

            IDataReader reader = cloneReader ? CloneReader() : m_reader;

            foreach (var index in m_indextable)
            {
                reader.Seek(index.Value + m_contentOffset, SeekOrigin.Begin);

                try
                {
                    result.Add(index.Key, ReadObject(index.Key, reader));
                }
                catch
                {
                    if (allownulled)
                        result.Add(index.Key, null);
                    else
                        throw;
                }
            }

            if (cloneReader)
                reader.Dispose();

            return result;
        }

        public IEnumerable<object> EnumerateObjects(bool cloneReader = true)
        {
            IDataReader reader = cloneReader ? CloneReader() : m_reader;

            foreach (var index in m_indextable)
            {
                reader.Seek(index.Value + m_contentOffset, SeekOrigin.Begin);
                yield return ReadObject(index.Key, reader);
            }

            if (cloneReader)
                reader.Dispose();
        }

        /// <summary>
        ///   Get an object from his index
        /// </summary>
        /// <param name="cloneReader">When sets to true it copies the reader to have a thread safe method</param>
        /// <returns></returns>
        public object ReadObject(int index, bool cloneReader = false)
        {
            if (cloneReader)
            {
                using (var reader = CloneReader())
                {
                    return ReadObject(index, reader);
                }
            }

            lock (m_reader)
            {
                return ReadObject(index, m_reader);
            }
        }

        public bool ObjectExists(int index)
        {
            return m_indextable.ContainsKey(index);
        }
        private object ReadObject(int index, IDataReader reader)
        {
            int offset = m_indextable[index];
            reader.Seek(offset + m_contentOffset, SeekOrigin.Begin);

            int classid = reader.ReadInt();

            object result = BuildObject(m_classes[classid], reader);

            return result;
        }

        private object BuildObject(D2OClassDefinition classDefinition, IDataReader reader)
        {
            if (!objectCreators.ContainsKey(classDefinition.ClassType))
            {
                Func<object[], object> creator = CreateObjectBuilder(classDefinition.ClassType,
                                                                     classDefinition.Fields.Select(
                                                                         entry => entry.Value.FieldInfo).ToArray());

                objectCreators.Add(classDefinition.ClassType, creator);
            }

            var values = new List<object>();
            foreach (D2OFieldDefinition field in classDefinition.Fields.Values)
            {
                object fieldValue = ReadField(reader, field, field.TypeId);

                if (fieldValue == null || field.FieldType.IsInstanceOfType(fieldValue))
                    values.Add((object)fieldValue);
                else if (fieldValue is IConvertible &&
                         field.FieldType.GetInterface("IConvertible") != null)
                {
                    try
                    {
                        if (fieldValue is int && ((int)fieldValue) < 0 && field.FieldType == typeof(uint))
                            values.Add((object)unchecked((uint)((int)fieldValue)));
                        else
                            values.Add((object)Convert.ChangeType(fieldValue, field.FieldType));
                    }
                    catch
                    {
                        throw new Exception(string.Format("Field '{0}.{1}' with value {2} is not of type '{3}'", classDefinition.Name,
                                                          field.Name, fieldValue, fieldValue.GetType()));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Field '{0}.{1}' with value {2} is not of type '{3}'", classDefinition.Name,
                                                      field.Name, fieldValue, fieldValue.GetType()));
                }
            }

            return objectCreators[classDefinition.ClassType](values.ToArray());
        }

        public T ReadObject<T>(int index, bool cloneReader = false)
            where T : class
        {
            if (cloneReader)
            {
                using (var reader = CloneReader())
                {
                    return ReadObject<T>(index, reader);
                }
            }

            return ReadObject<T>(index, m_reader);
        }

        private T ReadObject<T>(int index, IDataReader reader)
            where T : class
        {
            if (!IsTypeDefined(typeof(T)))
                throw new Exception("The file doesn't contain this class");

            int offset = 0;
            if (!m_indextable.TryGetValue(index, out offset)) throw new Exception(string.Format("Can't find Index {0} in {1}", index, this.FileName));

            reader.Seek(offset + m_contentOffset, SeekOrigin.Begin);

            int classid = reader.ReadInt();

            if (m_classes[classid].ClassType != typeof(T) && !m_classes[classid].ClassType.IsSubclassOf(typeof(T)))
                throw new Exception(string.Format("Wrong type, try to read object with {1} instead of {0}",
                                                    typeof(T).Name, m_classes[classid].ClassType.Name));

            return BuildObject(m_classes[classid], reader) as T;
        }

        public int FindFreeId()
        {
            return m_indextable.Keys.Max() + 1;
        }

        public Dictionary<int, D2OClassDefinition> GetObjectsClasses()
        {
            return m_indextable.ToDictionary(index => index.Key, index => GetObjectClass(index.Key));
        }


        /// <summary>
        /// Get the class corresponding to the object at the given index
        /// </summary>
        public D2OClassDefinition GetObjectClass(int index)
        {
            lock (m_reader)
            {
                int offset = m_indextable[index];
                m_reader.Seek(offset + m_contentOffset, SeekOrigin.Begin);

                int classid = m_reader.ReadInt();

                return m_classes[classid];
            }
        }

        private object ReadField(IDataReader reader, D2OFieldDefinition field, D2OFieldType typeId,
                                 int vectorDimension = 0)
        {
            switch (typeId)
            {
                case D2OFieldType.Int:
                    return ReadFieldInt(reader);
                case D2OFieldType.Bool:
                    return ReadFieldBool(reader);
                case D2OFieldType.String:
                    return ReadFieldUTF(reader);
                case D2OFieldType.Double:
                    return ReadFieldDouble(reader);
                case D2OFieldType.I18N:
                    return ReadFieldI18n(reader);
                case D2OFieldType.UInt:
                    return ReadFieldUInt(reader);
                case D2OFieldType.List:
                    return ReadFieldVector(reader, field, vectorDimension);
                default:
                    return ReadFieldObject(reader);
            }
        }

        private object ReadField(IDataReader reader, D2OFieldType typeId)
        {
            switch (typeId)
            {
                case D2OFieldType.Int:
                    return ReadFieldInt(reader);
                case D2OFieldType.Bool:
                    return ReadFieldBool(reader);
                case D2OFieldType.String:
                    return ReadFieldUTF(reader);
                case D2OFieldType.Double:
                    return ReadFieldDouble(reader);
                case D2OFieldType.I18N:
                    return ReadFieldI18n(reader);
                case D2OFieldType.UInt:
                    return ReadFieldUInt(reader);
                default:
                    return ReadFieldObject(reader);
            }
        }


        private object ReadFieldVector(IDataReader reader, D2OFieldDefinition field, int vectorDimension)
        {
            int count = reader.ReadInt();

            Type vectorType = field.FieldType;
            for (int i = 0; i < vectorDimension; i++)
            {
                vectorType = vectorType.GetGenericArguments()[0];
            }

            if (!objectCreators.ContainsKey(vectorType))
            {
                Func<object[], object> creator = CreateObjectBuilder(vectorType, new FieldInfo[0]);
                objectCreators.Add(vectorType, creator);
            }

            var result = objectCreators[vectorType](new object[0]) as IList;

            for (int i = 0; i < count; i++)
            {
                vectorDimension++;
                // i didn't found a way to have thez correct dimension so i just add "- 1"
                var value = ReadField(reader, field, field.VectorTypes[vectorDimension - 1].Item1, vectorDimension);

                result.Add(value);

                vectorDimension--;
            }

            return result;
        }

        private object ReadFieldObject(IDataReader reader)
        {
            int classid = reader.ReadInt();

            if (classid == NullIdentifier)
                return null;

            if (Classes.Keys.Contains(classid))
                return BuildObject(Classes[classid], reader);

            return null;
        }

        private static int ReadFieldInt(IDataReader reader)
        {
            return reader.ReadInt();
        }

        private static uint ReadFieldUInt(IDataReader reader)
        {
            return reader.ReadUInt();
        }

        private static bool ReadFieldBool(IDataReader reader)
        {
            return reader.ReadByte() != 0;
        }

        private static string ReadFieldUTF(IDataReader reader)
        {
            return reader.ReadUTF();
        }

        private static double ReadFieldDouble(IDataReader reader)
        {
            return reader.ReadDouble();
        }

        private static int ReadFieldI18n(IDataReader reader)
        {
            return reader.ReadInt();
        }

        internal IDataReader CloneReader()
        {
            lock (m_reader)
            {
                if (m_reader.Position > 0)
                    m_reader.Seek(0, SeekOrigin.Begin);

                Stream streamClone;

                if (m_reader is FastBigEndianReader)
                    return new FastBigEndianReader((m_reader as FastBigEndianReader).Buffer);
                else
                {
                    streamClone = new MemoryStream();
                    ((BigEndianReader)m_reader).BaseStream.CopyTo(streamClone);
                }

                return new BigEndianReader(streamClone);
            }
        }

        public void Close()
        {
            lock (m_reader)
            {
                m_reader.Dispose();
            }
        }

        private static Func<object[], object> CreateObjectBuilder(Type classType, params FieldInfo[] fields)
        {
            IEnumerable<Type> fieldsType = from entry in fields
                                           select entry.FieldType;

            var method = new DynamicMethod(Guid.NewGuid().ToString("N"), typeof(object),
                                           new[] { typeof(object[]) }.ToArray());
            ILGenerator ilGenerator = method.GetILGenerator();

            ilGenerator.DeclareLocal(classType);
            ilGenerator.DeclareLocal(classType);

            ilGenerator.Emit(OpCodes.Newobj, classType.GetConstructor(Type.EmptyTypes));
            ilGenerator.Emit(OpCodes.Stloc_0);
            for (int i = 0; i < fields.Length; i++)
            {
                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldc_I4, i);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                if (fields[i].FieldType.IsClass)
                    ilGenerator.Emit(OpCodes.Castclass, fields[i].FieldType);
                else
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, fields[i].FieldType);
                }

                ilGenerator.Emit(OpCodes.Stfld, fields[i]);
            }

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldloc_1);
            ilGenerator.Emit(OpCodes.Ret);

            return
                (Func<object[], object>)
                method.CreateDelegate(Expression.GetFuncType(new[] { typeof(object[]), typeof(object) }.ToArray()));
        }
    }
}
