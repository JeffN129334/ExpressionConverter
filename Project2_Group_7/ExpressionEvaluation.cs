using System.Linq.Expressions;
using System.Text;

namespace Project2_Group_7
{
    public class ExpressionEvaluation
    {
        /*
        * Method Name: EvaluatePrefixExpression
        * Purpose: Evaluate a prefix expression using expression trees
        * Accepts: A string containing a single expression in prefix form
        * Returns: A double containing the result of the evaluation
        */
        public double EvaluatePrefixExpression(string prefixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = prefixExpression.Select(c => c.ToString()).ToArray();

            foreach (string token in tokens.Reverse())
            {
                if (IsOperator(token))
                {
                    //Left operator will be on the top
                    Expression left = stack.Pop();
                    Expression right = stack.Pop();
                    Expression combined = CombineExpressions(token, left, right);
                    stack.Push(combined);
                }
                else
                {
                    stack.Push(Expression.Constant(double.Parse(token)));
                }
            }

            Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(stack.Pop());
            Func<double> compiled = lambda.Compile();
            return compiled();
        }

        /*
        * Method Name: EvaluatePostfixExpression
        * Purpose: Evaluate a postfix expression using expression trees
        * Accepts: A string containing a single expression in postfix form
        * Returns: A double containing the result of the evaluation
        */
        public double EvaluatePostfixExpression(string postfixExpression)
        {
            Stack<Expression> stack = new Stack<Expression>();
            string[] tokens = postfixExpression.Select(c => c.ToString()).ToArray();

            foreach (string token in tokens)
            {
                if (IsOperator(token))
                {
                    //Right operator will be on the top
                    Expression right = stack.Pop();
                    Expression left = stack.Pop();
                    Expression combined = CombineExpressions(token, left, right);
                    stack.Push(combined);
                }
                else
                {
                    stack.Push(Expression.Constant(double.Parse(token)));
                }
            }

            Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(stack.Pop());
            Func<double> compiled = lambda.Compile();
            return compiled();
        }

        // Old Tokenizer - Delete before submission
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

        /*
        * Method Name: CombineExpressions
        * Purpose: Evaluate the operands based on the operator
        * Accepts: An operator string, and an Expression object for each operator
        * Returns: An expression object containing the result
        */
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

        /*
        * Method Name: IsOperator
        * Purpose: Helper method to check if a string is representing a valid operator
        * Accepts: String
        * Returns: Bool
        */
        private bool IsOperator(string str)
        {
            return str == "+" || str == "-" || str == "*" || str == "/";
        }
    }
}
