using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace BasicTermS
{
    public static class HypothesesStatic
    {
        public static DataTable ReadMortalityTable(string pathMortTable, string dataSchema)
        {
            DataTable MortTable = new DataTable();
            Dictionary<string, string> schemas = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataSchema);
            MortTable = DataFromCsv.AddColumnWithType(MortTable, schemas);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader(pathMortTable))
            using (var csv = new CsvReader(reader, config))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    MortTable.Load(dr);
                }
            }

            return MortTable;
        }
    }
}

