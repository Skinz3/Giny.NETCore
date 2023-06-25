using System.Collections.Generic;
using Giny.IO.DLM.Elements;
using System.Linq;
using System.IO;
using System;
using Giny.Core.IO;

namespace Giny.IO.DLM
{
    public class DlmCell
    {
        public short CellId
        {
            get;
            set;
        }

        public List<BasicElement> Elements
        {
            get;
            set;
        }
        public DlmCell()
        {

        }
        public DlmCell(BigEndianReader _reader, sbyte mapVersion)
        {
            CellId = _reader.ReadShort();
            Elements = new List<BasicElement>();

            short elementCount = _reader.ReadShort();

            for (int i = 0; i < elementCount; i++)
            {
                sbyte elementType = _reader.ReadSByte();
                var element = BasicElement.GetElementFromType(elementType, _reader, mapVersion);
                Elements.Add(element);
            }

        }

        public void Serialize(BigEndianWriter writer, sbyte mapVersion)
        {
            writer.WriteShort(CellId);

            writer.WriteShort((short)Elements.Count);

            foreach (var element in Elements)
            {
                if (element is SoundElement)
                {
                    writer.WriteSByte(33);
                }
                else if (element is GraphicalElement)
                {
                    writer.WriteSByte(2);
                }

                element.Serialize(writer, mapVersion);
            }
        }
    }
}
