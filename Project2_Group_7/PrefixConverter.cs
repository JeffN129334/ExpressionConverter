using System.Text;

namespace Project2_Group_7
{
    public class PrefixConverter
    {
        // Convert infix expression to prefix notation
        public string ConvertToPrefix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            Stack<string> operands = new Stack<string>();
            StringBuilder prefix = new StringBuilder();

            foreach (char c in infixExpression.Reverse())
            {
                if (char.IsLetterOrDigit(c))
                {
                    operands.Push(c.ToString());
                }
                else if (c == ')')
                {
                    operators.Push(c);
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && Precedence(c) < Precedence(operators.Peek()))
                    {
                        string op2 = operands.Pop();
                        string op1 = operands.Pop();
                        operands.Push(op2 + op1 + c);
                    }
                    operators.Push(c);
                }
                else if (c == '(')
                {
                    while (operators.Peek() != ')')
                    {
                        string op2 = operands.Pop();
                        string op1 = operands.Pop();
                        operands.Push(op2 + op1 + operators.Pop());
                    }
                    operators.Pop(); // Discard '('
                }
            }

            while (operators.Count > 0)
            {
                string op2 = operands.Pop();
                string op1 = operands.Pop();
                operands.Push(op2 + op1 + operators.Pop());
            }

            return operands.Peek();
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
