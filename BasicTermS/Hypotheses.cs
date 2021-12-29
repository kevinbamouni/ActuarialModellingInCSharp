﻿using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace BasicTermS
{
    public class Hypotheses
    {
        public DataTable MortTable;
        public string PathMortTable;
        public string DataSchema;

        public Hypotheses(string pathMortTable, string dataSchema)
        {
            PathMortTable = pathMortTable;
            DataSchema = dataSchema;
            MortTable = new DataTable();

            //Dictionary<string, string> columns = new Dictionary<string, string>(){{"Age","int"},{"0","float"},{"1","float"},{"2","float"},{"3","float"},{"4","float"},{"5","float"}};
            //string schemacolumn = @"{""Age"":""int"",""0"":""float"",""1"":""float"",""2"":""float"",""3"":""float"",""4"":""float"",""5"":""float""}";
            Dictionary<string, string> schemas = JsonConvert.DeserializeObject<Dictionary<string, string>>(DataSchema);
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
        }
    }
}

