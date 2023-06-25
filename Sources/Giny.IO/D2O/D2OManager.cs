using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OManager
    {
        private static Dictionary<string, D2OReader> m_readers = new Dictionary<string, D2OReader>();

        public const string Extension = ".d2o";

        public static void Initialize(string path)
        {
            foreach (var file in Directory.GetFiles(path).Where(x => Path.GetExtension(x).ToLower() == Extension))
            {
                m_readers.Add(Path.GetFileName(file), new D2OReader(file));
            }
        }

        public static IEnumerable<object> GetObjects(string filename)
        {
            return m_readers[filename].ReadObjects().Values;
        }

        public static IEnumerable<string> GetFilenames()
        {
            return m_readers.Keys;
        }
        public static bool ObjectExists(string filename, int id)
        {
            return m_readers[filename].ObjectExists(id);
        }
        public static T GetObject<T>(string fileName, int id) where T : IDataObject
        {
            return (T)m_readers[fileName].ReadObject(id);
        }
    }
}
