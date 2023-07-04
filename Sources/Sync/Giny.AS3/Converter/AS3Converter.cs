using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Converter
{
    public abstract class AS3Converter
    {
        protected List<AS3Field> FieldsToWrite
        {
            get;
            private set;
        }
        protected List<AS3Method> MethodsToWrite
        {
            get;
            private set;
        }
        public AS3File File
        {
            get;
            private set;
        }
        
        public AS3Converter(AS3File file)
        {
        
            this.File = file;
            this.FieldsToWrite = SelectFieldsToWrite();
            this.MethodsToWrite = SelectMethodsToWrite();
        }

        public abstract void Prepare(Dictionary<string, AS3File> context);

        public abstract string GetNamespace();

        public abstract string GetClassName();

        public abstract string GetExtends();

        public abstract string GetImplements();

        public abstract string GetImports();

        protected AS3Method GetMethodToWrite(string name)
        {
            return MethodsToWrite.FirstOrDefault(x => x.Name == name);
        }
        public string GetTextFields()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var field in FieldsToWrite)
            {
                sb.AppendLine(GetField(field) + ";");
            }
            return sb.ToString();
        }
        public string GetTextMethods()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var method in MethodsToWrite)
            {
                sb.AppendLine(GetMethodSignature(method));
                WriteBlock(method.Expressions, sb);
            }

            return sb.ToString();
        }

        protected abstract string GetField(AS3Field field);

        protected abstract List<AS3Method> SelectMethodsToWrite();

        protected abstract List<AS3Field> SelectFieldsToWrite();

       
        private void WriteBlock(List<BaseExpression> expressions, StringBuilder sb)
        {
            sb.AppendLine("{");

           

            foreach (var exp in expressions)
            {
                string endLine = ";";

                if (exp.HasBracket)
                {
                    endLine = string.Empty;
                }
                sb.AppendLine(GetExpression(exp) + endLine);
            }
         

            sb.AppendLine("}");
        }
        protected string GetExpression(BaseExpression expression)
        {
            switch (expression)
            {
                case AssignationExpression ex:
                    return GetAssignationExpression(ex);
                case ConstantExpression ex:
                    return GetConstantExpression(ex);
                case ElseExpression ex:
                    StringBuilder elseSb = new StringBuilder();
                    elseSb.AppendLine(GetElseHeader(ex));
                    WriteBlock(ex.Expressions, elseSb);
                    return elseSb.ToString();
                case ForExpression ex:
                    StringBuilder forSb = new StringBuilder();
                    forSb.AppendLine(GetForHeader(ex));
                    WriteBlock(ex.Expressions, forSb);
                    return forSb.ToString();
                case IfExpression ex:
                    StringBuilder ifSb = new StringBuilder();
                    ifSb.AppendLine(GetIfHeader(ex));
                    WriteBlock(ex.Expressions, ifSb);
                    return ifSb.ToString();
                case MethodCallExpression ex:
                    return GetMethodCallExpression(ex);
                case EmptyExpression ex:
                    return string.Empty;
                case NumericIncrementationExpression ex:
                    return GetNumericIncrementationExpression(ex);
                case ObjectInstantiationExpression ex:
                    return GetObjectInstantiationExpression(ex);
                case ThrowExpression ex:
                    return GetThrowExpression(ex);
                case UnchangedExpression ex:
                    return GetUnchangedExpression(ex);
                case VariableDeclarationExpression ex:
                    return GetVariableDeclarationExpression(ex);
                case VariableNameExpression ex:
                    return GetVariableNameExpression(ex);
                case AsExpression ex:
                    return GetAsExpression(ex);
                case ConditionExpression ex:
                    return GetConditionExpression(ex);


            }
            throw new Exception("unhandled expression " + expression);
        }

        protected abstract string GetConditionExpression(ConditionExpression ex);

        protected abstract string GetForHeader(ForExpression ex);

        protected abstract string GetAsExpression(AsExpression ex);

        protected abstract string GetVariableNameExpression(VariableNameExpression ex);

        protected abstract string GetUnchangedExpression(UnchangedExpression ex);

        protected abstract string GetVariableDeclarationExpression(VariableDeclarationExpression ex);

        protected abstract string GetThrowExpression(ThrowExpression ex);

        protected abstract string GetNumericIncrementationExpression(NumericIncrementationExpression expression);

        protected abstract string GetObjectInstantiationExpression(ObjectInstantiationExpression expression);

        protected abstract string GetAssignationExpression(AssignationExpression expression);

        protected abstract string GetConstantExpression(ConstantExpression expression);

        protected abstract string GetElseHeader(ElseExpression elseExpression);

        protected abstract string GetIfHeader(IfExpression ifExpression);

        protected abstract string GetMethodCallExpression(MethodCallExpression expression);

        protected abstract string GetMethodSignature(AS3Method method);

        protected abstract string GetType(AS3Type type);

        protected abstract string GetAccessor(AS3AccessorsEnum accessor);

        protected abstract string GetModifier(AS3ModifiersEnum modifier);

        public abstract string GetConvertedType(AS3Type variable);

        public abstract string VerifyVariableName(string variableName);

    }
}
