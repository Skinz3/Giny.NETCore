using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OClassDefinition
    {
        public D2OClassDefinition(int id, string classname, string packagename, Type classType, IEnumerable<D2OFieldDefinition> fields, long offset)
        {
            Id = id;
            Name = classname;
            PackageName = packagename;
            ClassType = classType;
            Fields = fields.ToDictionary(entry => entry.Name);
            Offset = offset;
        }

        public Dictionary<string, D2OFieldDefinition> Fields
        {
            get;
            private set;
        }

        public int Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string PackageName
        {
            get;
            private set;
        }

        public Type ClassType
        {
            get;
            private set;
        }

        internal long Offset
        {
            get;
            set;
        }
    }
}
