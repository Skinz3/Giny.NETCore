using Giny.Core;
using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.RawPatch
{
    public class RawPatchManager : Singleton<RawPatchManager>
    {
        public const string PATCH_EXTENSION = ".swf";

        public const string PATCHES_FOLDER_NAME = "SWF";

        Dictionary<string, byte[]> m_patches;

        public void Initialize()
        {
            string directory = Path.Combine(Environment.CurrentDirectory, PATCHES_FOLDER_NAME);
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            m_patches = new Dictionary<string, byte[]>();

            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) == PATCH_EXTENSION)
                {
                    m_patches.Add(Path.GetFileNameWithoutExtension(file), File.ReadAllBytes(file));
                }
                else
                {
                    Logger.Write("Unknown raw patch :" + file, Channels.Warning);
                }
            }
        }
        public byte[] GetRawPatch(string name)
        {
            return m_patches[name];
        }
    }
}
