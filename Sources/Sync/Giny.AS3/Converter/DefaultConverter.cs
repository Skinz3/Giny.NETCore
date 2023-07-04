using Giny.AS3;
using Giny.AS3.Converter;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Converter
{
    public abstract class DefaultConverter : AS3Converter
    {
        static string[] FORBIDDEN_VARIABLE_NAMES = new string[]
        {
            "object",
            "params",
            "operator"
        };
        public abstract bool WriteDefaultFieldValues
        {
            get;
        }
        public DefaultConverter(AS3File file) : base(file)
        {

        }
        public override void Prepare(Dictionary<string, AS3File> context)
        {

        }

        public override string GetNamespace()
        {
            return File.Package;
        }
        public override string GetClassName()
        {
            return File.ClassName;
        }

        public override string GetExtends()
        {
            return File.Extends;
        }
        public override string GetImplements()
        {
            if (File.Implementations.Length == 0)
            {
                return string.Empty;
            }
            return ", " + string.Join(",", File.Implementations);
        }

        public override string GetImports()
        {
            return "using " + string.Join(";" + Environment.NewLine + "using ", File.Imports) + ";";
        }
        protected override string GetIfHeader(IfExpression ifExpression)
        {
            return string.Format("if ({0})", GetExpression(ifExpression.ConditionExpression));
        }
        protected override string GetElseHeader(ElseExpression elseExpression)
        {
            if (elseExpression.Condition is EmptyExpression)
            {
                return "else";
            }
            else
            {
                return string.Format("else ({0})", GetExpression(elseExpression.Condition));
            }
        }
        protected override string GetMethodCallExpression(MethodCallExpression expression)
        {
            StringBuilder sb = new StringBuilder();

            if (expression.CastReturnType != null)
            {
                sb.Append("(" + expression.CastReturnType.RawType + ")");
            }

            sb.Append(VerifyVariableName(expression.GetMethodFullName()));
            sb.Append("(");

            string[] parameters = new string[expression.Parameters.Count];

            for (int i = 0; i < expression.Parameters.Count; i++)
            {
                var parameter = expression.Parameters.ElementAt(i);
                string cast = string.Empty;

                if (parameter.Value != null)
                {
                    cast = "(" + parameter.Value.RawType + ")";
                }
                parameters[i] = cast + GetExpression(parameter.Key);
            }
            sb.Append(string.Join(",", parameters));
            sb.Append(")");
            return sb.ToString();
        }


        protected override string GetMethodSignature(AS3Method method)
        {
            StringBuilder sb = new StringBuilder();

            List<string> parameters = new List<string>();

            foreach (var parameter in method.Parameters)
            {
                parameters.Add(string.Format("{0} {1}", GetConvertedType(parameter.Type), VerifyVariableName(parameter.Name)));
            }
            sb.Append(method.Accessor + " ");

            if (method.Modifiers != AS3ModifiersEnum.None)
            {
                sb.Append(method.Modifiers + " ");
            }

            if (method.IsConstructor == false)
                sb.Append(GetConvertedType(method.ReturnType) + " ");

            sb.Append(method.Name);
            sb.Append("(");

            if (parameters.Count > 0)
                sb.Append(string.Join(",", parameters));

            sb.Append(")");
            return sb.ToString();
        }

        protected override List<AS3Method> SelectMethodsToWrite()
        {
            return File.Methods.ToList();
        }
        protected override List<AS3Field> SelectFieldsToWrite()
        {
            return File.Fields.ToList();
        }

        protected override string GetType(AS3Type type)
        {
            return type.ToString();
        }
        protected override string GetAssignationExpression(AssignationExpression expression)
        {
            string cast = expression.Cast != null ? "(" + expression.Cast.RawType + ")" : string.Empty;
            return string.Format("{0} = {2}{1}", VerifyVariableName(expression.Target), GetExpression(expression.Value), cast);
        }

        protected override string GetAccessor(AS3AccessorsEnum accessor)
        {
            return accessor.ToString();
        }

        protected override string GetModifier(AS3ModifiersEnum modifier)
        {
            return modifier.ToString();
        }

        protected override string GetVariableDeclarationExpression(VariableDeclarationExpression ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetConvertedType(ex.Variable.Type) + " ");
            sb.Append(VerifyVariableName(ex.Variable.Name));

            if (!(ex.Value is EmptyExpression))
            {
                sb.Append(" = " + GetExpression(ex.Value));
            }

            return sb.ToString();
        }

        protected override string GetThrowExpression(ThrowExpression ex)
        {
            return ex.Line.Replace("Error", "System.Exception");
        }

        protected override string GetNumericIncrementationExpression(NumericIncrementationExpression expression)
        {
            return VerifyVariableName(expression.VariableName) + expression.Operator;
        }

        private bool IsGenericArray(string type)
        {
            return type.Contains('[');
        }
        protected override string GetAsExpression(AsExpression ex)
        {
            return string.Format("({0} as {1}).{2}", VerifyVariableName(ex.VariableName), GetConvertedType(ex.AsType), GetExpression(ex.Expression));
        }
        protected override string GetObjectInstantiationExpression(ObjectInstantiationExpression expression)
        {
            var convertedType = GetConvertedType(expression.VariableType);

            if (IsGenericArray(convertedType) == false)
            {
                return "new " + convertedType + "()";
            }
            else
            {
                return "new " + convertedType.Replace("[]", "[0]");
            }
        }

        protected override string GetForHeader(ForExpression ex)
        {
            return string.Format("for ({0};{1};{2})", GetExpression(ex.VariableExpression), GetExpression(ex.Minimum), GetExpression(ex.Incrementation));
        }

        protected override string GetField(AS3Field field)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(field.Accessor + " ");
            if (field.Modifiers != AS3ModifiersEnum.None)
                sb.Append(field.Modifiers + " ");

            sb.Append(GetConvertedType(field.Variable.Type) + " ");
            sb.Append(VerifyVariableName(field.Name));

            if (!(field.Value is EmptyExpression) && WriteDefaultFieldValues)
            {
                sb.Append(" = " + GetExpression(field.Value));
            }

            return sb.ToString();
        }
        protected override string GetVariableNameExpression(VariableNameExpression ex)
        {
            return VerifyVariableName(ex.GetVariableFullName());
        }
        protected override string GetConstantExpression(ConstantExpression expression)
        {
            return expression.GetValueString();
        }
        public override string GetConvertedType(AS3Type type)
        {
            switch (type.AS3TypeEnum)
            {
                case AS3TypeEnum.CustomOrUndefined:
                    return type.RawType;
                case AS3TypeEnum.String:
                    return "string";
                case AS3TypeEnum.Number:
                    return "double";
                case AS3TypeEnum.@int:
                    return "int";
                case AS3TypeEnum.@uint:
                    return "uint";
                case AS3TypeEnum.Boolean:
                    return "bool";
                case AS3TypeEnum.Object:
                    return "object";
                case AS3TypeEnum.Array:
                    return "Array";
                case AS3TypeEnum.Date:
                    return "DateTime";
                case AS3TypeEnum.Error:
                    return "Exception";
                case AS3TypeEnum.Function:
                    return "Action";
                case AS3TypeEnum.RegExp:
                    return "Regex";
                case AS3TypeEnum.XML:
                    return "Xml";
                case AS3TypeEnum.XMLList:
                    return "XmlList";
                case AS3TypeEnum.@void:
                    return "void";
            }
            throw new Exception("Unhandled type " + type.RawType);
        }

        protected override string GetUnchangedExpression(UnchangedExpression ex)
        {
            //  return "/*=>*/" + VerifyVariableName(ex.Line) + "/*<=*/";
            return VerifyVariableName(ex.Line);
        }
        protected override string GetConditionExpression(ConditionExpression ex)
        {
            return ConditionToString(ex.Condition);
        }
        private string ConditionToString(AS3Condition condition)
        {
            if (condition == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();

            string leftCast = condition.LeftCast != null ? "(" + GetConvertedType(condition.LeftCast) + ")" : string.Empty;
            string rightCast = condition.RightCast != null ? "(" + GetConvertedType(condition.RightCast) + ")" : string.Empty;

            sb.Append(leftCast + GetExpression(condition.Left) + " ");
            sb.Append(condition.Operator + " ");
            sb.Append(rightCast + GetExpression(condition.Right));

            if (condition.Next.NextOperator == AndOrEnum.None)
            {
                return sb.ToString();
            }
            else
            {
                string @operator = condition.Next.NextOperator == AndOrEnum.And ? "&&" : "||";

                sb.Append(" " + @operator + " ");
                sb.Append(ConditionToString(condition.Next.Condition));
                return sb.ToString();
            }
        }
        public override string VerifyVariableName(string variableName)
        {
            foreach (var forbidden in FORBIDDEN_VARIABLE_NAMES)
            {
                if (variableName == forbidden)
                {
                    return "@" + variableName;
                }
                else if (variableName == "this." + forbidden)
                {
                    return "this.@" + forbidden;
                }

                var split = variableName.Split('[');


                if (split.Length > 1)
                {
                    if (split[0] == forbidden)
                    {
                        return "@" + forbidden + "[" + split[1];
                    }
                }

                var split2 = variableName.Split('.');

                string final = string.Empty;

                if (split2.Length > 1)
                {
                    bool forbid = false;

                    for (int i = 0; i < split2.Length; i++)
                    {
                        if (split2[i] == forbidden)
                        {
                            forbid = true;
                            final += "@" + split2[i] + ".";
                        }
                        else
                        {
                            final += split2[i] + ".";
                        }
                    }
                    if (forbid)
                    {
                        final = final.Remove(final.Length - 1, 1);
                        return final;
                    }
                }



            }
            return variableName;
        }
    }
}