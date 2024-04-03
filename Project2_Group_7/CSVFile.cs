namespace Project2_Group_7
{
    public static class CSVFile
    {
        public static List<string> ParseCSV(string fileName)
        {
            //String list to store expressions
            List<string> expressionList = new List<string>();

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

                //Create a new CityInfo object
                expressionList.Add(fields[1]);
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
