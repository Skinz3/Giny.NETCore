using Giny.World.Managers.Fights.Fighters;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions.Handlers
{
    public abstract class Criterion
    {
        protected CriterionComparaisonOperator Operator
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }
        public virtual short MaxValue => 1;

        public Criterion()
        {

        }

        public Criterion(string criteriaFull)
        {
            Text = criteriaFull;
            Parse();

        }

        private void Parse()
        {
            var rawOperator = Text.Remove(0, 2).Take(1).ToArray()[0];

            Value = Text.Remove(0, 3);

            switch (rawOperator)
            {
                case '<':
                    Operator = CriterionComparaisonOperator.Inferior;
                    break;
                case '>':
                    Operator = CriterionComparaisonOperator.Superior;
                    break;
                case '!':
                    Operator = CriterionComparaisonOperator.Negation;
                    break;
                case '=':
                    Operator = CriterionComparaisonOperator.Equal;
                    break;
                case 'X':
                    Operator = CriterionComparaisonOperator.X;
                    break;
                case '~':
                    Operator = CriterionComparaisonOperator.Tilde;
                    break;

                default:
                    throw new NotImplementedException("Unhandled criterion operator : " + rawOperator);
            }
        }

        public abstract bool Eval(WorldClient client);

        public virtual bool Eval(Fighter fighter)
        {
            return true;
        }

        protected bool ArithmeticEval(int delta)
        {
            int criterialDelta = int.Parse(Value);

            switch (Operator)
            {
                case CriterionComparaisonOperator.Equal:
                    if (delta == criterialDelta)
                        return true;
                    break;
                case CriterionComparaisonOperator.Inferior:
                    if (delta < criterialDelta)
                        return true;
                    break;
                case CriterionComparaisonOperator.Superior:
                    if (delta > criterialDelta)
                        return true;
                    break;
            }

            return false;
        }

        public virtual short GetCurrentValue(WorldClient client)
        {
            return 0;
        }
    }
}
