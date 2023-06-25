using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.MA3
{
    public class MA3Mount
    {
        public short Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Look
        {
            get;
            set;
        }

    }
    public class MA3MountFile
    {
        public MA3Mount[] Mounts
        {
            get;
            private set;
        }
        public MA3MountFile(byte[] tab)
        {
            using (BigEndianReader reader = new BigEndianReader(tab))
            {
                Deserialize(reader);
            }
        }

        private void Deserialize(BigEndianReader reader)
        {
            List<MA3Mount> mounts = new List<MA3Mount>();

            while (reader.BytesAvailable > 0)
            {
                MA3Mount mount = new MA3Mount()
                {
                    Id = reader.ReadShort(),
                    Name = reader.ReadUTF(),
                    Look = reader.ReadUTF(),
                };
                mounts.Add(mount);
            }
            Mounts = mounts.ToArray();
        }
    }
}
