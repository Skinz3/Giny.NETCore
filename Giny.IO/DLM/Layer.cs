using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Giny.IO.DLM
{
    public class Layer
    {
        public int LayerId
        {
            get;
            set;
        }
        public List<DlmCell> Cells
        {
            get;
            set;
        }

        public Layer()
        {

        }
        public Layer(BigEndianReader _reader, sbyte mapVersion)
        {
            if (mapVersion >= 9)
            {
                this.LayerId = _reader.ReadSByte();
            }
            else
            {
                this.LayerId = _reader.ReadInt();
            }
            int cellsCount = _reader.ReadShort();

            Cells = new List<DlmCell>();
            for (int i = 0; i < cellsCount; i++)
            {
                var cell = new DlmCell(_reader, mapVersion);
                Cells.Add(cell);
            }
        }

        public void Serialize(BigEndianWriter writer, sbyte mapVersion)
        {
            if (mapVersion >= 9)
            {
                writer.WriteSByte((sbyte)LayerId);
            }
            else
            {
                writer.WriteInt(LayerId);
            }

            writer.WriteShort((short)Cells.Count);

            foreach (var cell in Cells)
            {
                cell.Serialize(writer, mapVersion);
            }
        }
    }
}
