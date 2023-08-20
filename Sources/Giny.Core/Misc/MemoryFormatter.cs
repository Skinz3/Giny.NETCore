using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Misc
{
    public class MemoryFormatter
    {
        public static string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int index = 0;
            double size = bytes;

            while (size >= 1024 && index < suffixes.Length - 1)
            {
                size /= 1024;
                index++;
            }

            return $"{size:0.##} {suffixes[index]}";
        }
    }
}
