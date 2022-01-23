using System;
using System.Data;
using System.Collections.Concurrent;
using Excel = Microsoft.Office.Interop.Excel;

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

            // Test de la fonctionnalité des hypothèses
            DataTable mortrate = DataFromCsv.ReadDataTableFromCsv(Tables.PathMortRate, Tables.SchemasMortRate);
            foreach (DataRow row in mortrate.Rows)
            {
                break;
                //Console.WriteLine("{0,-12} {1,-12} {2,-12} {3,-12} {4,-12} {5,-12} {6,-12}", row["Age"], row["0"], row["1"], row["2"], row["3"], row["4"], row["5"]);
                //var a = row.Field<Double>("0");
            }

            //DataTable discountrate = DataFromCsv.ReadDataTableFromCsv(Tables.PathDiscountRate, Tables.SchemasDiscountRate);
            //foreach (DataRow row in discountrate.Rows)
            //{
            //    break;
            //    //Console.WriteLine("{0,-12} {1,-12}", row["year"], row["zero_spot"]);
            //}

            //DataTable modelpoint = DataFromCsv.ReadDataTableFromCsv(Tables.PathModelPoint, Tables.SchemasModelPoint);
            //foreach (DataRow row in modelpoint.Rows)
            //{
            //    break;
            //    //Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}", row["point_id"], row["age_at_entry"], row["sex"], row["policy_term"], row["policy_count"], row["sum_assured"]);
            //}

            //Dictionary<string, List<double>> results_run = new Dictionary<string, List<double>>();
            //ConcurrentDictionary<string, List<double>> concurrentresult = new ConcurrentDictionary<string, List<double>>();
            ConcurrentDictionary<int, Dictionary<string, List<double>>> concurrentresult = new ConcurrentDictionary<int, Dictionary<string, List<double>>>();
            DataTable modelpointest = DataFromCsv.ReadDataTableFromCsv(Tables.PathModelPoint, Tables.SchemasModelPoint);
            int dickey = 0;
            
            //foreach (DataRow row in modelpointest.Rows)
            //{
            //    Projection proj = new Projection(row);
            //    results_run = proj.result_cf();
            //    //break;
            //}

            Parallel.ForEach(modelpointest.AsEnumerable(), modelpointestrow => 
            { 
                Projection proj = new Projection(modelpointestrow);
                concurrentresult.AddOrUpdate(dickey, proj.result_cf(), (key, oldValue) => proj.result_cf());
                dickey++;
            });

            //File.WriteAllLines(Tables.results, concurrentresult.Select(kvp => string.Format("{{0}:{1} \n}", concurrentresult.Keys, concurrentresult.Values)));
        }
    }
}