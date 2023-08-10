using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions
{
    public class OperatorNode : Node
    {
        public char Operator
        {
            get;
            private set;
        }

        public Node Left
        {
            get;
            set;
        }
        public Node Right
        {
            get;
            set;
        }

        public OperatorNode(char @operator, Node left, Node right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }


        public override bool Eval(WorldClient client)
        {
            bool leftValue = Left.Eval(client);
            bool rightValue = Right.Eval(client);

            if (Operator == '&')
            {
                return leftValue && rightValue;
            }
            else if (Operator == '|')
            {
                return leftValue || rightValue;
            }
            else
            {
                throw new InvalidOperationException("Unsupported operator: " + Operator);
            }
        }
        public override string ToString()
        {
            return this.Operator.ToString();
        }

        public override IEnumerable<Criterion> FindCriterionHandlers()
        {
            return Right.FindCriterionHandlers().Concat(Left.FindCriterionHandlers());
        }
    }
}
