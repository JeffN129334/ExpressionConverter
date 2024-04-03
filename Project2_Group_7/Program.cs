using System.Diagnostics;
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
            List<string> infixExpressions = CSVFile.ParseCSV(inputFile);

            // Process expressions
            ProcessExpressions(infixExpressions);

            // Generate and display summary report
            GenerateSummaryReport(infixExpressions);

            // Prompt?? user to upload XML file -> not sure what it means to prompt the user for the file since the file is already here
            OpenXMLFile();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void ProcessExpressions(List<string> expressions)
        {
            PrefixConverter prefixConverter = new PrefixConverter();
            PostfixConverter postfixConverter = new PostfixConverter();
            ExpressionEvaluation expressionEvaluation = new ExpressionEvaluation();

            Console.WriteLine("Summary Report:");

            foreach (var infix in expressions)
            {
                // Convert to prefix and postfix notations
                string prefix = prefixConverter.ConvertToPrefix(infix);
                string postfix = postfixConverter.ConvertToPostfix(infix);

                // Evaluate expressions
                int prefixResult = expressionEvaluation.EvaluatePrefixExpression(prefix);
                int postfixResult = expressionEvaluation.EvaluatePostfixExpression(postfix);

                // Compare results
                CompareExpressions comparer = new CompareExpressions();
                int comparisonResult = comparer.Compare(prefixResult, postfixResult);

                Console.WriteLine($"Infix: {infix}, Prefix: {prefix}, Postfix: {postfix}, Prefix Result: {prefixResult}, Postfix Result: {postfixResult}, Match: {(comparisonResult == 0 ? "True" : "False")}");
            }
        }

        static void GenerateSummaryReport(List<string> expressions)
        {
            Console.WriteLine("\nSummary Report:");

            // Save summary report to XML file
            using (XmlWriter writer = XmlWriter.Create(outputFile))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Summary");

                // Write conversion outputs to XML
                writer.WriteStartElement("Conversions");

                foreach (var infix in expressions)
                {
                    writer.WriteStartElement("Expression");
                    writer.WriteElementString("Infix", infix);

                    PrefixConverter prefixConverter = new PrefixConverter();
                    string prefix = prefixConverter.ConvertToPrefix(infix);
                    writer.WriteElementString("Prefix", prefix);

                    PostfixConverter postfixConverter = new PostfixConverter();
                    string postfix = postfixConverter.ConvertToPostfix(infix);
                    writer.WriteElementString("Postfix", postfix);

                    ExpressionEvaluation expressionEvaluation = new ExpressionEvaluation();
                    int prefixResult = expressionEvaluation.EvaluatePrefixExpression(prefix);
                    int postfixResult = expressionEvaluation.EvaluatePostfixExpression(postfix);
                    writer.WriteElementString("Evaluation", prefixResult.ToString());

                    CompareExpressions comparer = new CompareExpressions();
                    int comparisonResult = comparer.Compare(prefixResult, postfixResult);
                    writer.WriteElementString("Comparison",  (comparisonResult == 0 ? "True" : "False"));

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