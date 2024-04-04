using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Group_7
{
    public class ExpressionData
    {
        public string Sno { get; set; }
        public string Infix { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public int PrefixResult { get; set; }
        public int PostfixResult { get; set; }
        public bool Match { get; set; }

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
        public void Update(string prefix, string postfix, int prefixResult, int postfixResult, bool match)
        {
            Prefix = prefix;
            Postfix = postfix;
            PrefixResult = prefixResult;
            PostfixResult = postfixResult;
            Match = match;
        }

        public override string ToString()
        {
            return $"Sno: {Sno}, Infix: {Infix}, Prefix: {Prefix}, Postfix: {Postfix}, Prefix Result: {PrefixResult}, Postfix Result: {PostfixResult}, Match: {Match}";
        }
    }
}
