using Giny.Core.IO;
using Giny.IO.DLM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PFastMapFile
    {
        public const long START_POSITION_END_OFFSET = 16;

        public string D2pFilePath
        {
            get;
            private set;
        }

        public Dictionary<int, CompressedMap> CompressedMaps
        {
            get;
            set;
        }

        public D2PFastMapFile(string d2pFilePath)
        {
            if (Path.GetExtension(d2pFilePath) != ".d2p")
                throw new ArgumentException("Invalid file type, " + d2pFilePath + " is not a .d2p file");

            this.CompressedMaps = new Dictionary<int, CompressedMap>();

            D2pFilePath = d2pFilePath;

            Deserialize();
        }
      
        private void Deserialize()
        {
            var reader = new BigEndianReader(File.ReadAllBytes(D2pFilePath));

            byte param1 = reader.ReadByte();
            byte param2 = reader.ReadByte();

            if ((param1 != 2) || (param2 != 1))
                throw new ArgumentException("Invalid file header, " + D2pFilePath + " is not a valid .d2p file");


            reader.BaseStream.Position = (reader.BaseStream.Length - START_POSITION_END_OFFSET);
            uint position = reader.ReadUInt();

            int compressedMapsCount = (int)(reader.ReadUInt());
            reader.BaseStream.Position = (position);

            CompressedMap compressedMap = null;

            for (int i = 0; i <= compressedMapsCount; i++)
            {
                compressedMap = new CompressedMap(reader, D2pFilePath);

                if (compressedMap.IsInvalidMap)
                    continue;

                CompressedMaps.Add(compressedMap.GetMapId(), compressedMap);
            }

            reader.Dispose();
        }


    }
}
