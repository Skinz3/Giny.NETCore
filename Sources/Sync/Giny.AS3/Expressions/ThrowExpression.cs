using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ThrowExpression : BaseExpression
    {
        public string ExceptionContent
        {
            get;
            set;
        }
        public ThrowExpression(string line) : base(line)
        {
            this.ExceptionContent = Regex.Match(line, "[(](.*)[)]").Groups[1].Value;
        }

        public static bool IsValid(string line)
        {
            return line.StartsWith("throw");
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {

        }
        public override void RenameType(string typeName, string newTypeName)
        {

        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {

        }
    }
}
