using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PDirectory
    {
        private D2PDirectory m_parent;
        private List<D2PEntry> m_entries = new List<D2PEntry>();
        private List<D2PDirectory> m_directories = new List<D2PDirectory>();

        public string Name
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public D2PDirectory Parent
        {
            get
            {
                return this.m_parent;
            }
            set
            {
                this.m_parent = value;
                this.UpdateFullName();
            }
        }

        public List<D2PEntry> Entries
        {
            get
            {
                return this.m_entries;
            }
            set
            {
                this.m_entries = value;
            }
        }

        public List<D2PDirectory> Directories
        {
            get
            {
                return this.m_directories;
            }
            set
            {
                this.m_directories = value;
            }
        }

        public bool IsRoot
        {
            get
            {
                return this.Parent == null;
            }
        }

        public D2PDirectory(string name)
        {
            this.Name = name;
            this.FullName = name;
        }

        private void UpdateFullName()
        {
            D2PDirectory current = this;
            this.FullName = current.Name;
            while (current.Parent != null)
            {
                this.FullName = this.FullName.Insert(0, current.Parent.Name + "\\");
                current = current.Parent;
            }
        }

        public bool HasDirectory(string directory)
        {
            return this.m_directories.Any((D2PDirectory entry) => entry.Name == directory);
        }

        public D2PDirectory TryGetDirectory(string directory)
        {
            return this.m_directories.SingleOrDefault((D2PDirectory entry) => entry.Name == directory);
        }

        public bool HasEntry(string entryName)
        {
            return this.m_entries.Any((D2PEntry entry) => entry.FullFileName == entryName);
        }

        public D2PEntry TryGetEntry(string entryName)
        {
            return this.m_entries.SingleOrDefault((D2PEntry entry) => entry.FullFileName == entryName);
        }
    }
}
