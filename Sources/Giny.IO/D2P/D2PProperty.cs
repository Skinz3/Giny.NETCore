using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2P
{
    public class D2PProperty
    {
        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public D2PProperty()
        {
        }

        public D2PProperty(string key, string property)
        {
            this.Key = key;
            this.Value = property;
        }

        public void ReadProperty(IDataReader reader)
        {
            this.Key = reader.ReadUTF();
            this.Value = reader.ReadUTF();
        }

        public void WriteProperty(IDataWriter writer)
        {
            writer.WriteUTF(this.Key);
            writer.WriteUTF(this.Value);
        }
     
    }
}
