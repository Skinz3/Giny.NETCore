using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class MethodCallExpression : BaseExpression
    {
        public VariableNameExpression[] MethodCallReferences
        {
            get;
            set;
        }
        public string MethodName
        {
            get;
            set;
        }
        /// <summary>
        /// key = cast
        /// </summary>
        public Dictionary<BaseExpression, AS3Type> Parameters
        {
            get;
            set;
        }
        public string[] ParametersString
        {
            get;
            set;
        }
        public bool IsBaseCall
        {
            get
            {
                var reference = MethodCallReferences.FirstOrDefault();

                if (reference != null)
                {
                    return reference.VariableName == "super";
                }
                else
                {
                    return this.MethodName == "super";
                }
            }
        }
        public AS3Type CastReturnType
        {
            get;
            private set;
        }
        public MethodCallExpression(AS3File file, string line, int i) : base(line)
        {
            string fullName = Regex.Match(line, @".+?(?=\()").Value;
            this.MethodName = fullName.Split('.').Last();

            int firstIndex = line.IndexOf('(');
            int lastIndex = line.LastIndexOf(')');

            var paramString = line.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

            if (paramString == string.Empty)
            {
                ParametersString = new string[0];
                Parameters = new Dictionary<BaseExpression, AS3Type>();
            }
            else
            {
                ParametersString = paramString.Split(',');
                Parameters = new Dictionary<BaseExpression, AS3Type>();

                foreach (var parameter in ParametersString)
                {
                    Parameters.Add(ExpressionManager.Construct(file, parameter, i), null); // todo parse casts.
                }
            }


            var tree = line.Replace("(" + paramString + ")", string.Empty).Split('.');
            tree = tree.Take(tree.Length - 1).ToArray();
            MethodCallReferences = new VariableNameExpression[tree.Length];

            for (int w = 0; w < tree.Length; w++)
            {
                MethodCallReferences[w] = new VariableNameExpression(tree[w]);
            }

        }
        public string GetMethodReferences()
        {
            StringBuilder result = new StringBuilder();

            foreach (var reference in MethodCallReferences)
            {
                result.Append(reference.VariableName + ".");
            }
            return result.ToString();
        }
        public string GetMethodFullName()
        {
            StringBuilder result = new StringBuilder();
            result.Append(GetMethodReferences());
            result.Append(MethodName);
            return result.ToString();
        }

        public static bool IsValid(string line)
        {
            return line.EndsWith(")") && line.StartsWith("(") == false;
        }
        public void CastCall(string rawType)
        {
            this.CastReturnType = new AS3Type(rawType);
        }
        public void CastParameter(int parameterIndex, string rawCast)
        {
            var key = GetParameter<BaseExpression>(parameterIndex);
            Parameters[key] = new AS3Type(rawCast);
        }
        public void SetExpression(int index, BaseExpression expression, string cast = null)
        {
            Parameters.Remove(GetParameter(0));
            Parameters.Add(expression, cast != null ? new AS3Type(cast) : null);
        }
        public BaseExpression GetParameter(int index)
        {
            return GetParameter<BaseExpression>(index);
        }
        public T GetParameter<T>(int index) where T : BaseExpression
        {
            return Parameters.Keys.ElementAt(index) as T;
        }

        public void RemoveParameter(int index)
        {
            Parameters.Remove(GetParameter<BaseExpression>(index));
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            foreach (var reference in MethodCallReferences)
            {
                if (reference.VariableName == variableName)
                {
                    reference.RenameVariable(variableName, newVariableName);
                }
            }
            foreach (var parameter in Parameters.Keys)
            {
                parameter.RenameVariable(variableName, newVariableName);
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {
            foreach (var parameter in Parameters.Keys)
            {
                parameter.RenameType(typeName, newTypeName);
            }

        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            if (this.MethodName == methodName)
            {
                this.MethodName = newMethodName;
            }

        }
    }
}
