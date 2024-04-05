using System.Collections;

namespace Project2_Group_7
{
    public class CompareExpressions : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("Both objects must be non-null.");
            }

            if (!(x is double) || !(y is double))
            {
                throw new ArgumentException("Both objects must be of type double.");
            }

            double resultX = (double)x;
            double resultY = (double)y;

            // Compare the results of prefix and postfix evaluation
            if (resultX == resultY)
            {
                return 0; // Results match
            }
            else
            {
                return -1; // Results do not match
            }
        }
    }
}
