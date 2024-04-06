using System.Text;

namespace Project2_Group_7
{
    /*
     * Class Name:		PrefixConverter
     * Purpose:			A class capable of converting infix expressions to prefix notation
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public class PrefixConverter
    {
        /*
        * Method Name: ConvertToPrefix
        * Purpose: Convert infix expression to prefix notation
        * Accepts: Expression in infix notation as string
        * Returns: Expression in prefix notation as string
        */
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
