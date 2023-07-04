using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3
{
    public class AS3Method
    {
        public string Name
        {
            get;
            private set;
        }
        public List<AS3Variable> Parameters
        {
            get;
            private set;
        }
        public AS3AccessorsEnum Accessor
        {
            get;
            private set;
        }
        public AS3ModifiersEnum Modifiers
        {
            get;
            private set;
        }
        private BlockDefinition BlockDefinition
        {
            get;
            set;
        }
        public List<BaseExpression> Expressions
        {
            get;
            set;
        }
        public int LinesCountWithSignature
        {
            get
            {
                return BlockDefinition.LinesCount + 3; // '{' and '}' and signature lines
            }
        }
        public AS3Type ReturnType
        {
            get;
            set;
        }
        public bool IsConstructor
        {
            get
            {
                return ReturnType.RawType == string.Empty;
            }
        }

        private AS3File File
        {
            get;
            set;
        }
        public AS3Method(string name, List<AS3Variable> parameters, AS3Type returnVariable, List<BaseExpression> expressions, AS3AccessorsEnum accessor, AS3ModifiersEnum modifiers)
        {
            this.Name = name;
            this.Parameters = parameters;
            this.ReturnType = returnVariable;
            this.Expressions = expressions;
            this.Accessor = accessor;
            this.Modifiers = modifiers;
            this.File = null;
            this.BlockDefinition = new BlockDefinition();
        }

        public void SetModifiers(AS3ModifiersEnum modifiers)
        {
            this.Modifiers = modifiers;
        }

        public void RenameMethodCall(string methodName, string newMethodName)
        {
            foreach (var expression in Expressions)
            {
                expression.RenameMethodCall(methodName, newMethodName);
            }
        }
        /// <summary>
        /// remove?
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="rawCastType"></param>
        public void AddCastAfterMethodCall(string methodName, string rawCastType)
        {
            foreach (var expression in Expressions)
            {
                var methodCallExp = expression as MethodCallExpression;

                if (methodCallExp != null && methodCallExp.MethodName == methodName)
                {
                    methodCallExp.Parameters[methodCallExp.Parameters.First().Key] = new AS3Type(rawCastType);
                }
            }
        }
        public void RenameType(string typeName, string newTypeName)
        {
            foreach (var parameter in Parameters)
            {
                if (parameter.Type.RawType == typeName)
                {
                    parameter.Type.RawType = newTypeName;
                }
            }

            foreach (var expression in Expressions)
            {
                expression.RenameType(typeName, newTypeName);
            }
        }

        public void ReplaceUnchangedExpression(string toReplace, string replaceBy)
        {
            RenameVariable(toReplace, replaceBy);
        }
        public void RenameVariable(string variableName, string newVariableName)
        {
            foreach (var parameter in Parameters)
            {
                if (parameter.Name == variableName)
                {
                    parameter.Name = newVariableName;
                }
            }

            foreach (var expression in Expressions)
            {
                expression.RenameVariable(variableName, newVariableName);
            }
        }

        public AS3Method(AS3File file, string[] lines, int i)
        {
            this.File = file;
            string declarationLine = lines[i].Trim();
            Name = Regex.Match(declarationLine, @"\w+(?=.?\()").Value;
            Accessor = Regex.Match(declarationLine, @"(?:public|private|protected)").Value.ParseEnum<AS3AccessorsEnum>();
            Modifiers = Regex.Match(declarationLine, @"(?:const|static|override|virtual)").Value.ParseEnum<AS3ModifiersEnum>();
            BlockDefinition = AS3Helper.GetBlockDefinitionForMethods(file, i);
            ReturnType = new AS3Type(Regex.Match(declarationLine, @"(?<=: )(\w+)").Value);
            Parameters = AS3Helper.GetParameters(file, lines, i);
            this.Expressions = AS3Helper.BuildExpressions(file, BlockDefinition);
        }
        public void Rename(string newName)
        {
            this.Name = newName;
        }
        public override string ToString()
        {
            return Name + "()";
        }
        public static void DecapsulateMethod(AS3File file, string methodName)
        {
            var method = file.GetMethod(x => x.Name == methodName);

            List<BaseExpression> expressions = new List<BaseExpression>();

            foreach (var initialExpression in method.Expressions)
            {
                MethodCallExpression exp = initialExpression as MethodCallExpression;

                if (exp != null)
                {
                    var target = file.GetMethod(x => x.Name == exp.GetMethodFullName());

                    if (target != null)
                    {
                        foreach (var ex in target.Expressions)
                        {
                            expressions.Add(ex);
                        }
                    }
                    else
                    {
                        expressions.Add(initialExpression);
                    }

                }
                else
                {
                    expressions.Add(initialExpression);
                }

            }

            method.Expressions = expressions;
        }
    }

}
