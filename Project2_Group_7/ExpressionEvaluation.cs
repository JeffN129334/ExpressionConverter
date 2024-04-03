namespace Project2_Group_7
{
    public class ExpressionEvaluation
    {
        public int EvaluatePrefixExpression(string prefixExpression)
        {
            Stack<int> operands = new Stack<int>();

            for (int i = prefixExpression.Length - 1; i >= 0; i--)
            {
                char c = prefixExpression[i];
                if (char.IsDigit(c))
                {
                    operands.Push(c - '0');
                }
                else if (IsOperator(c))
                {
                    int op1 = operands.Pop();
                    int op2 = operands.Pop();
                    int result = PerformOperation(c, op1, op2);
                    operands.Push(result);
                }
            }

            return operands.Pop();
        }

        public int EvaluatePostfixExpression(string postfixExpression)
        {
            Stack<int> operands = new Stack<int>();

            foreach (char c in postfixExpression)
            {
                if (char.IsDigit(c))
                {
                    operands.Push(c - '0');
                }
                else if (IsOperator(c))
                {
                    int op2 = operands.Pop();
                    int op1 = operands.Pop();
                    int result = PerformOperation(c, op1, op2);
                    operands.Push(result);
                }
            }

            return operands.Pop();
        }

        private int PerformOperation(char op, int operand1, int operand2)
        {
            switch (op)
            {
                case '+':
                    return operand1 + operand2;
                case '-':
                    return operand1 - operand2;
                case '*':
                    return operand1 * operand2;
                case '/':
                    if (operand2 == 0)
                        throw new DivideByZeroException();
                    return operand1 / operand2;
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
