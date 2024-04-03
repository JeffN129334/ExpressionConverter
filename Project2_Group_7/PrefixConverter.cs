using System.Text;

namespace Project2_Group_7
{
    public class PrefixConverter
    {
        public string ConvertToPrefix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            StringBuilder prefix = new StringBuilder();

            // Reverse the infix expression to facilitate conversion
            char[] infixArray = infixExpression.ToCharArray();
            Array.Reverse(infixArray);
            infixExpression = new string(infixArray);

            foreach (char c in infixExpression)
            {
                if (c == '(')
                {
                    while (operators.Count > 0 && operators.Peek() != ')')
                        prefix.Append(operators.Pop());
                    operators.Pop(); // Discard ')'
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && Priority(c) < Priority(operators.Peek()))
                        prefix.Append(operators.Pop());
                    operators.Push(c);
                }
                else if (char.IsLetterOrDigit(c))
                {
                    prefix.Append(c);
                }
            }

            // Append remaining operators
            while (operators.Count > 0)
                prefix.Append(operators.Pop());

            // Reverse the result to get the prefix expression
            char[] prefixArray = prefix.ToString().ToCharArray();
            Array.Reverse(prefixArray);
            return new string(prefixArray);
        }

        private int Priority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0; // Parentheses
            }
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
