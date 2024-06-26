﻿namespace Project2_Group_7
{
    /*
     * Class Name:		XMLExtension
     * Purpose:			A StreamWriter extension class for writing XML elements
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public static class XMLExtension
    {
        public static void WriteStartDocument(this StreamWriter writer)
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

        public static void WriteStartElement(this StreamWriter writer, string elementName)
        {
            writer.WriteLine("\t<" + elementName + ">");
        }

        public static void WriteEndElement(this StreamWriter writer, string elementName)
        {
            writer.WriteLine("\t</" + elementName + ">");
        }

        public static void WriteAttribute(this StreamWriter writer, string attributeName, string attributeValue)
        {
            writer.WriteLine(attributeName + "=\"" + attributeValue + "\"");
        }
    }
}
