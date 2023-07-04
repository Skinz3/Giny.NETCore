using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class ForExpression : ParentExpression
    {
        public override bool HasBracket => true;

        public BaseExpression VariableExpression
        {
            get;
            set;
        }
        public BaseExpression Minimum
        {
            get;
            set;
        }
        public BaseExpression Incrementation
        {
            get;
            set;
        }
        public List<BaseExpression> Expressions
        {
            get;
            set;
        }
        public ForExpression(AS3File file, string line, int i) : base(line)
        {
            string variableExpression = Regex.Match(line, @"(?<=\()(.*?)(?=;)").Value;
            string minimumAssersion = Regex.Match(line, @"(?<=;)(.*?)(?=;)").Groups[1].Value;
            string incrementation = line.Split(';').Last().Replace(")", string.Empty);

            VariableExpression = ExpressionManager.Construct(file, variableExpression, i);
            Minimum = ExpressionManager.Construct(file, minimumAssersion, i);
            Incrementation = ExpressionManager.Construct(file, incrementation, i);

            int forStartIndentIndex = i + 1;
            var indent = file.GetLineIndentLevel(forStartIndentIndex);
            var blockDefinition = AS3Helper.GetBlockDefinitionForInstructions(file, forStartIndentIndex, indent);
            Expressions = AS3Helper.BuildExpressions(file, blockDefinition);
            this.LineSkip = blockDefinition.LinesCount;
        }
        public static bool IsValid(string line)
        {
            return line.Trim().StartsWith("for(") && !line.Contains(" in ");
        }
        public override void RenameVariable(string variableName, string newVariableName)
        {
            VariableExpression.RenameVariable(variableName, newVariableName);
            Minimum.RenameVariable(variableName, newVariableName);
            Incrementation.RenameVariable(variableName, newVariableName);

            foreach (var expression in Expressions)
            {
                expression.RenameVariable(variableName, newVariableName);
            }
        }
        public override void RenameType(string typeName, string newTypeName)
        {
            VariableExpression.RenameType(typeName, newTypeName);
            Minimum.RenameType(typeName, newTypeName);
            Incrementation.RenameType(typeName, newTypeName);

            foreach (var expression in Expressions)
            {
                expression.RenameType(typeName, newTypeName);
            }
        }
        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            VariableExpression.RenameMethodCall(methodName, newMethodName);
            Minimum.RenameMethodCall(methodName, newMethodName);
            Incrementation.RenameMethodCall(methodName, newMethodName);

            foreach (var expression in Expressions)
            {
                expression.RenameMethodCall(methodName, newMethodName);
            }
        }
    }
}
