using System.Linq.Expressions;
using System.Text;

namespace Project2_Group_7
{
    public class ExpressionEvaluation
    {
        // Evaluate prefix expression using expression trees
        public int EvaluatePrefixExpression(string prefixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = GetTokens(prefixExpression);

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
            string[] tokens = GetTokens(postfixExpression);

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

        // Method to extract tokens from the expression
        private string[] GetTokens(string expression)
        {
            List<string> tokens = new List<string>();
            StringBuilder token = new StringBuilder();

            foreach (char c in expression)
            {
                if (IsOperator(token.ToString()) || c == '(' || c == ')')
                {
                    if (token.Length > 0)
                    {
                        tokens.Add(token.ToString());
                        token.Clear();
                    }
                    tokens.Add(c.ToString());
                }
                else if (char.IsDigit(c))
                {
                    token.Append(c);
                }
            }

            if (token.Length > 0)
            {
                tokens.Add(token.ToString());
            }

            return tokens.ToArray();
        }

        // Combine expressions based on operators
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

        // Check if the string is an operator
        private bool IsOperator(string str)
        {
            return str == "+" || str == "-" || str == "*" || str == "/";
        }
    }
}
