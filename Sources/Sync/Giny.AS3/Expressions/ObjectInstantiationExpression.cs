using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ObjectInstantiationExpression : BaseExpression
    {
        public AS3Type VariableType
        {
            get;
            set;
        }
        public BaseExpression[] ConstructorParameters
        {
            get;
            set;
        }
        public string[] ConstructorParametersString
        {
            get;
            set;
        }
        public ObjectInstantiationExpression(AS3File file, string line, int i) : base(line)
        {
            var type = Regex.Match(line, @"[new](.*)[(]").Groups[1].Value.Remove(0, 3);
            VariableType = new AS3Type(type);
            string parameters = Regex.Match(line, @"[(](.*)[)]").Groups[1].Value;

            if (parameters == string.Empty)
            {
                ConstructorParameters = new BaseExpression[0];
                ConstructorParametersString = new string[0];
            }
            else
            {

                ConstructorParametersString = parameters.Split(',');
                ConstructorParameters = new BaseExpression[ConstructorParametersString.Length];
                int n = 0;
                foreach (var parameter in ConstructorParametersString)
                {
                    ConstructorParameters[n] = ExpressionManager.Construct(file, parameter, i);
                    n++;
                }
            }
        }
        public static bool IsValid(string line)
        {
            return line.StartsWith("new ");
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            foreach (var parameter in ConstructorParameters)
            {
                parameter.RenameVariable(variableName, newVariableName);
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {
            if (VariableType.RawType == typeName)
            {
                VariableType.RawType = newTypeName;
            }

            foreach (var parameter in ConstructorParameters)
            {
                parameter.RenameType(typeName, newTypeName);
            }
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            foreach (var parameter in ConstructorParameters)
            {
                parameter.RenameMethodCall(methodName, newMethodName);
            }
        }
    }
}
