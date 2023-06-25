using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OClassAttribute : Attribute
    {
        public D2OClassAttribute(string name)
        {
            Name = name;
        }

        public D2OClassAttribute(string name, string packageName)
        {
            Name = name;
            PackageName = packageName;
        }

        public string Name
        {
            get;
            set;
        }

        public string PackageName
        {
            get;
            set;
        }
    }
}
