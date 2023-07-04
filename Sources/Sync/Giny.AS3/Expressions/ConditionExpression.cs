using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3.Expressions
{
    public class AS3Condition
    {
        public string Operator
        {
            get;
            private set;
        }
        public BaseExpression Right
        {
            get;
            private set;
        }
        public AS3Type RightCast
        {
            get;
            private set;
        }
        public BaseExpression Left
        {
            get;
            private set;
        }
        public AS3Type LeftCast
        {
            get;
            private set;
        }
        public NextAS3Condition Next
        {
            get;
            set;
        }
        public AS3Condition(string @operator, BaseExpression right, BaseExpression left, NextAS3Condition Next)
        {
            this.Operator = @operator;
            this.Right = right;
            this.Left = left;
            this.Next = Next;
        }

        public void CastRight(string rawType)
        {
            this.RightCast = new AS3Type(rawType);
        }
        public void CastLeft(string rawType)
        {
            this.LeftCast = new AS3Type(rawType);
        }
    }

    public class NextAS3Condition
    {
        public AndOrEnum NextOperator
        {
            get;
            set;
        }
        public AS3Condition Condition
        {
            get;
            set;
        }
        public NextAS3Condition(AndOrEnum nextOperator, AS3Condition condition)
        {
            this.NextOperator = nextOperator;
            this.Condition = condition;
        }

    }
    public enum AndOrEnum
    {
        None,
        And,
        Or,
    }
    public class ConditionExpression : BaseExpression
    {
        public AS3Condition Condition
        {
            get;
            set;
        }
        public ConditionExpression(AS3File file, string line, int i) : base(line)
        {
            List<AndOrEnum> AndsOrds = new List<AndOrEnum>();

            for (int w = 0; w < line.Length - 1; w++)
            {
                if (line[w] == '|' && line[w + 1] == '|')
                {
                    AndsOrds.Add(AndOrEnum.Or);
                }
                else if (line[w] == '&' && line[w + 1] == '&')
                {
                    AndsOrds.Add(AndOrEnum.And);
                }
            }

            if (AndsOrds.Count == 0)
                AndsOrds.Add(AndOrEnum.None);

            try
            {
                var splitted = line.Split(new string[] { "&&", "||" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                this.Condition = CreateConditionRecursively(file, AndsOrds, splitted, null);
            }
            catch
            {

            }


        }
        private AS3Condition GetConditionFromString(string str, AS3File file, NextAS3Condition nextCondition)
        {
            var op = Regex.Match(str, @"(?: != | == | > | < | >= | <= | is)").Value.Trim();
            var split = str.Split(new string[] { op }, StringSplitOptions.RemoveEmptyEntries);

            var left = ExpressionManager.Construct(file, split[0].Trim(), 0);
            var right = ExpressionManager.Construct(file, split[1].Trim(), 0);

            return new AS3Condition(op, right, left, nextCondition);
        }
        private AS3Condition CreateConditionRecursively(AS3File file, List<AndOrEnum> andsor, List<string> conditions, AS3Condition condition)
        {
            if (conditions.Count == 0)
            {
                return condition;
            }

            var first = andsor.FirstOrDefault();

            if (andsor.Count > 0)
                andsor.RemoveAt(0);
            var firstC = conditions.First();
            conditions.RemoveAt(0);
            return GetConditionFromString(firstC, file, new NextAS3Condition(first, CreateConditionRecursively(file, andsor, conditions, condition)));
        }


        public override void RenameMethodCall(string methodName, string newMethodName)
        {
            RenameMethodCallRecursively(Condition, methodName, newMethodName);
        }
        private void RenameMethodCallRecursively(AS3Condition condition, string methodName, string newMethodName)
        {
            if (condition == null)
                return;

            condition.Left.RenameMethodCall(methodName, newMethodName);
            condition.Right.RenameMethodCall(methodName, newMethodName);
            RenameMethodCallRecursively(condition.Next.Condition, methodName, newMethodName);
        }

        public override void RenameType(string typeName, string newTypeName)
        {
            RenameTypeRecursively(Condition, typeName, newTypeName);
        }
        private void RenameTypeRecursively(AS3Condition condition, string typeName, string newTypeName)
        {
            if (condition == null)
                return;

            condition.Left.RenameType(typeName, newTypeName);
            condition.Right.RenameType(typeName, newTypeName);
            RenameTypeRecursively(condition.Next.Condition, typeName, newTypeName);
        }

        public override void RenameVariable(string variableName, string newVariableName)
        {
            RenameVariableRecursively(Condition, variableName, newVariableName);
        }
        private void RenameVariableRecursively(AS3Condition condition, string variableName, string newVariableName)
        {
            if (condition == null)
                return;

            condition.Left.RenameVariable(variableName, newVariableName);
            condition.Right.RenameVariable(variableName, newVariableName);
            RenameVariableRecursively(condition.Next.Condition, variableName, newVariableName);
        }
        public static bool IsValid(string line)
        {
            return Regex.Match(line, @"(?: != | == | > | < | >= | <= | is )").Success;
        }
    }
}
