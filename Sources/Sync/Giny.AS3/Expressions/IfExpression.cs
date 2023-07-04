using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    /// <summary>
    /// todo => comparaison operator
    /// comparaison system. 
    /// </summary>
    public class IfExpression : ParentExpression
    {
        public override bool HasBracket => true;

        public List<BaseExpression> Expressions
        {
            get;
            private set;
        }
        public BaseExpression ConditionExpression
        {
            get;
            private set;
        }
        public IfExpression(AS3File file, string line, int i) : base(line)
        {
            int ifStartIndentIndex = i + 1;
            var indent = file.GetLineIndentLevel(ifStartIndentIndex);
            var blockDefinition = AS3Helper.GetBlockDefinitionForInstructions(file, ifStartIndentIndex, indent);
            Expressions = AS3Helper.BuildExpressions(file, blockDefinition);
            this.LineSkip = blockDefinition.LinesCount;
            var conditionLine = Regex.Match(line, "[(](.*)[)]").Groups[1].Value;
            this.ConditionExpression = ExpressionManager.Construct(file, conditionLine, i);
        }
        public static bool IsValid(string line)
        {
            return line.StartsWith("if");
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            ConditionExpression.RenameVariable(variableName, newVariableName);

            foreach (var expression in Expressions)
            {
                expression.RenameVariable(variableName, newVariableName);
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {
            ConditionExpression.RenameType(typeName, newTypeName);

            foreach (var expression in Expressions)
            {
                expression.RenameType(typeName, newTypeName);
            }
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            ConditionExpression.RenameMethodCall(methodName, newMethodName);

            foreach (var expression in Expressions)
            {
                expression.RenameMethodCall(methodName, newMethodName);
            }
        }
    }
}
