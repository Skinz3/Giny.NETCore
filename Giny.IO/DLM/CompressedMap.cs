using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.DLM
{
    public class CompressedMap
    {
        public string D2pFilePath
        {
            get;
            private set;
        }

        public string IndexName
        {
            get;
            private set;
        }

        public uint Offset
        {
            get;
            private set;
        }

        public uint BytesCount
        {
            get;
            private set;
        }

        public bool IsInvalidMap
        {
            get;
            private set;
        }
        public CompressedMap(BigEndianReader reader, string d2pFilePath)
        {
            D2pFilePath = d2pFilePath;

            IndexName = reader.ReadUTF();

            if (IndexName == "link" || IndexName == "")
            {
                IsInvalidMap = true;
                return;
            }

            Offset = reader.ReadUInt() + 2;
            BytesCount = reader.ReadUInt();
        }

        public int GetMapId()
        {
            return int.Parse(IndexName.Substring(IndexName.IndexOf('/') + 1).Replace(".dlm", ""));
        }

    }
}
