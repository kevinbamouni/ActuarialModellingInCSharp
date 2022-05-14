//using System;
using System.Data;
using System.Collections.Concurrent;
//using System.IO;
//using System.Text;
using System.Text.Json;
using MongoDB.Driver;


namespace BasicTermS
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable modelpointest = DataFromCsv.ReadDataTableFromCsv(Tables.PathModelPoint, Tables.SchemasModelPoint);
            ////////////////////////////////////////////////////////////////////////////////////////
            //ConcurrentDictionary<int, Dictionary<string, List<double>>> concurrentresult = new ConcurrentDictionary<int, Dictionary<string, List<double>>>();
            //int dickey = 0;
            //Parallel.ForEach(modelpointest.AsEnumerable(), modelpointestrow =>
            //{
            //    Projection proj = new Projection(modelpointestrow);
            //    concurrentresult.AddOrUpdate(dickey, proj.result_cf(), (key, oldValue) => proj.result_cf());
            //    dickey++;
            //});

            //var options = new JsonSerializerOptions { WriteIndented = true };
            //string jsonresult = JsonSerializer.Serialize(concurrentresult, options);
            //File.WriteAllText(Tables.results, jsonresult);

            /////////////////////////////////////////////////////////////////////////////////////// PV projection
            Dictionary<string, double> results_run = new Dictionary<string, double>();
            foreach (DataRow row in modelpointest.Rows)
            {
                Projection proj = new Projection(row);
                results_run = proj.result_pv();
                //break;
            }
            //////////////////////////////////////////////////////////////////////////////////////// Mango DB test and save
            //MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");

            //var dbList = dbClient.ListDatabases().ToList();

            //Console.WriteLine("The list of databases on this server is: ");
            //foreach (var db in dbList)
            //{
            //    Console.WriteLine(db);
            //}
        }
    }
}