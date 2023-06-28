using Giny.Core.DesignPattern;
using Giny.Core.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.ELE
{
    /// <summary>
    /// Stump
    /// </summary>
    public class EleReader
    {
        public static Dictionary<int, EleGraphicalData> ReadElements(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                using (var reader = new BigEndianReader(stream))
                {
                    reader.Seek(0, SeekOrigin.Begin);
                    byte header = reader.ReadByte();
                    reader.Seek(0, SeekOrigin.Begin);


                    byte[] uncompress = Deflate(reader.BaseStream);
                    using (var reader2 = new BigEndianReader(uncompress))
                    {
                        return Elements.ReadFromStream(reader2);
                    }
                }
            }

        }

        private static byte[] Deflate(Stream compressedStream)
        {
            var outputStream = new MemoryStream();

            using (var inputStream = new InflaterInputStream(compressedStream))
            {
                inputStream.CopyTo(outputStream);
                outputStream.Position = 0;
                return outputStream.ToArray();
            }
        }

    }
}
