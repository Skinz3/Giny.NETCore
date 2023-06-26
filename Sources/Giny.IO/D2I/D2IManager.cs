using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2I
{
    public class D2IManager
    {
        static D2IFile File
        {
            get;
            set;
        }
        public static void Initialize(string path)
        {
            File = new D2IFile(path);
        }
        public static string GetText(int id)
        {
            return File.GetText(id);
        }
    }
}
