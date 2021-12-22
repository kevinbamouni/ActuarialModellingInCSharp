using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace BasicTermS
{
    class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            //Console.WriteLine("Hello, World!");
            //using (var reader = new StreamReader("/Data/mort_table.csv"))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    // Do any configuration to `CsvReader` before creating CsvDataReader.
            //    using (var dr = new CsvDataReader(csv))
            //    {
            //        var dt = new DataTable();
            //        dt.Load(dr);
            //    }
            //}

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            var disc_rate_ann = new DataTable();
            disc_rate_ann.Columns.Add("year", typeof(int));
            disc_rate_ann.Columns.Add("zero_spot", typeof(string));

            var mort_table = new DataTable();
            mort_table.Columns.Add("Age", typeof(int));
            mort_table.Columns.Add("0", typeof(string));
            mort_table.Columns.Add("1", typeof(string));
            mort_table.Columns.Add("2", typeof(string));
            mort_table.Columns.Add("3", typeof(string));
            mort_table.Columns.Add("4", typeof(string));
            mort_table.Columns.Add("5", typeof(string));

            var model_point = new DataTable();
            model_point.Columns.Add("point_id", typeof(int));
            model_point.Columns.Add("age_at_entry", typeof(int));
            model_point.Columns.Add("sex", typeof(string));
            model_point.Columns.Add("policy_term", typeof(string));
            model_point.Columns.Add("policy_count", typeof(string));
            model_point.Columns.Add("sum_assured", typeof(string));

            using (var reader = new StreamReader("/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/disc_rate_ann.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    disc_rate_ann.Load(dr);
                }
            }

            using (var reader = new StreamReader("/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/mort_table.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    mort_table.Load(dr);
                }
            }

            using (var reader = new StreamReader("/Users/kevinbamouni/OneDrive/8-PROJETS/ActuarialModellingInCSharp/BasicTermS/Data/model_point_table.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    model_point.Load(dr);
                }
            }

            var query = from mp in model_point.AsEnumerable()
                        join mr in mort_table.AsEnumerable()
                           on mp.Field<int>("age_at_entry") equals
                           mr.Field<int>("Age")
                           select new { mp, mr };

            //DataTable boundTable = query.ToDataTable<DataRow>();
            

            foreach (DataRow row in model_point.Rows)
            {
                Console.WriteLine("{0,8} {1,8}", (int)row["age_at_entry"], row["sex"]);
            }
        }
    }
}