using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ConstantNumberExpression : ConstantExpression
    {
        public ConstantNumberExpression(string line) : base(line)
        {
            this.Value = double.Parse(line);
        }
        public static bool IsValid(string line)
        {
            return double.TryParse(line, out double n);
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
            if (Value.ToString() == "NaN")
            {
                return "double.NaN";
            }
            return Value.ToString().Replace(",", ".");
        }
    }
}
