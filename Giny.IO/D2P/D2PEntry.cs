using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PEntry
    {
        private byte[] m_newData;

        public D2PFile Container
        {
            get;
            set;
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(FullFileName);
            }
        }

        public string FullFileName
        {
            get;
            set;
        }

        public D2PDirectory Directory
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int Offset
        {
            get
            {
                return Index + 2;
            }
        }
        public D2pEntryState State
        {
            get;
            set;
        }

        private D2PEntry(D2PFile container)
        {
            this.Container = container;
            this.Index = -1;
        }

        public D2PEntry(D2PFile container, string fileName)
        {
            this.Container = container;
            this.FullFileName = fileName;
            this.Index = -1;
        }

        public D2PEntry(D2PFile container, string fileName, byte[] data)
        {
            this.Container = container;
            this.FullFileName = fileName;
            this.m_newData = data;
            this.State = D2pEntryState.Added;
            this.Size = data.Length;
            this.Index = -1;
        }

        public static D2PEntry CreateEntryDefinition(D2PFile container, IDataReader reader)
        {
            D2PEntry entry = new D2PEntry(container);
            entry.ReadEntryDefinition(reader);
            return entry;
        }

        public void ReadEntryDefinition(IDataReader reader)
        {
            this.FullFileName = reader.ReadUTF();
            this.Index = reader.ReadInt();
            this.Size = reader.ReadInt();
        }

        public void WriteEntryDefinition(IDataWriter writer)
        {
            if (this.Index == -1)
            {
                throw new InvalidOperationException("Invalid entry, index = -1");
            }
            writer.WriteUTF(this.FullFileName);
            writer.WriteInt(this.Index);
            writer.WriteInt(this.Size);
        }

        public byte[] ReadEntry(IDataReader reader)
        {
            if (this.State == D2pEntryState.Removed)
            {
                throw new InvalidOperationException("Cannot read a deleted entry");
            }
            byte[] result;
            if (this.State == D2pEntryState.Dirty || this.State == D2pEntryState.Added)
            {
                result = this.m_newData;
            }
            else
            {
                result = reader.ReadBytes(this.Size);
            }
            return result;
        }
        public int GetMapId()
        {
            return int.Parse(this.FileName.Replace(".dlm", string.Empty));
        }
        public void ModifyEntry(byte[] data)
        {
            this.m_newData = data;
            this.Size = data.Length;
            this.State = D2pEntryState.Dirty;
        }

        public string[] GetDirectoriesName()
        {
            return Path.GetDirectoryName(this.FullFileName).Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
