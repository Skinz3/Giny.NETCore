using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ConstantIntExpression : ConstantExpression
    {
        public ConstantIntExpression(string line) : base(line)
        {
            this.Value = int.Parse(line);
        }
        public static bool IsValid(string line)
        {
            return int.TryParse(line, out int n);
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
            return Value.ToString();
        }
    }
}
