using System.Text;

namespace Project2_Group_7
{
    public class PostfixConverter
    {
        public string ConvertToPostfix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            StringBuilder postfix = new StringBuilder();

            foreach (char c in infixExpression)
            {
                if (c == '(')
                {
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                        postfix.Append(operators.Pop());
                    operators.Pop(); // Discard '('
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && Priority(c) <= Priority(operators.Peek()))
                        postfix.Append(operators.Pop());
                    operators.Push(c);
                }
                else if (char.IsLetterOrDigit(c))
                {
                    postfix.Append(c);
                }
            }

            // Append remaining operators
            while (operators.Count > 0)
                postfix.Append(operators.Pop());

            return postfix.ToString();
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
