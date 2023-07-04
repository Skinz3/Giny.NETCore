using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class NumericIncrementationExpression : BaseExpression
    {
        public string VariableName
        {
            get;
            private set;
        }
        public string Operator
        {
            get;
            private set;
        }
        public bool IsOperatorBeforeVariable
        {
            get;
            private set;
        }
        public NumericIncrementationExpression(string line) : base(line)
        {
            var match = Regex.Match(line, @"(?:\+\+|\+\=|\-\=)");
            this.Operator = match.Value;
            IsOperatorBeforeVariable = match.Index == 0;
            this.VariableName = line.Replace(match.Value, string.Empty);
        }
        public static bool IsValid(string line)
        {
            return Regex.Match(line, @"(?:\+\+|\+\=|\-\=)").Success;
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            if (VariableName == variableName)
            {
                VariableName = newVariableName;
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {

        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {

        }
    }
}
