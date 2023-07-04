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
    public class ElseExpression : ParentExpression
    {
        public override bool HasBracket => true;

        public List<BaseExpression> Expressions
        {
            get;
            set;
        }
        public BaseExpression Condition
        {
            get;
            private set;
        }
        public ElseExpression(AS3File file, string line, int i) : base(line)
        {
            int elseStartIndentIndex = i + 1;
            var indent = file.GetLineIndentLevel(elseStartIndentIndex);
            var blockDefinition = AS3Helper.GetBlockDefinitionForInstructions(file, elseStartIndentIndex, indent);
            Expressions = AS3Helper.BuildExpressions(file, blockDefinition);
            this.LineSkip = blockDefinition.LinesCount;
            string conditionLine = Regex.Match(line, "[(](.*)[)]").Groups[1].Value;
            this.Condition = ExpressionManager.Construct(file, conditionLine, i);
        }
        public static bool IsValid(string line)
        {
            return line.StartsWith("else");
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            Condition.RenameVariable(variableName, newVariableName);

            foreach (var expression in Expressions)
            {
                expression.RenameVariable(variableName, newVariableName);
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {
            Condition.RenameType(typeName, newTypeName);

            foreach (var expression in Expressions)
            {
                expression.RenameType(typeName, newTypeName);
            }
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            Condition.RenameMethodCall(methodName, newMethodName);

            foreach (var expression in Expressions)
            {
                expression.RenameMethodCall(methodName, newMethodName);
            }
        }
    }
}
