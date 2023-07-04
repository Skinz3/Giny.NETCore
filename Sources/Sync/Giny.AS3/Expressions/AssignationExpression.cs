using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class AssignationExpression : BaseExpression
    {
        public string Target
        {
            get;
            private set;
        }
        public BaseExpression Value
        {
            get;
            private set;
        }
        public AS3Type Cast
        {
            get;
            set;
        }
        public AssignationExpression(AS3File file, string line, int i) : base(line)
        {
            this.Target = Regex.Match(line, @".+?(?=\=)").Value.Trim();
            string value = Regex.Match(line, @"(?<=\=).*$").Value.Trim();

            if (value.StartsWith("("))
            {
                Cast = new AS3Type(value.Split(')')[0].Substring(1));
                value = Regex.Match(value, @"(?<=\)).*$").Value;
            }
            Value = ExpressionManager.Construct(file, value, i);
        }
        public AssignationExpression(string target, BaseExpression value, AS3Type cast = null) : base(string.Empty)
        {
            this.Target = target;
            this.Value = value;
            this.Cast = cast;
        }
        public static bool IsValid(string line)
        {
            return line.Contains("=");
        }

        public override void RenameVariable(string variableName, string newVariableName)
        {
            if (Target == "this." + variableName)
            {
                Target = "this." + newVariableName;
            }
            if (Target == variableName)
            {
                Target = newVariableName;
            }
            Value.RenameVariable(variableName, newVariableName);
        }

        public override void RenameType(string typeName, string newTypeName)
        {
            Value.RenameType(typeName, newTypeName);

            if (Cast != null && Cast.RawType == typeName)
            {
                Cast.RawType = newTypeName;
            }
        }

        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            Value.RenameMethodCall(methodName, newMethodName);
        }
    }
}
