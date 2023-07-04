using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class VariableDeclarationExpression : BaseExpression
    {
        public AS3Variable Variable
        {
            get;
            private set;
        }
        public BaseExpression Value
        {
            get;
            private set;
        }
        public VariableDeclarationExpression(AS3Variable variable, BaseExpression value) : base(variable.ToString() + "=" + value.ToString())
        {
            this.Variable = variable;
            this.Value = value;
        }
        public VariableDeclarationExpression(AS3File file, string line, int i) : base(line)
        {
            this.Variable = new AS3Variable(Regex.Match(line, @"\w+(?=.?:)").Value, Regex.Match(line, @"(?<=:)(\w+)").Value);
            string value = Regex.Match(line, @"(?<=\=).*$").Value;
            this.Value = ExpressionManager.Construct(file, value, i);
        }
        public static bool IsValid(string line)
        {
            return line.StartsWith("var ");
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            if (Variable.Name == variableName)
            {
                Variable.Name = newVariableName;
            }

            Value.RenameVariable(variableName, newVariableName);
        }

        public override void RenameType(string typeName, string newTypeName)
        {
            if (Variable.Type.RawType == typeName)
            {
                Variable.Type.RawType = newTypeName;
            }

            Value.RenameType(typeName, newTypeName);
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            Value.RenameMethodCall(methodName, newMethodName);
        }
    }
}
