using System.Collections;

namespace Project2_Group_7
{
    /*
     * Class Name:		CompareExpressions
     * Purpose:			A class containing a method for comparing expression results
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public class CompareExpressions : IComparer
    {
        /*
        * Method Name: Compare
        * Purpose: Compare the results of two doubles
        * Accepts: Two objects (which should be doubles)
        * Returns: Int representation of comparison results
        */
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
