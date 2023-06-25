using Giny.Core.IO;
using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PFile : IDisposable
    {
        public const string Extension = ".d2p";

        private readonly Dictionary<string, D2PEntry> m_entries = new Dictionary<string, D2PEntry>();
        private readonly List<D2PFile> m_links = new List<D2PFile>();
        private readonly Queue<D2PFile> m_linksToSave = new Queue<D2PFile>();
        private readonly List<D2PProperty> m_properties = new List<D2PProperty>();
        private readonly List<D2PDirectory> m_rootDirectories = new List<D2PDirectory>();
        private bool m_isDisposed;
        private IDataReader m_reader;

        public event Action<D2PFile, int> ExtractPercentProgress;

        public D2PIndexTable IndexTable
        {
            get;
            set;
        }

        public ReadOnlyCollection<D2PProperty> Properties
        {
            get
            {
                return this.m_properties.AsReadOnly();
            }
        }

        public IEnumerable<D2PEntry> Entries
        {
            get
            {
                return this.m_entries.Values;
            }
        }

        public ReadOnlyCollection<D2PFile> Links
        {
            get
            {
                return this.m_links.AsReadOnly();
            }
        }

        public ReadOnlyCollection<D2PDirectory> RootDirectories
        {
            get
            {
                return this.m_rootDirectories.AsReadOnly();
            }
        }

        public string FilePath
        {
            get;
            set;
        }

        public D2PEntry this[string fileName]
        {
            get
            {
                return this.m_entries[fileName];
            }
        }

        private void OnExtractPercentProgress(int percent)
        {
            Action<D2PFile, int> handler = this.ExtractPercentProgress;
            if (handler != null)
            {
                handler(this, percent);
            }
        }

        public D2PFile()
        {
            this.IndexTable = new D2PIndexTable(this);
            this.m_reader = new FastBigEndianReader(new byte[0]);
        }

        public D2PFile(string file)
        {
            this.FilePath = file;

            try
            {
                this.m_reader = new FastBigEndianReader(File.ReadAllBytes(file));
            }
            catch
            {
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                this.m_reader = new BigEndianReader(stream);
            }
            this.InternalOpen();
        }

        public void Dispose()
        {
            if (!this.m_isDisposed)
            {
                this.m_isDisposed = true;
                if (this.m_reader != null)
                {
                    //this.m_reader.Dispose();
                }
                if (this.m_links != null)
                {
                    foreach (D2PFile link in this.m_links)
                    {
                        link.Dispose();
                    }
                }
            }
        }

        public bool HasFilePath()
        {
            return !string.IsNullOrEmpty(this.FilePath);
        }

        private void InternalOpen()
        {
            if (this.m_reader.ReadByte() != 2 || this.m_reader.ReadByte() != 1)
            {
                throw new FileLoadException("Corrupted d2p header");
            }
            this.ReadTable();
            this.ReadProperties();
            this.ReadEntriesDefinitions();
        }

        private void ReadTable()
        {
            this.m_reader.Seek(-24, SeekOrigin.End);
            this.IndexTable = new D2PIndexTable(this);
            this.IndexTable.ReadTable(this.m_reader);
        }

        private void ReadProperties()
        {
            this.m_reader.Seek(this.IndexTable.PropertiesOffset, SeekOrigin.Begin);
            for (int i = 0; i < this.IndexTable.PropertiesCount; i++)
            {
                D2PProperty property = new D2PProperty();
                property.ReadProperty(this.m_reader);
                if (property.Key == "link")
                {
                    this.InternalAddLink(property.Value);
                }
                this.m_properties.Add(property);
            }
        }

        private void ReadEntriesDefinitions()
        {
            this.m_reader.Seek(this.IndexTable.EntriesDefinitionOffset, SeekOrigin.Begin);
            for (int i = 0; i < this.IndexTable.EntriesCount; i++)
            {
                D2PEntry entry = D2PEntry.CreateEntryDefinition(this, this.m_reader);
                this.InternalAddEntry(entry);
            }
        }

        public void AddProperty(D2PProperty property)
        {
            if (property.Key == "link")
            {
                this.InternalAddLink(property.Value);
            }
            this.InternalAddProperty(property);
        }

        public bool RemoveProperty(D2PProperty property)
        {
            if (property.Key == "link")
            {
                D2PFile link = this.m_links.FirstOrDefault((D2PFile entry) => Path.GetFullPath(this.GetLinkFileAbsolutePath(property.Value)) == Path.GetFullPath(entry.FilePath));
                if (link == null || !this.InternalRemoveLink(link))
                {
                    throw new Exception(string.Format("Cannot remove the associated link {0} to this property", property.Value));
                }
            }
            bool result;
            if (this.m_properties.Remove(property))
            {
                this.IndexTable.PropertiesCount--;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void InternalAddProperty(D2PProperty property)
        {
            this.m_properties.Add(property);
            this.IndexTable.PropertiesCount++;
        }

        public void AddLink(string linkFile)
        {
            this.InternalAddLink(linkFile);
            this.InternalAddProperty(new D2PProperty
            {
                Key = "link",
                Value = linkFile
            });
        }

        private void InternalAddLink(string linkFile)
        {
            string path = this.GetLinkFileAbsolutePath(linkFile);
            if (!File.Exists(path))
            {
                return;
                throw new FileNotFoundException(linkFile);
            }
            D2PFile link = new D2PFile(path);
            foreach (D2PEntry entry in link.Entries)
            {
                this.InternalAddEntry(entry);
            }
            this.m_links.Add(link);
        }

        private string GetLinkFileAbsolutePath(string linkFile)
        {
            string result;
            if (File.Exists(linkFile))
            {
                result = linkFile;
            }
            else
            {
                if (this.HasFilePath())
                {
                    string absolutePath = Path.Combine(Path.GetDirectoryName(this.FilePath), linkFile);
                    if (File.Exists(absolutePath))
                    {
                        result = absolutePath;
                        return result;
                    }
                }
                result = linkFile;
            }
            return result;
        }

        public bool RemoveLink(D2PFile file)
        {
            D2PProperty property = this.m_properties.FirstOrDefault((D2PProperty entry) => Path.GetFullPath(this.GetLinkFileAbsolutePath(entry.Value)) == Path.GetFullPath(file.FilePath));
            bool result2;
            if (property == null)
            {
                result2 = false;
            }
            else
            {
                bool result = this.InternalRemoveLink(file) && this.m_properties.Remove(property);
                result2 = result;
            }
            return result2;
        }

        private bool InternalRemoveLink(D2PFile link)
        {
            bool result;
            if (this.m_links.Remove(link))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public D2PEntry[] GetEntriesOfInstanceOnly()
        {
            return (
                from entry in this.m_entries.Values
                where entry.Container == this
                select entry).ToArray<D2PEntry>();
        }

        public D2PEntry GetEntry(string fileName)
        {
            return this.m_entries[fileName];
        }

        public D2PEntry TryGetEntry(string fileName)
        {
            D2PEntry entry;
            if (this.m_entries.TryGetValue(fileName, out entry))
            {
                return entry;
            }
            else
            {
                return null;
            }
        }

        public string[] GetFilesName()
        {
            return this.m_entries.Keys.ToArray<string>();
        }

        public void AddEntry(D2PEntry entry)
        {
            entry.State = D2pEntryState.Added;
            this.InternalAddEntry(entry);
            this.IndexTable.EntriesCount++;
        }

        private void InternalAddEntry(D2PEntry entry)
        {
            this.m_entries.Add(entry.FullFileName, entry);
            this.InternalAddDirectories(entry);
        }

        private void InternalAddDirectories(D2PEntry entry)
        {
            string[] directories = entry.GetDirectoriesName();
            if (directories.Length != 0)
            {
                D2PDirectory current = null;
                if (!this.HasDirectory(directories[0]))
                {
                    current = new D2PDirectory(directories[0]);
                    this.m_rootDirectories.Add(current);
                }
                else
                {
                    current = this.TryGetDirectory(directories[0]);
                }
                current.Entries.Add(entry);
                foreach (string directory in directories.Skip(1))
                {
                    if (!current.HasDirectory(directory))
                    {
                        D2PDirectory dir = new D2PDirectory(directory)
                        {
                            Parent = current
                        };
                        current.Directories.Add(dir);
                        current = dir;
                    }
                    else
                    {
                        current = current.TryGetDirectory(directory);
                    }
                    current.Entries.Add(entry);
                }
                entry.Directory = current;
            }
        }

        public bool RemoveEntry(D2PEntry entry)
        {
            bool result;
            if (entry.Container != this)
            {
                if (!entry.Container.RemoveEntry(entry))
                {
                    result = false;
                    return result;
                }
                if (!this.m_linksToSave.Contains(entry.Container))
                {
                    this.m_linksToSave.Enqueue(entry.Container);
                }
            }
            if (this.m_entries.Remove(entry.FullFileName))
            {
                entry.State = D2pEntryState.Removed;
                this.InternalRemoveDirectories(entry);
                if (entry.Container == this)
                {
                    this.IndexTable.EntriesCount--;
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void InternalRemoveDirectories(D2PEntry entry)
        {
            for (D2PDirectory current = entry.Directory; current != null; current = current.Parent)
            {
                current.Entries.Remove(entry);
                if (current.Parent != null && current.Entries.Count == 0)
                {
                    current.Parent.Directories.Remove(current);
                }
                else
                {
                    if (current.IsRoot && current.Entries.Count == 0)
                    {
                        this.m_rootDirectories.Remove(current);
                    }
                }
            }
        }

        public bool Exists(string fileName)
        {
            return this.m_entries.ContainsKey(fileName);
        }
        public bool Contains(string fileName)
        {
            return m_entries.ContainsKey(fileName);
        }

        public Dictionary<D2PEntry, byte[]> ReadAllFiles()
        {
            Dictionary<D2PEntry, byte[]> result = new Dictionary<D2PEntry, byte[]>();
            foreach (KeyValuePair<string, D2PEntry> entry in this.m_entries)
            {
                result.Add(entry.Value, this.ReadFile(entry.Value));
            }
            return result;
        }

        public byte[] ReadFile(D2PEntry entry)
        {
            byte[] result;
            if (entry.Container != this)
            {
                result = entry.Container.ReadFile(entry);
            }
            else
            {
                if (entry.Index >= 0 && this.IndexTable.OffsetBase + entry.Index >= 0)
                {
                    this.m_reader.Seek(this.IndexTable.OffsetBase + entry.Index, SeekOrigin.Begin);
                }
                byte[] data = entry.ReadEntry(this.m_reader);
                result = data;
            }
            return result;
        }
        public byte[] ReadFile(string fileName)
        {
            if (!this.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }
            D2PEntry entry = this.GetEntry(fileName);
            return this.ReadFile(entry);
        }

        public void ExtractFile(string fileName, bool overwrite = false)
        {
            if (!this.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }
            D2PEntry entry = this.GetEntry(fileName);
            string dest = Path.Combine("./", entry.FullFileName);
            if (!Directory.Exists(Path.GetDirectoryName(dest)))
            {
                Directory.CreateDirectory(dest);
            }
            this.ExtractFile(fileName, dest, overwrite);
        }

        public void ExtractFile(string fileName, string destination, bool overwrite = false)
        {
            byte[] bytes = this.ReadFile(fileName);
            FileAttributes attr = File.GetAttributes(destination);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                destination = Path.Combine(destination, Path.GetFileName(fileName));
            }
            if (File.Exists(destination) && !overwrite)
            {
                throw new InvalidOperationException(string.Format("Cannot overwrite {0}", destination));
            }
            if (!Directory.Exists(Path.GetDirectoryName(destination)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
            }
            File.WriteAllBytes(destination, bytes);
        }

        public void ExtractDirectory(string directoryName, string destination)
        {
            if (!this.HasDirectory(directoryName))
            {
                throw new InvalidOperationException(string.Format("Directory {0} does not exist", directoryName));
            }
            D2PDirectory directory = this.TryGetDirectory(directoryName);
            if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(destination, directory.FullName))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(destination, directory.FullName)));
            }
            foreach (D2PEntry entry in directory.Entries)
            {
                this.ExtractFile(entry.FullFileName, Path.Combine(destination, entry.FullFileName), false);
            }
            foreach (D2PDirectory pDirectory in directory.Directories)
            {
                this.ExtractDirectory(pDirectory.FullName, destination);
            }
        }

        public void ExtractAllFiles(string destination, bool overwrite = false, bool progress = false)
        {
            foreach (string[] dir in (
                from entry in this.m_entries
                select entry.Value.GetDirectoriesName()).Distinct<string[]>())
            {

                string dest = Path.Combine(Path.GetFullPath(destination), Path.Combine(dir));

                Directory.CreateDirectory(dest);
            }

            double i = 0.0;
            int progressPercent = 0;
            foreach (KeyValuePair<string, D2PEntry> entry2 in this.m_entries)
            {

                string dest = Path.Combine(Path.GetFullPath(destination), entry2.Value.FullFileName);

                File.WriteAllBytes(dest, this.ReadFile(entry2.Value));

                i += 1.0;
                if (progress)
                {
                    if ((int)(i / (double)this.m_entries.Count * 100.0) != progressPercent)
                    {
                        this.OnExtractPercentProgress(progressPercent = (int)(i / (double)this.m_entries.Count * 100.0));
                    }
                }
            }
        }

        public D2PEntry AddFile(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            string dest = file;
            if (this.HasFilePath())
            {
                dest = this.GetRelativePath(file, Path.GetDirectoryName(this.FilePath));
            }
            return this.AddFile(dest, bytes);
        }

        public D2PEntry AddFile(string fileName, byte[] data)
        {
            D2PEntry entry = new D2PEntry(this, fileName, data);
            this.AddEntry(entry);
            return entry;
        }

        /// <summary>
        /// !! This method is not working !!
        /// </summary>
        public bool RemoveFile(string file)
        {
            D2PEntry entry = this.TryGetEntry(file);
            return entry != null && this.RemoveEntry(entry);
        }
        /// <summary>
        /// !! This method is not working  !!
        /// </summary>
        public bool ModifyFile(string file, byte[] data)
        {
            D2PEntry entry = this.TryGetEntry(file);
            bool result;
            if (entry == null)
            {
                result = false;
            }
            else
            {
                entry.ModifyEntry(data);
                if (entry.Container != this && !this.m_linksToSave.Contains(entry.Container))
                {
                    this.m_linksToSave.Enqueue(entry.Container);
                }
                result = true;
            }
            return result;
        }

        private string GetRelativePath(string file, string directory)
        {
            Uri uri = new Uri(Path.GetFullPath(file));
            Uri currentUri = new Uri(Path.GetFullPath(directory));
            return currentUri.MakeRelativeUri(uri).ToString();
        }

        public void Save()
        {
            if (this.HasFilePath())
            {
                this.SaveAs(this.FilePath, true);
                return;
            }
            throw new InvalidOperationException("Cannot perform Save : No path defined, use SaveAs instead");
        }

        public void SaveAs(string destination, bool overwrite = true)
        {
            while (this.m_linksToSave.Count > 0)
            {
                D2PFile link = this.m_linksToSave.Dequeue();
                link.Save();
            }
            Stream stream;
            if (!File.Exists(destination))
            {
                stream = File.Create(destination);
            }
            else
            {
                if (!overwrite)
                {
                    throw new InvalidOperationException("Cannot perform SaveAs : file already exist, notify overwrite parameter to true");
                }
                stream = File.OpenWrite(destination);
            }
            using (BigEndianWriter writer = new BigEndianWriter(stream))
            {
                writer.WriteByte(2);
                writer.WriteByte(1);
                D2PEntry[] entries = this.GetEntriesOfInstanceOnly();
                int offset = 2;
                D2PEntry[] array = entries;
                for (int i = 0; i < array.Length; i++)
                {
                    D2PEntry entry = array[i];


                    byte[] data = this.ReadFile(entry);
                    entry.Index = (int)writer.Position - offset;
                    writer.WriteBytes(data);
                }
                int entriesDefOffset = (int)writer.Position;
                array = entries;
                for (int i = 0; i < array.Length; i++)
                {
                    D2PEntry entry = array[i];
                    entry.WriteEntryDefinition(writer);
                }
                int propertiesOffset = (int)writer.Position;
                foreach (D2PProperty property in this.m_properties)
                {
                    property.WriteProperty(writer);
                }
                this.IndexTable.OffsetBase = offset;
                this.IndexTable.EntriesCount = entries.Length;
                this.IndexTable.EntriesDefinitionOffset = entriesDefOffset;
                this.IndexTable.PropertiesCount = this.m_properties.Count;
                this.IndexTable.PropertiesOffset = propertiesOffset;
                this.IndexTable.Size = this.IndexTable.EntriesDefinitionOffset - this.IndexTable.OffsetBase;
                this.IndexTable.WriteTable(writer);
            }
        }

        public bool HasDirectory(string directory)
        {
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            bool result;
            if (directoriesName.Length == 0)
            {
                result = false;
            }
            else
            {
                D2PDirectory current = this.m_rootDirectories.SingleOrDefault((D2PDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result = false;
                }
                else
                {
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result = false;
                            return result;
                        }
                        current = current.TryGetDirectory(dir);
                    }
                    result = true;
                }
            }
            return result;
        }

        public D2PDirectory TryGetDirectory(string directory)
        {
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            D2PDirectory result;
            if (directoriesName.Length == 0)
            {
                result = null;
            }
            else
            {
                D2PDirectory current = this.m_rootDirectories.SingleOrDefault((D2PDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result = null;
                }
                else
                {
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result = null;
                            return result;
                        }
                        current = current.TryGetDirectory(dir);
                    }
                    result = current;
                }
            }
            return result;
        }

        public D2PDirectory[] GetDirectoriesTree(string directory)
        {
            List<D2PDirectory> result = new List<D2PDirectory>();
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            D2PDirectory[] result2;
            if (directoriesName.Length == 0)
            {
                result2 = new D2PDirectory[0];
            }
            else
            {
                D2PDirectory current = this.m_rootDirectories.SingleOrDefault((D2PDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result2 = new D2PDirectory[0];
                }
                else
                {
                    result.Add(current);
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result2 = result.ToArray();
                            return result2;
                        }
                        current = current.TryGetDirectory(dir);
                        result.Add(current);
                    }
                    result2 = result.ToArray();
                }
            }
            return result2;
        }
    }
}
