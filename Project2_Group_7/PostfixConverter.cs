using System.Text;

namespace Project2_Group_7
{
    public class PostfixConverter
    {
        // Convert infix expression to postfix notation
        public string ConvertToPostfix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            StringBuilder postfix = new StringBuilder();

            foreach (char c in infixExpression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    postfix.Append(c);
                }
                else if (c == '(')
                {
                    operators.Push(c);
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && Precedence(c) <= Precedence(operators.Peek()))
                    {
                        postfix.Append(operators.Pop());
                    }
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    while (operators.Peek() != '(')
                    {
                        postfix.Append(operators.Pop());
                    }
                    operators.Pop(); // Discard '('
                }
            }

            while (operators.Count > 0)
            {
                postfix.Append(operators.Pop());
            }

            return postfix.ToString();
        }

        // Helper method to determine precedence of operators
        private int Precedence(char op)
        {
            if (op == '*' || op == '/')
                return 2;
            else if (op == '+' || op == '-')
                return 1;
            else
                return 0; // Parentheses
        }

        // Helper method to check if character is an operator
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
