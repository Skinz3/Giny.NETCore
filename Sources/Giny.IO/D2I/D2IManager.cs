using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2I
{
    public class D2IManager
    {
        private const string I18NExtension = ".d2i";

        private static Dictionary<string, string> FilesPaths
        {
            get;
            set;
        } = new Dictionary<string, string>();

        private static Dictionary<string, D2IFile> Files
        {
            get;
            set;
        } = new Dictionary<string, D2IFile>();

        public static void Initialize(string path)
        {
            FilesPaths.Clear();
            Files.Clear();
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == I18NExtension)
                {
                    var lang = Path.GetFileNameWithoutExtension(file).Split('_')[1];
                    FilesPaths.Add(lang, file);
                }

            }
        }
        public static IEnumerable<D2IEntry<int>> GetAllText(string lang = "fr")
        {
            InitializeLanguageFile(lang);
            return Files[lang].GetAllText();
        }

        private static void InitializeLanguageFile(string lang)
        {
            if (!Files.ContainsKey(lang))
            {
                if (FilesPaths.ContainsKey(lang))
                {
                    Files.Add(lang, new D2IFile(FilesPaths[lang]));
                }
                else
                {
                    throw new FileNotFoundException("Unable to find d2i file for lang " + lang);
                }
            }
        }
        public static void SetText(int id, string text, string lang = "fr")
        {
            InitializeLanguageFile(lang);
            Files[lang].SetText(id, text);
        }
        public static string GetText(int id, string lang = "fr")
        {
            InitializeLanguageFile(lang);
            return Files[lang].GetText(id);
        }
        public static void SaveAll()
        {
            foreach (var file in Files)
            {
                file.Value.Save();
            }
        }
    }
}
