using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.MA3
{
    public class MA3Item
    {
        public int Id
        {
            get;
            set;
        }
        public short TypeId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public short Level
        {
            get;
            set;
        }

        public int Skin
        {
            get;
            set;
        }

        public int IconId
        {
            get;
            set;
        }

        public string Look
        {
            get;
            set;
        }

        public bool IsCameleon
        {
            get;
            set;
        }
    }
    public class MA3ItemFile
    {
        public MA3Item[] Items
        {
            get;
            private set;
        }
        public MA3ItemFile(byte[] tab)
        {
            using (BigEndianReader reader = new BigEndianReader(tab))
            {
                Deserialize(reader);
            }
        }

        private void Deserialize(BigEndianReader reader)
        {
            List<MA3Item> items = new List<MA3Item>();

            while (reader.BytesAvailable > 0)
            {
                MA3Item item = new MA3Item()
                {
                    Id = (int)reader.ReadUInt(),
                    TypeId = reader.ReadShort(),
                    Name = reader.ReadUTF(),
                    Level = reader.ReadShort(),
                    IconId = (int)reader.ReadUInt(),
                    Skin = (int)reader.ReadUInt(),
                    Look = reader.ReadUTF(),
                };
                items.Add(item);
            }
            Items = items.ToArray();
        }
    }
}
