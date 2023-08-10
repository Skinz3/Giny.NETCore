using Giny.World.Managers.Criterions;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Zones.Sets;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Network;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Giny.World.Managers.Criterions.Handlers;
using Giny.ORM.Attributes;

namespace Giny.World.Managers.Criterias
{
    public class CriteriaExpression
    {
        public string Expression
        {
            get;
            private set;
        }

        private Node Tree
        {
            get;
            set;
        }
        public CriteriaExpression(string expression = "")
        {
            this.Expression = expression;
            this.Tree = BuildNode(expression);
        }



        private Node BuildNode(string expression)
        {
            if (expression == null || string.IsNullOrWhiteSpace(expression))
            {
                return new EmptyNode();
            }

            if (expression.StartsWith("(") && expression.EndsWith(")"))
            {
                expression = expression.Substring(1, expression.Length - 2); // Remove outer parentheses
            }

            int operatorIndex = FindOutermostOperator(expression);

            if (operatorIndex != -1)
            {
                char @operator = expression[operatorIndex];
                string leftExpression = expression.Substring(0, operatorIndex);
                string rightExpression = expression.Substring(operatorIndex + 1);

                Node leftNode = BuildNode(leftExpression);
                Node rightNode = BuildNode(rightExpression);

                return new OperatorNode(@operator, leftNode, rightNode);
            }
            else
            {
                return new CriterionNode(expression);
            }
        }

        private int FindOutermostOperator(string expression)
        {
            int parenCount = 0;
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];
                if (c == '(') parenCount++;
                else if (c == ')') parenCount--;
                else if (parenCount == 0 && (c == '&' || c == '|'))
                    return i;
            }
            return -1;
        }

        public List<Criterion> FindCriterionHandlers()
        {
            return Tree.FindCriterionHandlers().ToList();
        }

        public override string ToString()
        {
            return this.Expression;
        }


        [CustomDeserialize]
        private static CriteriaExpression Deserialize(string? str)
        {
            return new CriteriaExpression(str);
        }

        [CustomSerialize]
        private static string Serialize(CriteriaExpression expression)
        {
            return expression.Expression;
        }

        public static bool Eval(string expression, WorldClient client)
        {
            return new CriteriaExpression(expression).Eval(client);
        }

        public bool Eval(WorldClient client)
        {
            return Tree.Eval(client);
        }
    }
}
