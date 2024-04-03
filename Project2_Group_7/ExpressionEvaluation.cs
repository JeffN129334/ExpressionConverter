using System.Linq.Expressions;

namespace Project2_Group_7
{
    public class ExpressionEvaluation
    {
        public int EvaluatePrefixExpression(string prefixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = prefixExpression.Split(' ');

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];
                if (IsOperator(token))
                {
                    Expression right = stack.Pop();
                    Expression left = stack.Pop();
                    Expression combined = CombineExpressions(token, left, right);
                    stack.Push(combined);
                }
                else
                {
                    stack.Push(Expression.Constant(int.Parse(token)));
                }
            }

            var lambda = Expression.Lambda<Func<int>>(stack.Pop());
            var compiled = lambda.Compile();
            return compiled();
        }

        public int EvaluatePostfixExpression(string postfixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = postfixExpression.Split(' ');

            foreach (string token in tokens)
            {
                if (IsOperator(token))
                {
                    Expression right = stack.Pop();
                    Expression left = stack.Pop();
                    Expression combined = CombineExpressions(token, left, right);
                    stack.Push(combined);
                }
                else
                {
                    stack.Push(Expression.Constant(int.Parse(token)));
                }
            }

            var lambda = Expression.Lambda<Func<int>>(stack.Pop());
            var compiled = lambda.Compile();
            return compiled();
        }

        private Expression CombineExpressions(string op, Expression left, Expression right)
        {
            switch (op)
            {
                case "+":
                    return Expression.Add(left, right);
                case "-":
                    return Expression.Subtract(left, right);
                case "*":
                    return Expression.Multiply(left, right);
                case "/":
                    return Expression.Divide(left, right);
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
    }
}
