using Giny.Core.DesignPattern;
using Giny.Core.IO;
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
        private BigEndianReader m_reader;
        private Stream m_stream;

        public EleReader(string filePath)
        {
            this.m_stream = File.OpenRead(filePath);
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public EleReader(Stream stream)
        {
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public Dictionary<int, EleGraphicalData> ReadElements()
        {
            this.m_reader.Seek(0, SeekOrigin.Begin);
            int header = (int)this.m_reader.ReadByte();
            this.m_reader.Seek(0, SeekOrigin.Begin);

            MemoryStream output = new MemoryStream();
            Deflate(new MemoryStream(this.m_reader.ReadBytes((int)this.m_reader.BytesAvailable)), output);
            byte[] uncompress = output.ToArray();
            this.m_reader = new BigEndianReader(uncompress);
            return Elements.ReadFromStream(this.m_reader);
        }
        private void ChangeStream(Stream stream)
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        [WIP("working?")]
        private static void Deflate(Stream input, Stream output)
        {
            using (DeflateStream zoutput = new DeflateStream(output, CompressionMode.Decompress))
            {
                using (BinaryReader inputReader = new BinaryReader(input))
                {
                    zoutput.Write(inputReader.ReadBytes((int)input.Length), 0, (int)input.Length);
                    zoutput.Flush();
                }
            }
        }

        public void Dispose()
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
        }
    }
}
