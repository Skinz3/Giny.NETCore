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
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == I18NExtension)
                {
                    var lang = Path.GetFileNameWithoutExtension(file).Split('_')[1];
                    FilesPaths.Add(lang, file);
                }

            }
        }
        public static string GetText(int id, string lang = "fr")
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

            return Files[lang].GetText(id);

        }
    }
}
