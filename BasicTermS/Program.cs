using System;
using System.Data;
using System.Collections.Concurrent;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BasicTermS
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonresult = JsonSerializer.Serialize(concurrentresult, options);
            File.WriteAllText(Tables.results, jsonresult);
            //File.WriteAllLines(Tables.results, concurrentresult.Select(kvp => string.Format("{{0}:{1} \n}", concurrentresult.Keys, concurrentresult.Values)));
        }
    }
}