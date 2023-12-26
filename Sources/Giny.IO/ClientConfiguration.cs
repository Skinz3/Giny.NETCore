using Giny.Core.Logging;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Giny.IO
{
    [XmlRoot("LangFile")]
    public class LangFile
    {
        [XmlElement("entry")]
        public Entry[] Entries { get; set; }
    }

    public class Entry
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class ClientConfiguration
    {
        private LangFile LangFile
        {
            get;
            set;
        }
        private string Filepath
        {
            get;
            set;
        }
        public ClientConfiguration(string path)
        {
            Filepath = path;
            Open();
        }

        private void Open()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LangFile));

            using (FileStream fileStream = new FileStream(Filepath, FileMode.Open))
            {
                LangFile = (LangFile)serializer.Deserialize(fileStream);
            }

        }

        public bool Set(string key, string value)
        {
            var entry = LangFile.Entries.FirstOrDefault(x => x.Key == key);

            if (entry != null)
            {
                entry.Value = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LangFile));
            using (FileStream fileStream = new FileStream(Filepath, FileMode.Create))
            {
                serializer.Serialize(fileStream, LangFile);
            }
        }
    }
}
