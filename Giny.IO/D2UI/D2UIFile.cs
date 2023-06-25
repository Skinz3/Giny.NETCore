using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2UI
{
    public class D2UIFile
    {
        public const string HEADER = "D2UI";

        private string Filepath
        {
            get;
            set;
        }
        public string XML
        {
            get;
            set;
        }
        public Dictionary<string, int> UIListPositions
        {
            get;
            set;
        }
        public byte[] LeftBytes
        {
            get;
            set;
        }
        public D2UIFile(string filePath)
        {
            this.Filepath = filePath;

            BigEndianReader reader = new BigEndianReader(File.ReadAllBytes(filePath));

            string header = reader.ReadUTF();

            if (header != HEADER)
            {
                throw new Exception("Malformated ui data file.");
            }

            XML = reader.ReadUTF();

            short definitionCount = reader.ReadShort();

            this.UIListPositions = new Dictionary<string, int>();

            for (int i = 0; i < definitionCount; i++)
            {
                UIListPositions.Add(reader.ReadUTF(), reader.ReadInt());
            }

            this.LeftBytes = reader.ReadBytes((int)reader.BytesAvailable);

            reader.Dispose();
        }
    }
}
