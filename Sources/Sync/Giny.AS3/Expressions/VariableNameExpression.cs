using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class VariableNameExpression : BaseExpression
    {
        public VariableNameExpression[] VariableCallReferences
        {
            get;
            set;
        }
        public string VariableName
        {
            get;
            private set;
        }
        public VariableNameExpression(string line) : base(line)
        {
            string fullName = line;

            var tree = fullName.Split('.');

            VariableName = tree.Last();

            var tree2 = tree.Take(tree.Length - 1).ToArray();

            VariableCallReferences = new VariableNameExpression[tree2.Length];

            for (int w = 0; w < tree2.Length; w++)
            {
                VariableCallReferences[w] = new VariableNameExpression(tree2[w]);
            }
        }
        public static bool IsValid(string line)
        {
            return line.Split('.').All(x => Regex.Match(x, @"^[a-zA-Z_$][a-zA-Z_$0-9]*$").Success);
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            if (variableName == VariableName)
            {
                VariableName = newVariableName;
            }
        }
        public string GetVariableFullName()
        {
            StringBuilder result = new StringBuilder();

            foreach (var reference in VariableCallReferences)
            {
                result.Append(reference.VariableName + ".");
            }
            result.Append(VariableName);

            return result.ToString();
        }
        public override void RenameType(string typeName, string newTypeName)
        {

        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {

        }
    }
}
