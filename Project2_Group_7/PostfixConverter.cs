namespace Project2_Group_7
{
    public class PostfixConverter
    {
        // Convert infix expression to postfix notation
        public string ConvertToPostfix(string infixExpression)
        {
            Stack<char> operators = new Stack<char>();
            Queue<char> output = new Queue<char>();

            foreach (char c in infixExpression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    output.Enqueue(c);
                }
                else if (c == '(')
                {
                    operators.Push(c);
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && Precedence(c) <= Precedence(operators.Peek()))
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    while (operators.Peek() != '(')
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Pop(); // Discard '('
                }
            }

            while (operators.Count > 0)
            {
                output.Enqueue(operators.Pop());
            }

            return string.Join("", output);
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
