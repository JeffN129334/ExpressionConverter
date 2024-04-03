using System.Linq.Expressions;

namespace Project2_Group_7
{
    public class ExpressionEvaluation
    {
        // Evaluate prefix expression using expression trees
        public int EvaluatePrefixExpression(string prefixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = prefixExpression.Split(' ');

            foreach (string token in tokens.Reverse())
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

            Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(stack.Pop());
            Func<int> compiled = lambda.Compile();
            return compiled();
        }

        // Evaluate postfix expression using expression trees
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

            Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(stack.Pop());
            Func<int> compiled = lambda.Compile();
            return compiled();
        }

        // Helper method to combine expressions using operators
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

        // Helper method to check if token is an operator
        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
    }
}
