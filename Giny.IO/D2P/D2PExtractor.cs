using Giny.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PExtractor
    {
        public static void Extract(string d2pDirectory, string ouputDirectory)
        {
            foreach (var file in Directory.GetFiles(d2pDirectory))
            {
                Logger.Write(Path.GetFileName(file));
                D2PFile d2pFile = new D2PFile(file);
                d2pFile.ExtractAllFiles(ouputDirectory);
            }
        }
    }
}
