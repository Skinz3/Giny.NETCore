using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class D2OClassAttribute : Attribute
    {
        public D2OClassAttribute(string name)
        {
            Name = name;
        }

        public D2OClassAttribute(string name, string packageName, params Type[] types)
        {
            Name = name;
            PackageName = packageName;
            Types = types;
        }

        public string Name
        {
            get;
            set;
        }

        public Type[] Types
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
