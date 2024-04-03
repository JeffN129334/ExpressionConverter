using System.Text;

namespace Project2_Group_7
{
    public class PrefixConverter
    {
        // Convert infix expression to prefix notation
        public string ConvertToPrefix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            StringBuilder prefix = new StringBuilder();

            foreach (char c in infixExpression.Reverse())
            {
                if (char.IsDigit(c))
                {
                    prefix.Insert(0, c);
                }
                else if (c == ')')
                {
                    operators.Push(c);
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && Precedence(c) < Precedence(operators.Peek()))
                    {
                        prefix.Insert(0, operators.Pop());
                    }
                    operators.Push(c);
                }
                else if (c == '(')
                {
                    while (operators.Peek() != ')')
                    {
                        prefix.Insert(0, operators.Pop());
                    }
                    operators.Pop(); // Discard '('
                }
            }

            while (operators.Count > 0)
            {
                prefix.Insert(0, operators.Pop());
            }

            return prefix.ToString();
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
