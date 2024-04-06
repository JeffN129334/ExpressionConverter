using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Group_7
{
    /*
     * Class Name:		ExpressionData
     * Purpose:			Creating and storing objects that represent an expression
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public class ExpressionData
    {
        public string Sno { get; set; }
        public string Infix { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public double PrefixResult { get; set; }
        public double PostfixResult { get; set; }
        public bool Match { get; set; }

        /*
        * Method Name: Constructor
        * Purpose: Create a new expression data object with an id and an infix representation
        * Accepts: ID and infix representation as strings
        * Returns:
        */
        public ExpressionData(string sno, string infix)
        {
            Sno = sno;
            Infix = infix;
            Prefix = "";
            Postfix = "";
            PrefixResult = 0;
            PostfixResult = 0;
            Match = false;
        }

        /*
        * Method Name: Update
        * Purpose: Update the object to include the calculated data for postfix and prefix notation
        * Accepts: Prefix and Postfix representations as strings, prefix and postfix calculation results as doubles, bool representing whether or not the calculations got the same answer
        * Returns: Void
        */
        public void Update(string prefix, string postfix, double prefixResult, double postfixResult, bool match)
        {
            Prefix = prefix;
            Postfix = postfix;
            PrefixResult = prefixResult;
            PostfixResult = postfixResult;
            Match = match;
        }

        /*
        * Method Name: ToString
        * Purpose: Create a nicely formatted string representation of the object
        * Accepts: N/A
        * Returns: String
        */
        public override string ToString()
        {
            string output = string.Format("|{0,5}|{1,20}|{2,20}|{3,20}|{4,10}|{5,10}|{6,7}|", Sno, Infix, Prefix, Postfix, PrefixResult, PostfixResult, Match);
            return output;
        }
    }
}
