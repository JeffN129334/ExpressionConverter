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

            if (!(x is int) || !(y is int))
            {
                throw new ArgumentException("Both objects must be of type int.");
            }

            int resultX = (int)x;
            int resultY = (int)y;

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
