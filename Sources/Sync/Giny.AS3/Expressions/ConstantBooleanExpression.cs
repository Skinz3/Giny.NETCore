using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ConstantBooleanExpression : ConstantExpression
    {
        public ConstantBooleanExpression(string line) : base(line)
        {
            this.Value = bool.Parse(line);
        }
        public static bool IsValid(string line)
        {
            return bool.TryParse(line, out bool n);
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

        public override string GetValueString()
        {
            return Value.ToString().ToLower();
        }
    }
}
