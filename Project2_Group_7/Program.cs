using System.Diagnostics;
using System.Runtime;

namespace Project2_Group_7
{
    public class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "../../../Data/Project 2_INFO_5101.csv";
            string outputFile = "../../../Data/Output.xml";

            //Parse CSV to string list and output it
            List<string> list = CSVFile.ParseCSV(inputFile);
            foreach (string item in list)
            {
                Console.WriteLine(item);
            }


            //Write stuff to an XML file
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteStartDocument();
                writer.WriteStartRootElement();
                writer.WriteStartElement();
                writer.WriteAttribute();        //does nothing yet
                writer.WriteEndElement();
                writer.WriteEndRootElement();
            }

            //Open the XML file
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