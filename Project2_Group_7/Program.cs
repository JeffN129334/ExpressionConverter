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

        static void ProcessExpressions(List<ExpressionData> expressions)
        {
            PrefixConverter prefixConverter = new PrefixConverter();
            PostfixConverter postfixConverter = new PostfixConverter();
            ExpressionEvaluation expressionEvaluation = new ExpressionEvaluation();

            Console.WriteLine("Summary Report:");

            foreach (ExpressionData expr in expressions)
            {
                // Convert to prefix and postfix notations
                string prefix = prefixConverter.ConvertToPrefix(expr.Infix);
                string postfix = postfixConverter.ConvertToPostfix(expr.Infix);

                // Evaluate expressions
                int prefixResult = expressionEvaluation.EvaluatePrefixExpression(prefix);
                int postfixResult = expressionEvaluation.EvaluatePostfixExpression(postfix);

                // Compare results
                CompareExpressions comparer = new CompareExpressions();
                int comparisonResult = comparer.Compare(prefixResult, postfixResult);

                expr.Update(prefix, postfix, prefixResult, postfixResult, comparisonResult == 0);
                Console.WriteLine(expr.ToString());
            }
        }

        static void GenerateSummaryReport(List<ExpressionData> expressions)
        {
            Console.WriteLine("\nSummary Report:");

            // Save summary report to XML file
            using (XmlWriter writer = XmlWriter.Create(outputFile))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Summary");

                // Write conversion outputs to XML
                writer.WriteStartElement("Conversions");

                foreach (var expr in expressions)
                {
                    writer.WriteStartElement("Expression");
                    writer.WriteElementString("Sno", expr.Sno);
                    writer.WriteElementString("Infix", expr.Infix);
                    writer.WriteElementString("Prefix", expr.Prefix);
                    writer.WriteElementString("Postfix", expr.Postfix);
                    if (expr.Match)
                    {
                        writer.WriteElementString("Evaluation", expr.PostfixResult.ToString());
                    }
                    else
                    {
                        writer.WriteElementString("Evaluation", "Indeterminate");
                    }
                    writer.WriteElementString("Comparison", expr.Match.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); // End Conversions

                // Write summary details
                writer.WriteStartElement("Details");
                writer.WriteElementString("TotalExpressions", expressions.Count.ToString());
                // Add more details if needed
                writer.WriteEndElement(); // End Details

                writer.WriteEndElement(); // End Summary
                writer.WriteEndDocument();
            }

            Console.WriteLine("Summary report has been saved to Output.xml");
        }

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