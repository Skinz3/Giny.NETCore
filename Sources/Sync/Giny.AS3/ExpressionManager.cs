using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3
{
    class ExpressionManager
    {
        public ParentExpression GetParentExpression(AS3Method method, BaseExpression methodExpression)
        {
            var index = method.Expressions.IndexOf(methodExpression);

            for (int i = index; i > 0; i--)
            {
                if (method.Expressions[i] is ParentExpression)
                {
                    return method.Expressions[i] as ParentExpression;
                }
            }
            throw new Exception("Unable to find parent.");
        }
        public static BaseExpression Construct(AS3File file, string line, int i)
        {
            line = line.PrepareForExpression();

            if (line == string.Empty)
            {
                return new EmptyExpression();
            }
            else if (ConstantStringExpression.IsValid(line))
            {
                return new ConstantStringExpression(line);
            }
            else if (ConstantIntExpression.IsValid(line))
            {
                return new ConstantIntExpression(line);
            }
            else if (ConstantBooleanExpression.IsValid(line))
            {
                return new ConstantBooleanExpression(line);
            }
            else if (ConstantNumberExpression.IsValid(line))
            {
                return new ConstantNumberExpression(line);
            }
            else if (ObjectInstantiationExpression.IsValid(line))
            {
                return new ObjectInstantiationExpression(file, line, i);
            }
            else if (ForExpression.IsValid(line))
            {
                return new ForExpression(file, line, i);
            }
            else if (IfExpression.IsValid(line))
            {
                return new IfExpression(file, line, i);
            }
            else if (ElseExpression.IsValid(line))
            {
                return new ElseExpression(file, line, i);
            }

            else if (ConditionExpression.IsValid(line))
            {
                return new ConditionExpression(file, line, i);
            }
            else if (VariableDeclarationExpression.IsValid(line))
            {
                return new VariableDeclarationExpression(file, line, i);
            }
            else if (AssignationExpression.IsValid(line))
            {
                return new AssignationExpression(file, line, i);
            }
            else if (VariableNameExpression.IsValid(line))
            {
                return new VariableNameExpression(line);
            }
            else if (ThrowExpression.IsValid(line))
            {
                return new ThrowExpression(line);
            }
            else if (NumericIncrementationExpression.IsValid(line))
            {
                return new NumericIncrementationExpression(line);
            }
            else if (MethodCallExpression.IsValid(line))
            {
                return new MethodCallExpression(file, line, i);
            }
            else if (AsExpression.IsValid(line))
            {
                return new AsExpression(file, line, i);
            }
            else
            {
                return new UnchangedExpression(line);
            }
        }

    }
}
