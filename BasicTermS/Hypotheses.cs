using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace BasicTermS
{
    public class Hypotheses
    {
        public DataTable MortTable;
        public string PathMortTable;

        public Hypotheses(string pathMortTable)
        {
            PathMortTable = pathMortTable;

            MortTable = new DataTable();

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
        }
    }
}

