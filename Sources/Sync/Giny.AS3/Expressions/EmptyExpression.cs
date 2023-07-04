using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class EmptyExpression : BaseExpression
    {
        public EmptyExpression() : base(string.Empty)
        {

        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {

        }

        public override void RenameType(string typeName, string newTypeName)
        {

        }

        public override void RenameVariable(string variableName, string newVariableName)
        {

        }
    }
}
