using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class UnchangedExpression : BaseExpression
    {
        public UnchangedExpression(string line) : base(line)
        {

        }
        public override void RenameType(string typeName, string newTypeName)
        {
            Line = Line.Replace(typeName, newTypeName);
        }

        public override void RenameVariable(string variableName, string newVariableName)
        {
            Line = Regex.Replace(Line, "(.*)\b"+variableName+"\b(.*)", newVariableName);
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            Line = Line.Replace(methodName, newMethodName);
        }
    }
}
