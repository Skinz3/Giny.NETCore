
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class AsExpression : BaseExpression
    {
        public BaseExpression Expression
        {
            get;
            private set;
        }
        public AS3Type AsType
        {
            get;
            private set;
        }
        public string VariableName
        {
            get;
            private set;
        }
        public AsExpression(AS3File file, string line, int i) : base(line)
        {
            line = line.Replace("((", "(");
            line = line.Replace("))", ")");
            int index = line.LastIndexOf('.');

            if (index == -1)
                return;
            var method = line.Substring(index + 1, line.Length - index - 1);
            var exp = ExpressionManager.Construct(file, method, i);

            if (!(exp is MethodCallExpression)) // trash parsing +1
            {
                return;
            }

            this.Expression = exp;
            string cast = line.Substring(0, index);


            string castContent = Regex.Match(cast, @"[(](.*)[)]").Groups[1].Value;

            var split2 = castContent.Split(new string[] { " as " }, StringSplitOptions.RemoveEmptyEntries);

            this.AsType = new AS3Type(split2[1]);
            this.VariableName = split2[0];

        }
        public void SetExpression(BaseExpression exp)
        {
            this.Expression = exp;
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            this.Expression.RenameMethodCall(methodName, newMethodName);
        }

        public override void RenameType(string typeName, string newTypeName)
        {
            if (AsType.RawType == typeName)
            {
                this.AsType = new AS3Type(newTypeName);
            }
        }

        public override void RenameVariable(string variableName, string newVariableName)
        {
            this.Expression.RenameVariable(variableName, newVariableName);

            if (this.VariableName == variableName)
            {
                this.VariableName = newVariableName;
            }
        }

        public static bool IsValid(string content)
        {
            return content.Contains(" as ");
        }
    }
}
