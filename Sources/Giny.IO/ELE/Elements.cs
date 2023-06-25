using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.ELE
{
    public class Elements
    {
        public byte Version { get; set; }

        public Dictionary<int, EleGraphicalData> GraphicalDatas { get; set; }

        private Dictionary<int, bool> _GfxJpgMap { get; set; }

        public Dictionary<int, bool> GfxJpgMap { get; set; }

        public Elements()
        {
            this.GraphicalDatas = new Dictionary<int, EleGraphicalData>();
            this.GfxJpgMap = new Dictionary<int, bool>();
            Indexes = new Dictionary<int, int>();
        }

        private Dictionary<int, int> Indexes;

        private BigEndianReader Reader;

        private byte header
        {
            get;
            set;
        }
        public static Dictionary<int, EleGraphicalData> ReadFromStream(BigEndianReader reader)
        {
            Elements instance = new Elements();
            instance.Reader = reader;
            instance.header = reader.ReadByte();
            instance.Version = reader.ReadByte();
            uint count = reader.ReadUInt();
            int edId;
            ushort skypLen = 0;
            for (int i = 0; i < count; i++)
            {
                if (instance.Version >= 9)
                {
                    skypLen = reader.ReadUShort();
                }
                edId = reader.ReadInt();

                if (instance.Version <= 8)
                {
                    instance.Indexes[edId] = (int)reader.Position;
                    ReadElement(instance, edId);
                }
                else
                {
                    instance.Indexes[edId] = (int)reader.Position;
                    reader.Seek((skypLen - 4), SeekOrigin.Current);
                }
            }

            if (instance.Version >= 8)
            {
                int gfxCount = reader.ReadInt();
                for (int i = 0; i < gfxCount; i++)
                {
                    instance.GfxJpgMap.Add(reader.ReadInt(), true);
                }
            }

            Dictionary<int, EleGraphicalData> result = new Dictionary<int, EleGraphicalData>();

            foreach (var index in instance.Indexes)
            {
                result.Add(index.Key, ReadElement(instance, index.Key));
            }
            return result;

        }
        private static EleGraphicalData ReadElement(Elements intance, int elementId)
        {
            int position = 0;

            if (!intance.Indexes.TryGetValue(elementId, out position))
            {
                return null;
            }
            intance.Reader.Seek(position, SeekOrigin.Begin);
            return EleGraphicalData.readElement(intance, intance.Reader, (int)elementId);
        }


    }
}
