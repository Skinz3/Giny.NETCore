using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ORM.Attributes
{
    public class TypeOverrideAttribute : Attribute
    {
        public string NewType
        {
            get;
            set;
        }
        public TypeOverrideAttribute(string newType)
        {
            this.NewType = newType;
        }
    }
}
