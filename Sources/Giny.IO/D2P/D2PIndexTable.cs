using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PIndexTable
    {
        public const int TableOffset = -24;

        public const SeekOrigin TableSeekOrigin = SeekOrigin.End;

        public D2PFile Container
        {
            get;
            set;
        }

        public int OffsetBase
        {
            get;
            set;
        }
        public int Size
        {
            get;
            set;
        }

        public int EntriesDefinitionOffset
        {
            get;
            set;
        }

        public int EntriesCount
        {
            get;
            set;
        }

        public int PropertiesOffset
        {
            get;
            set;
        }

        public int PropertiesCount
        {
            get;
            set;
        }

        public D2PIndexTable(D2PFile container)
        {
            this.Container = container;
        }

        public void ReadTable(IDataReader reader)
        {
            this.OffsetBase = reader.ReadInt();
            this.Size = reader.ReadInt();
            this.EntriesDefinitionOffset = reader.ReadInt();
            this.EntriesCount = reader.ReadInt();
            this.PropertiesOffset = reader.ReadInt();
            this.PropertiesCount = reader.ReadInt();
        }

        public void WriteTable(IDataWriter writer)
        {
            writer.WriteInt(this.OffsetBase);
            writer.WriteInt(this.Size);
            writer.WriteInt(this.EntriesDefinitionOffset);
            writer.WriteInt(this.EntriesCount);
            writer.WriteInt(this.PropertiesOffset);
            writer.WriteInt(this.PropertiesCount);
        }
    }
}
