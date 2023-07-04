using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public abstract class ConstantExpression : BaseExpression
    {
        public object Value
        {
            get;
            protected set;
        }
        public ConstantExpression(string line) : base(line)
        {

        }
        public abstract string GetValueString();
    }
}
