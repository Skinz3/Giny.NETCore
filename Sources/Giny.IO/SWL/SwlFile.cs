using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.SWL
{
    public class SwlFile
    {
        public byte[] Swf
        {
            get;
            private set;
        }

        public string[] Classes
        {
            get;
            private set;
        }

        public SwlFile(byte[] fileContent)
        {
            BigEndianReader reader = new BigEndianReader(fileContent);
            Deserialize(reader);
        }
        private void Deserialize(BigEndianReader reader)
        {
            byte header = reader.ReadByte();
            if (header != 76)
            {
                throw new Exception("Malformated library file (wrong header).");
            }
            byte version = reader.ReadByte();
            uint frameRate = reader.ReadUInt();
            int classesCount = reader.ReadInt();

            var classes = new List<string>();
            for (int i = 0; i < classesCount; i++)
            {
                classes.Add(reader.ReadUTF());
            }

            this.Classes = classes.ToArray();

            Swf = reader.ReadBytes((int)reader.BytesAvailable);
        }

        public void ExtractSwf(string output)
        {
            File.WriteAllBytes(output, Swf);
        }
    }
}
