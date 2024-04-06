namespace Project2_Group_7
{
    /*
     * Class Name:		CSVFile
     * Purpose:			A class containing a method for parsing a CSV file to a format that can be used by the program
     * Coder:			    Gui Miranda, Jeff Nesbitt, Andrew Mattice
     * Date:			    2024-04-05
    */
    public static class CSVFile
    {
        /*
        * Method Name: ParseCSV
        * Purpose: Parse the input file to a list of ExpressionData objects
        * Accepts: CSV file name as string
        * Returns: List of ExpressionData objects
        */
        public static List<ExpressionData> ParseCSV(string fileName)
        {
            //String list to store expressions
            List<ExpressionData> expressionList = new List<ExpressionData>();

            //Read all the text from the file
            string csvContent = File.ReadAllText(fileName);

            //Split the text into rows
            string[] rows = csvContent.Split('\n');
            string[] fields;

            //For each row (minus the first one which contains the headers)
            for (int i = 1; i < rows.Length; i++)
            {
                //Split the rows into fields
                fields = rows[i].Split(',');

                // Trim each field to remove leading/trailing whitespace including '\r' characters
                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = fields[j].Trim();
                }

                //Create a new expression object
                expressionList.Add(new ExpressionData(fields[0], fields[1]));
            }

            //If the list is empty throw an exception
            if (expressionList.Count <= 0)
            {
                throw new Exception("No data found");
            }

            return expressionList;
        }
    }
}
