using Giny.AS3;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Converters
{
    /// <summary>
    /// Only this part may change with dofus client versions ;)
    /// </summary>
    public class DofusHelper
    {
        public static string GetTypeFromDofusIOBeforePrep(string io)
        {
            switch (io)
            {
                case "writeInt":
                    return "int";
                case "writeShort":
                    return "short";
                case "writeByte":
                    return "byte";
                case "writeUTF":
                    return "string";
                case "writeVarLong":
                    return "long";
                case "writeVarInt":
                    return "int";
                case "writeBoolean":
                    return "bool";
                case "writeVarShort":
                    return "short";
                case "writeDouble":
                    return "double";
                case "writeUnsignedInt":
                    return "uint";
                case "writeUInt":
                    return "uint";
                case "writeFloat":
                    return "float";
            }

            return null;
        }
        public static string GetTypeFromDofusIO(string io)
        {
            switch (io)
            {
                case "WriteInt":
                    return "int";
                case "WriteShort":
                    return "short";
                case "WriteByte":
                    return "byte";
                case "WriteUTF":
                    return "string";
                case "WriteVarLong":
                    return "long";
                case "WriteVarInt":
                    return "int";
                case "WriteBoolean":
                    return "bool";
                case "WriteVarShort":
                    return "short";
                case "WriteDouble":
                    return "double";
                case "WriteUnsignedInt":
                    return "uint";
                case "WriteUInt":
                    return "uint";
                case "WriteFloat":
                    return "float";
            }

            throw new NotImplementedException("Undefined IO :" + io);
        }

        /// <summary>
        /// we change fields types depending of serialization. if WriteShort(serverId); serverId becomes a short.
        /// Maybe make recursive? For loop
        /// </summary>
        /// <param name="file"></param>
        /// <param name="serializeMethod"></param>
        public static void DeductFieldTypes(AS3File file, List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var methodCall = expression as MethodCallExpression;

                if (methodCall != null)
                {
                    if (methodCall.Parameters.Count == 1)
                    {
                        var variableNameExpression = methodCall.Parameters.ElementAt(0).Key as VariableNameExpression;

                        if (variableNameExpression != null)
                        {
                            var field = file.GetField(variableNameExpression.VariableName);

                            if (field != null)
                            {
                                field.ChangeType(DofusHelper.GetTypeFromDofusIO(methodCall.MethodName));
                            }
                        }

                        var arrayIndexer = methodCall.Parameters.ElementAt(0).Key as UnchangedExpression;


                        if (arrayIndexer != null && arrayIndexer.Line.Contains("[") && arrayIndexer.Line.Contains("]"))
                        {
                            var variableName = arrayIndexer.Line.Split('[')[0].Trim();

                            var field = file.GetField(variableName);

                            if (field != null)
                            {
                                field.ChangeType(DofusHelper.GetTypeFromDofusIO(methodCall.MethodName) + "[]");
                            }
                        }
                    }
                }

                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    DeductFieldTypes(file, forExpression.Expressions);
                }

            }
        }

        public static List<AS3Variable> DeductAllFieldTypes(List<BaseExpression> expressions)
        {
            List<AS3Variable> results = new List<AS3Variable>();

            foreach (var expression in expressions)
            {
                var methodCall = expression as MethodCallExpression;

                if (methodCall != null)
                {
                    if (methodCall.Parameters.Count == 1)
                    {
                        var variableNameExpression = methodCall.Parameters.ElementAt(0).Key as VariableNameExpression;

                        if (variableNameExpression != null)
                        {
                            var ioType = DofusHelper.GetTypeFromDofusIOBeforePrep(methodCall.MethodName);

                            if (ioType != null)
                                results.Add(new AS3Variable(variableNameExpression.VariableName, ioType));
                        }

                        var arrayIndexer = methodCall.Parameters.ElementAt(0).Key as UnchangedExpression;


                        if (arrayIndexer != null && arrayIndexer.Line.Contains("[") && arrayIndexer.Line.Contains("]"))
                        {
                            var variableName = arrayIndexer.Line.Split('[')[0].Trim();

                            var ioType = DofusHelper.GetTypeFromDofusIOBeforePrep(methodCall.MethodName);

                            if (ioType != null)
                                results.Add(new AS3Variable(variableName, ioType+"[]"));
                        }
                    }
                }

                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    results.AddRange(DeductAllFieldTypes(forExpression.Expressions));
                }

            }

            return results;
        }

        public static void DeductCtorFieldTypes(string baseName, AS3File current, List<AS3Variable> parameters, Dictionary<string,AS3File> context)
        {
            List<AS3Variable> results = new List<AS3Variable>();

            var target = context[current.Extends];

            var method = target.GetMethod("serializeAs_" + target.ClassName);

            var targetResults = DeductAllFieldTypes(method.Expressions);

            bool valid = true;

            foreach (var parameter in parameters)
            {
                var tRes = targetResults.FirstOrDefault(x => x.Name == parameter.Name);

                if (tRes != null)
                {
                    parameter.Type = tRes.Type;
                }
                else
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                if (target.Extends == baseName)
                {
                    return;
                }

                var target2 = context[target.Extends];

                if (target2.Extends == baseName)
                {
                    return;
                }
                DeductCtorFieldTypes(baseName, target2, parameters, context);


            }


        }

        public static void ChangeTypeIdToProperty(List<BaseExpression> expressions)
        {
            List<BaseExpression> finalExpressions = new List<BaseExpression>();

            foreach (var expression in expressions)
            {
                var methodCall = expression as MethodCallExpression;

                if (methodCall != null)
                {
                    int n = 0;
                    foreach (var parameter in methodCall.Parameters.ToArray())
                    {
                        if (parameter.Key is MethodCallExpression)
                        {
                            var methodCallP = (parameter.Key as MethodCallExpression);
                            if (methodCallP.MethodName == "getTypeId")
                            {
                                methodCall.SetExpression(n, new VariableNameExpression(methodCallP.GetMethodReferences() + "TypeId"), "short");
                            }
                        }
                        n++;
                    }
                    AsExpression asExpression = methodCall.GetParameter<AsExpression>(0);

                    if (asExpression != null && ((MethodCallExpression)asExpression.Expression).MethodName == "getTypeId")
                    {
                        asExpression.SetExpression(new VariableNameExpression("TypeId"));
                    }
                }
                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    ChangeTypeIdToProperty(forExpression.Expressions);
                }

                finalExpressions.Add(expression);
            }

        }
        public static void RenameSerializeAs_(List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var asExpression = expression as AsExpression;

                if (asExpression != null)
                {
                    var call = asExpression.Expression as MethodCallExpression;

                    if (call.MethodName.StartsWith("serializeAs_"))
                    {
                        call.MethodName = "Serialize";
                    }
                }

                var methodCall = expression as MethodCallExpression;

                if (methodCall != null && methodCall.MethodName.StartsWith("serializeAs_"))
                {
                    methodCall.MethodName = "Serialize";
                }
                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    RenameSerializeAs_(forExpression.Expressions);
                }
                var ifExpression = expression as IfExpression;

                if (ifExpression != null)
                {
                    RenameSerializeAs_(ifExpression.Expressions);
                }

                var elseExpression = expression as ElseExpression;

                if (elseExpression != null)
                {
                    RenameSerializeAs_(elseExpression.Expressions);
                }
            }
        }
        public static void CreateGenericTypeForProtocolInstance(List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var assignationExpression = expression as AssignationExpression;

                if (assignationExpression != null)
                {
                    var methodCall = assignationExpression.Value as MethodCallExpression;

                    if (methodCall != null && methodCall.GetMethodFullName() == "ProtocolTypeManager.GetInstance")
                    {
                        var typeParam = methodCall.GetParameter(0) as VariableNameExpression;
                        methodCall.MethodName += "<" + typeParam.VariableName + ">";
                        methodCall.RemoveParameter(0);
                        methodCall.CastParameter(0, "short");
                    }
                }
                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    CreateGenericTypeForProtocolInstance(forExpression.Expressions);
                }
            }
        }

        public static void CastEnumsComparaisons(List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var ifExpression = expression as IfExpression;

                if (ifExpression != null)
                {
                    var condition = ((ConditionExpression)ifExpression.ConditionExpression).Condition;
                    CastEnumOnConditionRecursively(condition);
                }

            }
        }
        private static void CastEnumOnConditionRecursively(AS3Condition condition)
        {
            if (condition == null)
            {
                return;
            }

            var variableNameExpLeft = condition.Left as VariableNameExpression;

            var variableNameExpRight = condition.Right as VariableNameExpression;

            if (variableNameExpLeft != null && variableNameExpLeft.GetVariableFullName().Contains("PlayableBreedEnum"))
            {
                condition.CastLeft("byte");
            }
            if (variableNameExpRight != null && variableNameExpRight.GetVariableFullName().Contains("PlayableBreedEnum"))
            {
                condition.CastRight("byte");
            }

            CastEnumOnConditionRecursively(condition.Next.Condition);
        }

        public static void IOReadCastRecursively(DofusConverter converter, AS3File file, AS3Method deserializeMethod, List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var variableDeclarationExpression = expression as VariableDeclarationExpression;

                if (variableDeclarationExpression != null)
                {
                    if (variableDeclarationExpression.Variable.Name.StartsWith("_box"))
                    {
                        variableDeclarationExpression.Variable.Type = new AS3Type("byte");
                        continue;
                    }
                    var methodCallExpression = variableDeclarationExpression.Value as MethodCallExpression;

                    if (methodCallExpression != null && methodCallExpression.GetMethodFullName().StartsWith("reader.Read"))
                    {
                        var type = variableDeclarationExpression.Variable.Type;
                        methodCallExpression.CastCall(type.RawType);
                    }
                }

                var assignationExpression = expression as AssignationExpression;

                if (assignationExpression != null)
                {
                    var methodCallExpression = assignationExpression.Value as MethodCallExpression;

                    if (methodCallExpression != null && methodCallExpression.GetMethodFullName().StartsWith("reader.Read"))
                    {
                        var fieldName = assignationExpression.Target;

                        var field = file.GetField(fieldName);

                        if (field != null)
                        {
                            string type = converter.GetConvertedType(new AS3Type(field.RawType));
                            methodCallExpression.CastCall(type);
                        }
                        else
                        {
                            var definition = deserializeMethod.Expressions.OfType<VariableDeclarationExpression>().FirstOrDefault(x => x.Variable.Name == assignationExpression.Target);
                            if (definition != null)
                            {
                                string type = converter.GetConvertedType(new AS3Type(definition.Variable.Type.RawType));
                                methodCallExpression.CastCall(type);
                            }
                        }
                    }
                }
                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    IOReadCastRecursively(converter, file, deserializeMethod, forExpression.Expressions);
                }
            }
        }
        public static void InstantiateArrays(DofusConverter converter, AS3File file, AS3Method deserializeMethod)
        {
            var arrays = file.GetFields(x => x.RawType.Contains("[]"));

            List<BaseExpression> finalExpressions = new List<BaseExpression>();

            foreach (var expression in deserializeMethod.Expressions)
            {
                finalExpressions.Add(expression);

                var variableDeclarationExpression = expression as VariableDeclarationExpression;

                if (variableDeclarationExpression != null)
                {

                    foreach (var array in arrays)
                    {
                        if (variableDeclarationExpression.Variable.Name == "_" + array.Name + "Len")
                        {
                            string type = converter.GetConvertedType(new AS3Type(array.RawType.Replace("[]", string.Empty)));
                            string line = string.Format("{0} = new {1}[{2}]", converter.VerifyVariableName(array.Variable.Name), type, "_" + array.Name + "Len");
                            finalExpressions.Add(new UnchangedExpression(line));
                        }
                    }
                }


            }

            deserializeMethod.Expressions = finalExpressions;
        }
        public static void TransformVectorPushIntoCSharpArrayIndexer(AS3File file, DofusConverter converter, AS3Method deserializeMethod)
        {
            foreach (var expression in deserializeMethod.Expressions)
            {
                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    List<BaseExpression> finalExpressions = new List<BaseExpression>();

                    foreach (var ex in forExpression.Expressions)
                    {
                        var methodCall = ex as MethodCallExpression;

                        if (methodCall != null && methodCall.MethodName == "push")
                        {
                            var variable = methodCall.MethodCallReferences[0];
                            var field = file.GetField(variable.VariableName);
                            AS3Type cast = null;

                            if (field != null && field.RawType.Contains('<') == false)
                            {
                                cast = new AS3Type(field.Variable.Type.RawType.RemoveChars('[', ']'));
                            }

                            string indexer = ((VariableDeclarationExpression)forExpression.VariableExpression).Variable.Name;
                            string target = string.Format("{0}[{1}]", converter.VerifyVariableName(variable.VariableName), indexer);
                            VariableNameExpression value = (methodCall.GetParameter(0) as VariableNameExpression);
                            finalExpressions.Add(new AssignationExpression(target, value, cast));
                        }
                        else
                        {
                            finalExpressions.Add(ex);
                        }
                    }

                    forExpression.Expressions = finalExpressions;
                }

            }
        }
        public static void RenameDofusTypesSerializeMethodsRecursively(List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var methodCallExpression = expression as MethodCallExpression;

                if (methodCallExpression != null)
                {
                    if (methodCallExpression.MethodName.StartsWith("serializeAs_"))
                    {
                        methodCallExpression.MethodName = "Serialize";
                    }
                }

                var unchangedExpression = expression as UnchangedExpression; // as

                if (unchangedExpression != null)
                {
                    if (unchangedExpression.Line.Contains("serializeAs_"))
                    {
                        methodCallExpression.MethodName = "Serialize";
                    }
                }

                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    RenameDofusTypesSerializeMethodsRecursively(forExpression.Expressions);
                }
            }
        }

        public static void IOWriteCastRecursively(List<BaseExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                var assignation = expression as VariableDeclarationExpression;

                if (assignation != null)
                {
                    if (assignation.Variable.Name.StartsWith("_box"))
                    {
                        assignation.Variable.Type = new AS3Type("byte");
                        continue;
                    }
                }


                var methodCallExpression = expression as MethodCallExpression;

                if (methodCallExpression != null)
                {
                    if (methodCallExpression.GetMethodFullName().StartsWith("writer.Write"))
                    {
                        var type = DofusHelper.GetTypeFromDofusIO(methodCallExpression.MethodName);

                        methodCallExpression.CastParameter(0, type);
                    }
                }

                var forExpression = expression as ForExpression;

                if (forExpression != null)
                {
                    IOWriteCastRecursively(forExpression.Expressions);
                }
            }
        }
    }
}
