using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace BasicTermS
{
    public static class DataFromCsv
    {

        public static Type GetColumnType(string type)
        {
            var result = typeof(string);

            switch (type)
            {
                case "int":
                    result = typeof(int);
                    break;
                case "long":
                    result = typeof(long);
                    break;
                case "float":
                    result = typeof(float);
                    break;
                case "double":
                    result = typeof(double);
                    break;
                case "Double":
                    result = typeof(Double);
                    break;
                case "decimal":
                    result = typeof(decimal);
                    break;
                default:
                    break;
            }
            return result;
        }

        // Params :
        //
        // Dictionary<string, string> paramColumnDictionnary = {"ColumnName1":"ColumnType1",
        //                                                      "ColumnName2":"ColumnType2",
        //                                                      "ColumnName3":"ColumnType3",
        //                                                      ...}
        public static DataTable AddColumnWithType(DataTable paramDataTable, Dictionary<string, string> paramColumnDictionnary)
        {
            foreach(var item in paramColumnDictionnary)
            {
                paramDataTable.Columns.Add(item.Key, GetColumnType(item.Value));
            }
            return paramDataTable;
        }

        public static DataTable ReadDataTableFromCsv(string pathToCsvFile, string dataSchema)
        {
            DataTable dataTable = new DataTable();
            Dictionary<string, string> schemas = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataSchema);
            dataTable = DataFromCsv.AddColumnWithType(dataTable, schemas);

            //CultureInfo cultureInfo = new CultureInfo("en-US", false);
            //cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            //cultureInfo.TextInfo.ListSeparator = ";";
            //cultureInfo.NumberFormat.NumberDecimalDigits = 9;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                NewLine = Environment.NewLine,
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            try
            {
                using (var reader = new StreamReader(pathToCsvFile))
                using (var csv = new CsvReader(reader, config))
                {
                    // Do any configuration to `CsvReader` before creating CsvDataReader.
                    using (var dr = new CsvDataReader(csv))
                    {
                        try
                        {
                            dataTable.Load(dr);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("File -->  " + pathToCsvFile);
                            Console.WriteLine("Schemas -->  " + dataSchema);
                            Console.WriteLine(ex.ToString());
                        }                        
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException dirEx)
            {
                // Let the user know that the directory did not exist.
                Console.WriteLine("Directory not found: " + dirEx.Message);
            }

            return dataTable;
        }
    }
}