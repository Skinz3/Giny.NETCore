using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO.D2O
{
    public class D2OFieldAttribute : Attribute
    {
        public string FieldName
        {
            get;
            set;
        }

        public D2OFieldAttribute()
        {
        }

        public D2OFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
