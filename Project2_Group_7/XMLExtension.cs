using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Group_7
{
    public static class XMLExtension
    {
        public static void WriteStartDocument (this StreamWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        }
        public static void WriteStartRootElement(this StreamWriter writer)
        {
            writer.WriteLine("<root>");
        }
        public static void WriteEndRootElement(this StreamWriter writer)
        {
            writer.WriteLine("</root>");
        }
        public static void WriteStartElement(this StreamWriter writer)
        {
            writer.WriteLine("\t<element>");
        }
        public static void WriteEndElement(this StreamWriter writer)
        {
            writer.WriteLine("\t</element>");
        }
        public static void WriteAttribute(this StreamWriter writer)
        {
            //Write attributes to XML file
            throw new NotImplementedException();

        }
    }
}
