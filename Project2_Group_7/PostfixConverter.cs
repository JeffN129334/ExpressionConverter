namespace Project2_Group_7
{
    /*
     * Class Name:		PostfixConverter
     * Purpose:			A class capable of converting infix expressions to postfix notation
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public class PostfixConverter
    {
        /*
        * Method Name: ConvertToPostfix
        * Purpose: Convert infix expression to postfix notation
        * Accepts: Expression in infix notation as string
        * Returns: Expression in postfix notation as string
        */
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

        /*
        * Method Name: Precedence
        * Purpose: Helper method to determine precedence of operators
        * Accepts: Operator as char
        * Returns: The priority of the operator as int
        */
        private int Precedence(char op)
        {
            if (op == '*' || op == '/')
                return 2;
            else if (op == '+' || op == '-')
                return 1;
            else
                return 0; // Parentheses
        }

        /*
        * Method Name: IsOperator
        * Purpose: Helper method to check if character is an operator
        * Accepts: Operator as char
        * Returns: Bool
        */
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
