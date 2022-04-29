using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace SimpleLife
{
    public class Economic
    {
        /// <summary>
        /// Hold the economic scenarios of the projections
        /// </summary>
        public DataTable EconomicScenarios;

        public Economic(string path, string schema)
        {
            EconomicScenarios = DataFromCsv.ReadDataTableFromCsv(path, schema);
        }

        public decimal DiscRate(int ScenID, int t)
        {
            var a = from rw in EconomicScenarios.AsEnumerable()
                    where rw.Field<int>("ScenID") == ScenID && rw.Field<int>("Year") == t
                    select rw.Field<decimal>("IntRate");
            return a.FirstOrDefault<decimal>();
        }

        public decimal InflFactor(int t)
        {
            if (t == 0)
            {
                return 1;
            }
            else
            {
                //return InflFactor(t - 1) / (1 + AsmpLookup("InflRate"));
                return InflFactor(t - 1) / (1 + 0);
            }
        }

        public decimal InvstRetRate(int ScenID, int t)
        {
            return DiscRate(ScenID, t);
        }
    }
}
