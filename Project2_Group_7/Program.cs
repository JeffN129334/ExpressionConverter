using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml;

namespace Project2_Group_7
{
    class Program
    {
        static string inputFile = "../../../Data/Project 2_INFO_5101.csv";
        static string outputFile = "../../../Data/Output.xml";
    
        static void Main(string[] args)
        {
            // Read infix expressions from CSV file
            List<ExpressionData> infixExpressions = CSVFile.ParseCSV(inputFile);

            // Process expressions
            ProcessExpressions(infixExpressions);

            // Generate and display summary report
            GenerateSummaryReport(infixExpressions);

            // Prompt?? user to upload XML file -> not sure what it means to prompt the user for the file since the file is already here
            OpenXMLFile();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /*
        * Method Name: ProcessExpressions
        * Purpose: Perform all the necessary conversions and calculations for an infix expression, then update the object to reflect those
        * Accepts: A list of ExpressionData objects
        * Returns: Void
        */
        static void ProcessExpressions(List<ExpressionData> expressions)
        {
            PrefixConverter prefixConverter = new PrefixConverter();
            PostfixConverter postfixConverter = new PostfixConverter();
            ExpressionEvaluation expressionEvaluation = new ExpressionEvaluation();

            foreach (ExpressionData expr in expressions)
            {
                // Convert to prefix and postfix notations
                string prefix = prefixConverter.ConvertToPrefix(expr.Infix);
                string postfix = postfixConverter.ConvertToPostfix(expr.Infix);

                // Evaluate expressions
                double prefixResult = expressionEvaluation.EvaluatePrefixExpression(prefix);
                double postfixResult = expressionEvaluation.EvaluatePostfixExpression(postfix);

                // Compare results
                CompareExpressions comparer = new CompareExpressions();
                double comparisonResult = comparer.Compare(prefixResult, postfixResult);

                //Update ExpressionData object
                expr.Update(prefix, postfix, prefixResult, postfixResult, comparisonResult == 0);
            }
        }

        /*
        * Method Name: GenerateSummaryReport
        * Purpose: Output the updated ExpressionData objects to both the console and the xml file
        * Accepts: A list of ExpressionData object
        * Returns: Void
        */
        static void GenerateSummaryReport(List<ExpressionData> expressions)
        {
            //Header for console
            Console.WriteLine("\nSummary Report:");

            // Save summary report to XML file
            using (XmlWriter writer = XmlWriter.Create(outputFile))
            {
                writer.WriteStartDocument();                       //Start Document
                writer.WriteStartElement("Summary");        //Start Summary
                writer.WriteStartElement("Conversions");    //Start Conversions

                foreach (var expr in expressions)
                {
                    //Write data to console
                    Console.WriteLine(expr.ToString());

                    //Write data to XML
                    writer.WriteStartElement("Expression"); //Start Expression
                    writer.WriteElementString("Sno", expr.Sno);
                    writer.WriteElementString("Infix", expr.Infix);
                    writer.WriteElementString("Prefix", expr.Prefix);
                    writer.WriteElementString("Postfix", expr.Postfix);
                    writer.WriteElementString("Evaluation", expr.Match ? expr.PostfixResult.ToString() : "Indeterminate");
                    writer.WriteElementString("Comparison", expr.Match.ToString());
                    writer.WriteEndElement();                       //End Expression
                }
                writer.WriteEndElement();                           // End Conversions
                writer.WriteStartElement("Details");          //Start Details
                writer.WriteElementString("TotalExpressions", expressions.Count.ToString());
                writer.WriteEndElement();                           //End Details
                writer.WriteEndElement();                           //End Summary
                writer.WriteEndDocument();                        //End Document
            }

            Console.WriteLine("Summary report has been saved to Output.xml");
        }

        /*
        * Method Name: OpenXMLFile
        * Purpose: Open the newly created XML file in the broswer
        * Accepts: N/A
        * Returns: Void
        */
        static void OpenXMLFile()
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputFile))
                };
                Process.Start(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}